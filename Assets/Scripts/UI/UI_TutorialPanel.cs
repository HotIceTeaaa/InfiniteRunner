using UnityEngine;
using UnityEngine.UI;

namespace InfiniteRunner {
    public class UI_TutorialPanel : MonoBehaviour 
    {
        [SerializeField] private Button _exitButton;

        public void Initialize() 
        {
            MainMenuUIManager.Instance.AssignTutorialPanel(gameObject);

            _exitButton.onClick.AddListener(MainMenuUIManager.Instance.CloseTutorialPanel);
            gameObject.SetActive(false);
        }
    }

}
