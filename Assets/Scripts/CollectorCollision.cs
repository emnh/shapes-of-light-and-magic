using UnityEngine;

namespace Assets.Scripts
{
    public class CollectorCollision : BuildingBehaviour
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
            Debug.Log("Trigger Name: " + collision.name);
            Debug.Log("Trigger GO Name: " + collision.gameObject.name);

            if (Simulator != null && this.gameObject != null && collision.gameObject != null && 
                (collision.gameObject.CompareTag("FireStone") || collision.gameObject.CompareTag("IceRock") || collision.gameObject.CompareTag("PoisonCloud")))
            {
                Debug.Log("Collecting");
                Simulator.Collect(this.gameObject, collision.gameObject);
            }
        }
    }
}
