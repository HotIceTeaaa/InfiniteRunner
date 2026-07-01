using UnityEngine;
using UnityEngine.UI;

namespace InfiniteRunner
{
    public class ScoreMultiplierIndicator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _scoreMultiplierImage;

        private void OnEnable() {
            EventManagers.Instance.OnScoreMultiplierCollect += ShowIndicator;
            EventManagers.Instance.OnScoreMultiplierLoss += HideIndicator;
        }

        private void OnDisable() {
            EventManagers.Instance.OnScoreMultiplierCollect -= ShowIndicator;
            EventManagers.Instance.OnScoreMultiplierLoss -= HideIndicator;
        }

        private void ShowIndicator()
        {
            _animator.SetTrigger("EnterTrigger");
        }

        private void HideIndicator()
        {
            _animator.SetTrigger("ExitTrigger");
        }

        void Update()
        {
            Color color = _scoreMultiplierImage.color;
            color.a = PlayerStates.Instance._scoreMultiplierDurationLeft / PlayerStates.Instance._scoreMultiplierDuration;
            _scoreMultiplierImage.color = color;
        }
    }
}
