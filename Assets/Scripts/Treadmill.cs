using UnityEngine;

namespace InfiniteRunner {
    public class Treadmill : MonoBehaviour
    {
        [SerializeField] private float _tileLength = 20f;
        [SerializeField] private int _tileCount = 4;      // how many tiles are in the loop
        [SerializeField] private float _recycleZ = -20f;  // once past this Z, jump to the front

        [SerializeField] private TileVariantHandler _tileVariantHandler;


        private void Update()
        {
            if (PlayerStates.Instance._isGameOver)
            {
                return;
            }

            float speed = GameManager.Instance.Speed;
            transform.Translate(Vector3.back * (speed * Time.deltaTime), Space.World);

            if (transform.position.z < _recycleZ)
            {
                _tileVariantHandler.CheckTileHandler();

                // Jump exactly one full loop ahead so the strip stays seamless.
                transform.position += Vector3.forward * (_tileLength * _tileCount);

            }
        }
    }
}