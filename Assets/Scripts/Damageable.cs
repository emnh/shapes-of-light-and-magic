using System.Security.Cryptography;
using UnityEngine;

namespace Assets.Scripts
{
    public class Damageable : MonoBehaviour
    {
        public GameObject Explosion;
        public int Health = 100;
        public int MaxHealth = 100;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collision: " + collision.gameObject.name);
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            var shotBehaviour = collider.gameObject.GetComponent<ShotBehaviour>();
            if (shotBehaviour != null && shotBehaviour.Target == this.gameObject)
            {
                Health -= 10;
                Debug.Log("Health");
                Destroy(collider.gameObject);
                if (Health <= 0.0)
                {
                    var exp = Instantiate(Explosion, transform.parent);
                    exp.transform.position = transform.position;
                    exp.transform.localScale = transform.localScale;
                    Destroy(exp, 2.0f);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
