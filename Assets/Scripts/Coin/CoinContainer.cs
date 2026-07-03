using UnityEngine;
using System.Collections;

namespace InfiniteRunner
{
    public class CoinContainer : MonoBehaviour
    {
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private Transform[] _coinRowTransforms; 
        [SerializeField] private Transform[] _coinArchTransforms; 
        [SerializeField] private float _coinsLifetime; 

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            int type = Random.Range(0, 2);

            switch (type)
            {
                case 0:     //coin row
                    InitCoinRow();
                    break;
                case 1:     //coin arch
                    InitCoinArch();
                    break;
            }
        }

        private void InitCoinRow()
        {
            for(int i = 0; i < _coinRowTransforms.Length; i++)
            {
                GameObject coin = Instantiate(_coinPrefab, _coinRowTransforms[i].position, Quaternion.identity);
                coin.transform.parent = gameObject.transform;

                Destroy(coin, _coinsLifetime);
            }
        }

        private void InitCoinArch()
        {
            for(int i = 0; i < _coinArchTransforms.Length; i++)
            {
                GameObject coin = Instantiate(_coinPrefab, _coinArchTransforms[i].position, Quaternion.identity);
                coin.transform.parent = gameObject.transform;

                Destroy(coin, _coinsLifetime);
            }
        }

        private void Update() {
            if (PlayerStates.Instance._isGameOver) {
                return;
            }

            float speed = GameManager.Instance.Speed;
            transform.Translate(Vector3.back * (speed * Time.deltaTime), Space.World);
        }
    }
}
