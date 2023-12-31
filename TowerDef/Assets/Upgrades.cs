using System.Collections;
using System;
using UnityEngine;

namespace TowerDef
{

    public class Upgrades : SingletonBase<Upgrades>
    {
        public const string filename = "upgrades.dat";

        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset asset;
            public int level = 0;
        }

        [SerializeField] private UpgradeSave[] save;

        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(filename, ref save);
        }

        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach(var upgr in Instance.save)
            {
                if(upgr.asset = asset)
                {
                    upgr.level += 1;
                    Saver<UpgradeSave[]>.Save(filename, Instance.save);
                }
            }

        }

        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            foreach (var upgr in Instance.save)
            {
                if (upgr.asset = asset)
                {
                    return upgr.level;
                }
            }
            return 0;
        }





    }
}