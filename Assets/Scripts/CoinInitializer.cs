using InfiniteRunner;
using System.Collections.Generic;
using UnityEngine;

namespace InifiniteRunner {
    public class CoinInitializer : MonoBehaviour {

        [SerializeField] private Transform[] _coinRowCoinsTransformArr = new Transform[5];
        [SerializeField] private Transform[] _coinArchCoinsTransformArr = new Transform[7];

        private Queue<GameObject> _coinGetted = new Queue<GameObject>();

        public void InitCoins(int coinType, GameObject parent) {
            switch (coinType) {
                case 0: //coin row
                    for (int i = 0; i < _coinRowCoinsTransformArr.Length; i++) {
                        GameObject obj = PoolManager.Instance.GetAndSetPositionRotation(PoolType.Coin, _coinRowCoinsTransformArr[i].position, Quaternion.identity, parent);
                        //obj.transform.localPosition = Vector3.zero;
                        _coinGetted.Enqueue(obj);
                    }
                    break;
                case 1: //coin arch
                    for (int i = 0; i < _coinArchCoinsTransformArr.Length; i++) {
                        GameObject obj = PoolManager.Instance.GetAndSetPositionRotation(PoolType.Coin, _coinArchCoinsTransformArr[i].position, Quaternion.identity, parent);
                        //obj.transform.localPosition = Vector3.zero;
                        _coinGetted.Enqueue(obj);
                    }
                    break;
            }
        }
        public void ReturnFirstNCoins(int N) {
            for(int i = 0; i < N; i++) {
                PoolManager.Instance.Return(PoolType.Coin, _coinGetted.Dequeue());
            }
        }
    }

}
