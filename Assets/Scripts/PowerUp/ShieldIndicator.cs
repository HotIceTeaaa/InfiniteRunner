using UnityEngine;
using UnityEngine.UI;

namespace InfiniteRunner
{
    public class ShieldIndicator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _shieldImage;

        private void OnEnable() {
            EventManagers.Instance.OnShieldCollect += ShowIndicator;
            EventManagers.Instance.OnShieldLoss += HideIndicator;
        }

        private void OnDisable() {
            EventManagers.Instance.OnShieldCollect -= ShowIndicator;
            EventManagers.Instance.OnShieldLoss -= HideIndicator;
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
            Color color = _shieldImage.color;
            color.a = PlayerStates.Instance._shieldDurationLeft / PlayerStates.Instance._shieldDuration;
            _shieldImage.color = color;
        }
    }
}
