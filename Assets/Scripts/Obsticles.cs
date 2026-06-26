using UnityEngine;

namespace InfiniteRunner
{
    public class Obsticles : MonoBehaviour
    {
        private void Update(){
            if (PlayerStates.Instance._isGameOver)
            {
                return;
            }

            float speed = GameManager.Instance.Speed;
            transform.Translate(Vector3.back * (speed * Time.deltaTime), Space.World);
        }
    }

}
