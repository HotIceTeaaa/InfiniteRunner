using UnityEngine;

namespace InfiniteRunner {
    public class Upgrades : MonoBehaviour {
        [SerializeField] private int _level2ShieldUpgradeCost;
        [SerializeField] private int _level3ShieldUpgradeCost;

        [SerializeField] private int _level2ScoreMultiplierUpgradeCost;
        [SerializeField] private int _level3ScoreMultiplierUpgradeCost;

        public bool UpgradeShield()
        {
            int level = PlayerPreferences.Instance.getInt("shieldLevel", 1);
            int coins = PlayerPreferences.Instance.getInt("coins", -9999);

            switch (level)
            {
                case 1:
                    if(coins >= _level2ShieldUpgradeCost)
                    {
                        level = 2;
                        PlayerPreferences.Instance.saveInt("shieldLevel", level);
                        coins -= _level2ShieldUpgradeCost;
                        PlayerPreferences.Instance.saveInt("coins", coins);

                        PlayerStates.Instance.InitPowerUpInfo();
                        
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case 2:
                    if(coins >= _level3ShieldUpgradeCost)
                    {
                        level = 3;
                        PlayerPreferences.Instance.saveInt("shieldLevel", level);
                        coins -= _level3ShieldUpgradeCost;
                        PlayerPreferences.Instance.saveInt("coins", coins);

                        PlayerStates.Instance.InitPowerUpInfo();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }

            return false;
        }

        public bool UpgradeScoreMultiplier()
        {
            int level = PlayerPreferences.Instance.getInt("scoreMultiplierLevel", 1);
            int coins = PlayerPreferences.Instance.getInt("coins", -9999);

            switch (level)
            {
                case 1:
                    if(coins >= _level2ScoreMultiplierUpgradeCost)
                    {
                        level = 2;
                        PlayerPreferences.Instance.saveInt("scoreMultiplierLevel", level);
                        coins -= _level2ScoreMultiplierUpgradeCost;
                        PlayerPreferences.Instance.saveInt("coins", coins);

                        PlayerStates.Instance.InitPowerUpInfo();

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case 2:
                    if(coins >= _level3ScoreMultiplierUpgradeCost)
                    {
                        level = 3;
                        PlayerPreferences.Instance.saveInt("scoreMultiplierLevel", level);
                        coins -= _level3ScoreMultiplierUpgradeCost;
                        PlayerPreferences.Instance.saveInt("coins", coins);

                        PlayerStates.Instance.InitPowerUpInfo();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }

            return false;
        }

    }

}
