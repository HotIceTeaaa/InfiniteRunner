using UnityEngine;
using UnityEngine.UI;

namespace InfiniteRunner
{
    public class MainMenuUIManager : MonoBehaviour
    {
        public static MainMenuUIManager Instance { get; private set; }

        [SerializeField] private GameObject _tutorialPanel;
        [SerializeField] private Upgrades _upgradeScript;

        [Header("ShieldLevelImageUI")]
        [SerializeField] private Image _level2ShieldImage;
        [SerializeField] private Image _level3ShieldImage;

        [Header("ScoreMultiplierLevelImageUI")]
        [SerializeField] private Image _level2ScoreMultiplierImage;
        [SerializeField] private Image _level3ScoreMultiplierImage;

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
            }
        }

        public void ScoreMultiplierUpgradeButtonPressed()
        {
            bool res = _upgradeScript.UpgradeScoreMultiplier();

            if (res)
            {
                HandleScoreMultiplierLevelUI();
            }
        }

        private void HandleShieldLevelUI()
        {
            int level = PlayerPreferences.Instance.getInt("shieldLevel", 1);

            if(level >= 2)
            {
                _level2ShieldImage.enabled = true;
            }

            if(level >= 3)
            {
                _level3ShieldImage.enabled = true;
            }
        }

        private void HandleScoreMultiplierLevelUI()
        {
            int level = PlayerPreferences.Instance.getInt("scoreMultiplierLevel", 1);

            if(level >= 2)
            {
                _level2ScoreMultiplierImage.enabled = true;
            }

            if(level >= 3)
            {
                _level3ScoreMultiplierImage.enabled = true;
            }
        }
    }

}
