using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Spin : MonoBehaviour
    {
        public float speed = 90.0f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.rotation *= Quaternion.Euler(speed * Time.deltaTime * Vector3.forward);
        }
    }

}
