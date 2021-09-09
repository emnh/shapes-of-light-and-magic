using UnityEngine;

namespace Assets.Scripts
{
    public class CreepBehaviour : MonoBehaviour
    {
        public GameObject ShotPrefab;

        public float Speed = 10.0f;

        public float ShootDistance = 8.0f;

        public float ShootInterval = 1.0f;
        
        private GameObject _mainBase;

        private float _lastShot = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            _mainBase = GameObject.Find("Main Base");
        }

        bool Shoot()
        {
            var pos = new Vector2(transform.position.x, transform.position.y);
            var potentialTargets = Physics2D.OverlapCircleAll(pos, ShootDistance, LayerMask.GetMask("Buildings"));
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
                return false;
            }

            Vector3 v2 = target.transform.position - transform.position;
            if (v2.magnitude <= ShootDistance)
            {
                if (Time.time - _lastShot >= ShootInterval)
                {
                    var obj = Instantiate(ShotPrefab, transform);
                    var shotBehaviour = obj.GetComponent<ShotBehaviour>();
                    shotBehaviour.Target = target;
                    _lastShot = Time.time;
                }
                return true;
            }

            return false;
        }

        // Update is called once per frame
        void Update()
        {
            if (_mainBase == null)
            {
                return;
            }

            Vector3 v = _mainBase.transform.position - transform.position;

            if (!Shoot())
            {
                transform.position += Speed * v.normalized * Time.deltaTime;
            }
        }
    }
}
