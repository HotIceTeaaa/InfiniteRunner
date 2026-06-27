using TMPro;
using UnityEngine;

namespace InfiniteRunner {
    public class UI_CoinText : MonoBehaviour {
        [SerializeField] private TMP_Text _coinAmountText;

        private void Start() {
            _coinAmountText.text = $"{PlayerPreferences.Instance.getInt("coins", 0)}";
        }
    
    }

}
