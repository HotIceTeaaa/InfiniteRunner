using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteRunner
{
    public class MainMenuUIManager : MonoBehaviour
    {
        public static MainMenuUIManager Instance { get; private set; }

        [SerializeField] private GameObject _tutorialPanel;
        [SerializeField] private TMP_Text _coinAmountText;

        private void Awake() {
            // Enforce the Singleton pattern
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void UpdateCoinCount()
        {
            _coinAmountText.text = $"{PlayerPreferences.Instance.getInt("coins", -9999)}";
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
            BGMManager.Instance.PlayGameplayBGM();
            SceneHandler.Instance.LoadNextScene();
        }
    }

}
