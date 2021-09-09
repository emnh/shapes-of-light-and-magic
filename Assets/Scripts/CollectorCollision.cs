using UnityEngine;

namespace Assets.Scripts
{
    public class CollectorCollision : MonoBehaviour
    {
        private Simulator Simulator;

        // Start is called before the first frame update
        void Start()
        {
            Simulator = GameObject.FindObjectOfType<Simulator>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collision: " + collision.gameObject.name);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger: " + collision.gameObject.name);

            if (collision.gameObject.CompareTag("Diamond"))
            {
                Debug.Log("Collecting");
                Simulator.Collect(this.gameObject, collision.gameObject);
            }
        }
    }
}
