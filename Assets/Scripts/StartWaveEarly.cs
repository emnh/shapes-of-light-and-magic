using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class StartWaveEarly : MonoBehaviour
    {

        public Button Button;

        public Simulator Simulator;

        // Start is called before the first frame update
        void Start()
        {
            Button.onClick.AddListener(OnMouseDown);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnMouseDown()
        {
            Simulator.StartWave();
        }
    }
}
