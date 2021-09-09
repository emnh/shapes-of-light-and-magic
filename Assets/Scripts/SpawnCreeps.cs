using UnityEngine;

namespace Assets.Scripts
{
    public class SpawnCreeps : MonoBehaviour
    {
        public Simulator Simulator;

        public GameObject CreepPrefab;

        public float Interval = 1.0f;

        public int CreepCount = 0;

        public int MaxCreepCount = 100;

        private float _elapsed = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!Simulator.Nighttime)
            {
                CreepCount = 0;
                return;
            }

            _elapsed += Time.fixedDeltaTime;
            if (_elapsed >= Interval)
            {
                _elapsed -= Time.fixedDeltaTime;

                CreepCount++;

                if (CreepCount >= MaxCreepCount)
                {
                    return;
                }

                var edge = Random.Range(0, 4);
                var obj = Instantiate(CreepPrefab, transform);
                switch (edge)
                {
                    case 0:
                        obj.transform.position = Vector3.up * Simulator.MapSizeY + Vector3.left * Random.Range(-Simulator.MapSizeX, Simulator.MapSizeX);
                        break;
                    case 1:
                        obj.transform.position = Vector3.down * Simulator.MapSizeY + Vector3.left * Random.Range(-Simulator.MapSizeX, Simulator.MapSizeX);
                        break;
                    case 2:
                        obj.transform.position = Vector3.left * Simulator.MapSizeX + Vector3.up * Random.Range(-Simulator.MapSizeY, Simulator.MapSizeY);
                        break;
                    case 3:
                        obj.transform.position = Vector3.right * Simulator.MapSizeX + Vector3.up * Random.Range(-Simulator.MapSizeY, Simulator.MapSizeY);
                        break;
                }
                //Debug.Log("Spawned creep at: " + obj.transform.position);
            }
        }
    }
}
