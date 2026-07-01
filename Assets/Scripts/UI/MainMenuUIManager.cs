using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteRunner
{
    public class MainMenuUIManager : MonoBehaviour
    {
        public static MainMenuUIManager Instance { get; private set; }

        [SerializeField] private GameObject _tutorialPanel;
        [SerializeField] private Upgrades _upgradeScript;

        [Header("ShieldLevelUI")]
        [SerializeField] private Image _level2ShieldImage;
        [SerializeField] private Image _level3ShieldImage;
        [SerializeField] private GameObject _shieldUpgrade;
        [SerializeField] private GameObject _maxShield;

        [Header("ScoreMultiplierLevelUI")]
        [SerializeField] private Image _level2ScoreMultiplierImage;
        [SerializeField] private Image _level3ScoreMultiplierImage;
        [SerializeField] private GameObject _scoreMultiplierUpgrade;
        [SerializeField] private GameObject _maxScoreMultiplier;

        private void Awake() {
            // Enforce the Singleton pattern
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            HandleShieldLevelUI();
            HandleScoreMultiplierLevelUI();
        }

        public void AssignTutorialPanel(GameObject newTutorialPanel) {
            _tutorialPanel = newTutorialPanel;
        }

        public void OpenTutorialPanel()
        {
            _tutorialPanel.SetActive(true);
        }

        public void CloseTutorialPanel()
        {
            _tutorialPanel.SetActive(false);
        }

        public void PlayButtonPressed()
        {
            SceneHandler.Instance.LoadNextScene();
        }

        public void ShieldUpgradeButtonPressed()
        {
            bool res = _upgradeScript.UpgradeShield();

            if (res)
            {
                HandleShieldLevelUI();
                SFXManager.Instance.PlayBoughtUpgradeSFX();
            }
            else
            {
                SFXManager.Instance.PlayBuyErrorSFX();
            }
        }

        public void ScoreMultiplierUpgradeButtonPressed()
        {
            bool res = _upgradeScript.UpgradeScoreMultiplier();

            if (res)
            {
                HandleScoreMultiplierLevelUI();
                SFXManager.Instance.PlayBoughtUpgradeSFX();
            }
            else
            {
                SFXManager.Instance.PlayBuyErrorSFX();
            }
        }

        private void HandleShieldLevelUI()
        {
            int level = PlayerPreferences.Instance.getInt("shieldLevel", 1);

            if(level >= 2)
            {
                _level2ShieldImage.enabled = true;

                //ganti opacitynya
                Color color = _level2ShieldImage.color;
                color.a = 1;
                _level2ShieldImage.color = color;
            }

            if(level >= 3)
            {
                _level3ShieldImage.enabled = true;

                //ganti opacitynya
                Color color = _level3ShieldImage.color;
                color.a = 1;
                _level3ShieldImage.color = color;

                //ganti tombol jadi image MAX
                _shieldUpgrade.SetActive(false);
                _maxShield.SetActive(true);
            }
        }

        private void HandleScoreMultiplierLevelUI()
        {
            int level = PlayerPreferences.Instance.getInt("scoreMultiplierLevel", 1);

            if(level >= 2)
            {
                _level2ScoreMultiplierImage.enabled = true;

                //ganti opacitynya
                Color color = _level2ScoreMultiplierImage.color;
                color.a = 1;
                _level2ScoreMultiplierImage.color = color;
            }

            if(level >= 3)
            {
                _level3ScoreMultiplierImage.enabled = true;

                //ganti opacitynya
                Color color = _level3ScoreMultiplierImage.color;
                color.a = 1;
                _level3ScoreMultiplierImage.color = color;

                //ganti tombol jadi image MAX
                _scoreMultiplierUpgrade.SetActive(false);
                _maxScoreMultiplier.SetActive(true);
            }
        }

        public void DelegateButtonSFXToManager()
        {
            SFXManager.Instance.PlayClickHoverSFX();
        }
    }

}
