using System;
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
                var fireStone = gameObject.CompareTag("FireStone");
                var iceRock = gameObject.CompareTag("IceRock");
                var poisonCloud = gameObject.CompareTag("PoisonCloud");

                Debug.Log("Particle Collision");
                if (_simulator != null && this.gameObject != null &&
                    (fireStone || iceRock || poisonCloud))
                {
                    Debug.Log("Collecting");
                    var resType = ResourceType.FireStone;
                    if (iceRock)
                    {
                        resType = ResourceType.IceRock;
                    }
                    if (poisonCloud)
                    {
                        resType = ResourceType.PoisonCloud;
                    }
                    _simulator.Collect(resType, other, Prefab);
                }
            }
        }
    }
}
