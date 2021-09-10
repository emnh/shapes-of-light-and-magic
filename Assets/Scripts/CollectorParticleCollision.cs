using UnityEngine;

namespace Assets.Scripts
{
    public class CollectorParticleCollision : MonoBehaviour
    {
        public GameObject Prefab;

        private Simulator _simulator;

        // Start is called before the first frame update
        void Start()
        {
            _simulator = GameObject.FindObjectOfType<Simulator>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Collector"))
            {
                Debug.Log("Particle Collision");
                if (_simulator != null && this.gameObject != null &&
                    (this.gameObject.CompareTag("FireStone") || this.gameObject.CompareTag("IceRock") || this.gameObject.CompareTag("PoisonCloud")))
                {
                    Debug.Log("Collecting");
                    _simulator.Collect(other, Prefab);
                }
            }
        }
    }
}
