using UnityEngine;

namespace TowerDef
{
    /// <summary>
    /// The base script of an entity, required for inheritance by other scripts.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        // object name for user
        [SerializeField] private string m_Nickname;
        public string IsNickname => m_Nickname;
    }
}