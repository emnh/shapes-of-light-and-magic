using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts
{
    public class Pulsate : MonoBehaviour
    {
        public float intensity = 100.0f;
        public float speed = 2.0f;

        private Bloom bloom;

        // Start is called before the first frame update
        void Start()
        {
            Volume volume = GetComponent<Volume>();
            if (volume.profile.TryGet<Bloom>(out var bloomTmp))
            {
                bloom = bloomTmp;
            }
        }

        // Update is called once per frame
        void Update()
        {
            bloom.intensity.value = intensity + 0.5f * intensity * Mathf.Sin(speed * Mathf.PI * Time.time);
        }
    }
}
