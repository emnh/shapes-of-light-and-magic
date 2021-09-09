using UnityEngine;

namespace Assets.Scripts
{
    public class TowerBehaviour : MonoBehaviour
    {
        public GameObject ShotPrefab;

        public float ShotSpeed = 100.0f;

        public float ShootDistance = 8.0f;

        public float ShootInterval = 1.0f;

        private float _lastShot = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            var pos = new Vector2(transform.position.x, transform.position.y);
            var potentialTargets = Physics2D.OverlapCircleAll(pos, ShootDistance, LayerMask.GetMask("Creeps"));
            var mindist = ShootDistance;
            GameObject target = null;
            foreach (var obj in potentialTargets)
            {
                var pos2 = new Vector2(obj.gameObject.transform.position.x, obj.gameObject.transform.position.y);
                var d = Vector2.Distance(pos, pos2);
                if (d < mindist)
                {
                    mindist = d;
                    target = obj.gameObject;
                }
            }

            if (target == null)
            {
                return;
            }

            if (Time.time - _lastShot >= ShootInterval)
            {
                var obj = Instantiate(ShotPrefab, transform);
                var shotBehaviour = obj.GetComponent<ShotBehaviour>();
                shotBehaviour.Target = target;
                shotBehaviour.Speed = ShotSpeed;
                _lastShot = Time.time;
            }
        }
    }
}
