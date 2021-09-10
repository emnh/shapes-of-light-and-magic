using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class SpawnEmitters : MonoBehaviour
    {
        public Simulator Simulator;
        public GameObject FireStoneEmitter;
        public GameObject IceRockEmitter;
        public GameObject PoisonCloudEmitter;
        public int Count = 100;
        public float MinDistanceToOther = 8.0f;
        public List<Vector3> AlreadyPlaced = new List<Vector3>();

        // Start is called before the first frame update
        void Start()
        {
            // Place one near main base so we can get started
            var obj2 = Instantiate(FireStoneEmitter, transform);
            obj2.transform.position = Simulator.Snap(new Vector3(3.0f, 3.0f, 0.0f));
            AlreadyPlaced.Add(obj2.transform.position);

            var emitters = new List<GameObject>()
            {
                FireStoneEmitter, IceRockEmitter, PoisonCloudEmitter
            };

            for (var i = 1; i < Count; i++)
            {
                var emitter = emitters[i % 3];
                var obj = Instantiate(emitter, transform);
                obj.transform.position = Vector3.zero;
                var k = 0;
                var tries = 100;
                var done = false;
                while ((!done || Vector3.Distance(obj.transform.position, Vector3.zero) <= MinDistanceToOther) && k < tries)
                {
                    done = true;
                    obj.transform.position = Simulator.Snap(new Vector3(Random.Range(-Simulator.MapSizeX, Simulator.MapSizeX), Random.Range(-Simulator.MapSizeY, Simulator.MapSizeY), 0.0f));
                    foreach (var placed in AlreadyPlaced)
                    {
                        if (Vector3.Distance(obj.transform.position, placed) <= MinDistanceToOther)
                        {
                            done = false;
                            break;
                        }
                    }
                    k++;
                }
                if (k >= tries)
                {
                    Destroy(obj);
                }
                else
                {
                    AlreadyPlaced.Add(obj.transform.position);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
