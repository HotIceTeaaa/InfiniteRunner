using UnityEngine;

namespace InfiniteRunner
{
    public class PlayerMainMenuAnimation : MonoBehaviour
    {   
        [SerializeField] private Animator _animator;
        [SerializeField] private float _changeAnimRate;
        private float _currentTimer;

        void Start()
        {
            ResetTimer();
        }

        void Update()
        {
            DecrementTimer();

            if( _currentTimer < 0) {
                ChangeAnim();
                ResetTimer();
            }
        }

        private void ResetTimer() {
            _currentTimer = _changeAnimRate;
        }

        private void DecrementTimer() {
            _currentTimer -= Time.deltaTime;
        }

        private void ChangeAnim()
        {
            int n = Random.Range(0, 3);

            switch (n)
            {
                case 0:
                    _animator.SetTrigger("LookAtNailTrigger");
                    break;
                case 1:
                    _animator.SetTrigger("ShooTrigger");
                    break;
                case 2:
                    _animator.SetTrigger("StretchTrigger");
                    break;
            }
        }
    }

}
