using UnityEngine;
using System.Collections;

namespace InfiniteRunner
{
    public class CoinContainer : MonoBehaviour
    {
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
                GameObject coin = PoolManager.Instance.GetAndSetPositionRotation(PoolType.Coin, _coinRowTransforms[i].position, Quaternion.identity, gameObject);

                StartCoroutine(ReturnAfter(PoolType.Coin, coin, _coinsLifetime));
            }
        }

        private void InitCoinArch()
        {
            for(int i = 0; i < _coinArchTransforms.Length; i++)
            {
                GameObject coin = PoolManager.Instance.GetAndSetPositionRotation(PoolType.Coin, _coinArchTransforms[i].position, Quaternion.identity, gameObject);

                StartCoroutine(ReturnAfter(PoolType.Coin, coin, _coinsLifetime));
            }
        }

        private IEnumerator ReturnAfter(PoolType type, GameObject obj, float lifespan) {
            yield return new WaitForSeconds(lifespan);

            if (obj.activeSelf) {
                PoolManager.Instance.Return(type, obj);
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
