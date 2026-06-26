using TMPro;
using UnityEngine;

namespace InfiniteRunner {
    public class UI_CoinTextDuringGameplay : MonoBehaviour {
        [SerializeField] private TMP_Text _coinText;

        private void Update() {
            _coinText.text = $"{GameManager.Instance.coinsCollectedThisRound}";
        }
    }
}
