using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaseShip))]
    public class AIController : MonoBehaviour
    {
        // тип поведения ИИ
        public enum AIBehaviour
        {
            Null,
            Patrol
        }

        [SerializeField] private AIBehaviour m_AIBehaviour;

        [SerializeField] private AIPointerPatrol[] m_PointPatrol;

        // скорость движения
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;

        // скорость поворота
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        // время упреждения выстрела
        [Range(0.0f, 5.0f)]
        [SerializeField] private float m_TimePreemptiveShot;

        // каждые N секунд выбирается новая сулчайная точка
        [SerializeField] private float m_RandomSelectMovePointTime;

        [SerializeField] private float m_FindNewTargetTime;

        [SerializeField] private float m_ShootDelay;

        // длинна рэйкаста
        [SerializeField] private float m_EvadeRayLength;

        private Rigidbody2D m_rigitbodytarget;
        private Vector3 speed=> m_rigitbodytarget.velocity;

        private SpaseShip m_SpaseShip;

        private SpawnerEntity spawnerEntity;

        private Vector3 m_MovePosition;

        private Destructible m_SelectedTarget;

        private Timer m_RandomizeDirectionTimer;
        private Timer m_FindNewTargetTimer;
        private Timer m_FireTimer;

        private void Start()
        {
            InitTimer();
            m_SpaseShip = GetComponent<SpaseShip>();
            spawnerEntity = GetComponentInParent<SpawnerEntity>();
            m_PointPatrol = spawnerEntity.PointPatroll;
            m_SpaseShip.transform.SetParent(null);

        }

        private void Update()
        {
            UpdateTimer();
            UpdateAi();
        }

        private void UpdateAi()
        {
            if (m_AIBehaviour == AIBehaviour.Null)
            {
                SetPatrolBehaviour(m_PointPatrol);
            }    

            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateAIBehaviourPatrol();
            }
        }

        public void UpdateAIBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();
        }

        // метод стрельбы ИИ
        private void ActionFire()
        {
            if(m_SelectedTarget != null)
            {
                if(m_FireTimer.IsFinished == true)
                {
                    m_SpaseShip.Fire(TurretMode.Primary);
                    m_FireTimer.Start(m_ShootDelay);
                }
            }
        }

        // метод поиска новой цели для атаки
        private void ActionFindNewAttackTarget()
        {

            if (m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearestDestructibleTarget();
                m_FindNewTargetTimer.Start(m_ShootDelay);
            }

        }

        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = 50.0f;

            Destructible potentialTarget = null;

            foreach (var v in Destructible.AllDestructible)
            {
                if (v.GetComponent<SpaseShip>() == m_SpaseShip) continue;

                if (v.TeamId == Destructible.TeamIdNeutral) continue;

                if (v.TeamId == m_SpaseShip.TeamId) continue;

                float dist = Vector2.Distance(m_SpaseShip.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                    m_rigitbodytarget = v.GetComponent<Rigidbody2D>();
                }
            }
            return potentialTarget;
        }

        // метод навигации ИИ
        private void ActionControlShip()
        {
            m_SpaseShip.ThrustControl = m_NavigationLinear;
            m_SpaseShip.TorqueControl = ComputeAlliginTorqueNormalized(m_MovePosition, m_SpaseShip.transform) * m_NavigationAngular;
        }


        private const float MAX_ANGLE = 45.0f;
        private static float ComputeAlliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            // переводим позицию в локальные координаты
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            // получаем угол между двумя векторами
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            // ограничиваем полученый угол до максимума в -45/45 и нормализуем его делением на 45
            // при максимальном угле корабль поворачиваем максимально быстро
            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }


        private void ActionFindNewMovePosition()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null)
                    m_MovePosition = m_SelectedTarget.transform.position + (speed * m_TimePreemptiveShot);
                else
                {
                    int i = UnityEngine.Random.Range(0, m_PointPatrol.Length);
                    //
                    if (m_PointPatrol[i] != null)
                    {
                        // внутри зоны патрулирования
                        bool isInsidePatrolZone = (m_PointPatrol[i].transform.position - transform.position).sqrMagnitude < m_PointPatrol[i].Radius * m_PointPatrol[i].Radius;

                        if (isInsidePatrolZone == true)
                        {
                            if (m_RandomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = (UnityEngine.Random.onUnitSphere * m_PointPatrol[i].Radius) + m_PointPatrol[i].transform.position;
                                m_MovePosition = newPoint;
                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            m_MovePosition = m_PointPatrol[i].transform.position;
                        }
                    }
                }
            }
        }

        // дефолтный уклонятор вправо от препятствия
        private void ActionEvadeCollision()
        {
            if(Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
            {
                m_MovePosition = transform.position + (transform.right * 100.0f);
            }
        }


        #region Timer

        // инициализация таймера
        private void InitTimer ()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
        }

        // обновление таймера
        private void UpdateTimer()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }
        #endregion

        public void SetPatrolBehaviour(AIPointerPatrol[] point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PointPatrol = point;
        }

    }
}


