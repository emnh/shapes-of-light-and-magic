using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts
{
    public class RocketEngine : MonoBehaviour
    {
        public GameObject Target;

        private Rigidbody2D _rigidbody2D;

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            var target = Target.transform.position;
            var v = target - transform.position;
            v = v.normalized;
            var a = Mathf.Rad2Deg * Mathf.Atan2(v.y, v.x);
            var rotation = _rigidbody2D.rotation;
            var rotationSpeed = 1000.0f;
            var diff = Mathf.DeltaAngle(rotation, a);
            //Debug.Log("a, rotation, diff: " + a + ", " + rotation + ", " + diff);
            //_rigidbody2D.fo
            //_rigidbody2D.angularVelocity = diff * Time.deltaTime;
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(a * Vector3.forward), rotationSpeed * Time.deltaTime);
            if (Math.Abs(diff) > 1.0f)
            {
                _rigidbody2D.rotation += Mathf.Min(Math.Abs(diff), rotationSpeed * Time.fixedDeltaTime) * Math.Sign(diff);
            }
            else
            {
                _rigidbody2D.rotation = a;
            }
            //transform.rotation = Quaternion.Euler(diff * Time.deltaTime * Vector3.forward);
            //_rigidbody2D.rotation = transform.rotation.z;
            //Debug.Log("Transform up: " + transform.up);
            var forward = new Vector2(transform.right.x, transform.right.y);
            //var toTheSide = (Mathf.Abs(diff) / 180.0f * -Mathf.Sign(diff)) * new Vector2(transform.right.x, transform.right.y);
            //var toTheSide = (Mathf.Abs(diff) / 3600.0f * Mathf.Sign(diff)) * new Vector2(transform.right.x, transform.right.y);
            //_rigidbody2D.AddForceAtPosition(1.0f * up, -up);
            var acceleration = 20.0f;
            if (Mathf.Abs(diff) <= 90.0f)
            {
                _rigidbody2D.AddForce(acceleration * forward);
            }
            
        }
    }
}
