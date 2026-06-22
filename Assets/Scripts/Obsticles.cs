using UnityEngine;

namespace InfiniteRunner
{
    public class Obsticles : MonoBehaviour
    {
        private void Update(){
            if (GameManager.Instance.IsGameOver)
            {
                return;
            }

            float speed = GameManager.Instance.Speed;
            transform.Translate(Vector3.back * (speed * Time.deltaTime), Space.World);
        }
    }

}
