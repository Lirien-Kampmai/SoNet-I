using UnityEngine;

namespace TowerDef
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        public int goldCoast = 15;
        public Sprite HUDSprite;
        public Sprite TowerSprite;
        public TurretProperties Projectile;

    }
}
