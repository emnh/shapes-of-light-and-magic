using UnityEngine;

// TODO: Deprecated. This script is no longer used. Instead CollectorParticleCollision is used.

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

            var fireStone = collision.gameObject.CompareTag("FireStone");
            var iceRock = collision.gameObject.CompareTag("IceRock");
            var poisonCloud = collision.gameObject.CompareTag("PoisonCloud");

            if (Simulator != null && this.gameObject != null && collision.gameObject != null && 
                (fireStone || iceRock || poisonCloud))
            {
                Debug.Log("Collecting");
                //Simulator.Collect(this.gameObject, collision.gameObject);
            }
        }
    }
}
