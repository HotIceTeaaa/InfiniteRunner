using TMPro;
using UnityEngine;

namespace InfiniteRunner {
    public class UI_PlayButtonHighScore : MonoBehaviour {
        [SerializeField] private TMP_Text _playButtonText;

        private void Start() {
            _playButtonText.text = $"Play       High Score: {PlayerPreferences.Instance.getFloat("highScore", -9999)}";
        }

    }

}
