using TMPro;
using UnityEngine;

namespace InfiniteRunner {
    public class UI_CoinText : MonoBehaviour {
        [SerializeField] private TMP_Text _coinAmountText;

        private void Update() {
            _coinAmountText.text = $"{PlayerPreferences.Instance.getInt("coins", 0)}";
        }
        
        //---------------------------------------- jangan kek gini, ini grgr w males aja
    }

}
