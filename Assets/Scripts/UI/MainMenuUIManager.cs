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

        private void Awake() {
            // Enforce the Singleton pattern
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
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
            GameManager.Instance.GameStart();
        }
    }

}
