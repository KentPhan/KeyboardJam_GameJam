using UnityEngine;

namespace Assets.Source
{
    public class PlayerComponent : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void OnCollisionEnter2D(Collision2D i_collision)
        {
            if (i_collision.collider.CompareTag("Monster"))
            {
                WordEventManager.Instance.TriggerGameOver();
                Destroy(gameObject);
            }
        }
    }
}
