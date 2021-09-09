using UnityEngine;

namespace Assets.Scripts
{
    public class ShotBehaviour : MonoBehaviour
    {
        public GameObject Target;

        public float Speed = 1000.0f;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Target != null)
            {
                Vector3 v = Target.transform.position - transform.position;
                transform.position += Speed * v.normalized * Time.deltaTime;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        //void OnCollisionEnter(Collider2D collision)
        //{
        //    // TODO: Damage object
        //    if (collision.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}
    }
}
