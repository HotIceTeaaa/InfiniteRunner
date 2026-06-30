using UnityEngine;
using System;

namespace InfiniteRunner
{
    public class EventManagers : MonoBehaviour
    {
        public event Action OnShieldCollect;
        public event Action OnShieldLoss;
        public event Action OnScoreMultiplierCollect;
        public event Action OnScoreMultiplierLoss;
        
        public static EventManagers Instance { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void InvokeOnShieldCollect() => OnShieldCollect.Invoke();
        public void InvokeOnShieldLoss() => OnShieldLoss.Invoke();
        public void InvokeOnScoreMultiplierCollect() => OnScoreMultiplierCollect.Invoke();
        public void InvokeOnScoreMultiplierLoss() => OnScoreMultiplierLoss.Invoke();
    }
}
