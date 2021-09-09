using UnityEngine;

namespace Assets.Scripts
{
    public class MenuFollowCamera : MonoBehaviour
    {
        public Transform CameraTransform;
        private Vector3 Offset;

        // Start is called before the first frame update
        void Start()
        {
            Offset = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            var ctpos = CameraTransform.position;
            ctpos.z = 0.0f;
            transform.position = ctpos + Offset;
        }
    }
}
