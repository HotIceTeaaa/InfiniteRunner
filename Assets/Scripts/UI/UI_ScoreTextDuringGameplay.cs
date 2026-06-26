using TMPro;
using UnityEngine;

namespace InfiniteRunner {
    public class UI_ScoreTextDuringGameplay : MonoBehaviour {
        [SerializeField] private TMP_Text _scoreText;

        private void Update() {
            _scoreText.text = $"{GameManager.Instance.Score}";
        }
    }
}
