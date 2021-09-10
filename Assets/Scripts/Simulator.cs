using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts
{
    public class Simulator : MonoBehaviour
    {
        public Light2D MainLight;

        public Volume MainVolume;

        public float DayNightCycleLightChangeTime = 2.0f;

        public GameObject MainBase;

        public TextMeshProUGUI FirestoneCount;

        public TextMeshProUGUI WaveCountdown;

        public float UntilNextWave = 30.0f;

        public int MapSizeX = 100;
        public int MapSizeY = 50;

        public bool NightTime = false;

        private int _firestoneCount = 250;
        private float _lastWaveEvent = 0.0f;


        public List<MovingCollected> MovingToBase = new List<MovingCollected>();

        public void Collect(GameObject collector, GameObject collected)
        {
            if (MainBase == null)
            {
                return;
            }
            var newMoving = new MovingCollected()
            {
                Moving = Instantiate(collected, MainBase.transform),
                Source =  collector,
                Destination = MainBase
            };
            Destroy(newMoving.Moving.gameObject.GetComponent<BoxCollider2D>());
            Destroy(newMoving.Moving.gameObject.GetComponent<Rigidbody2D>());
            newMoving.Moving.transform.position = newMoving.Source.transform.position;
            newMoving.Moving.transform.localScale = new Vector3(0.35f, 0.35f, 1.0f);
            newMoving.Moving.transform.rotation = Quaternion.Euler(Random.Range(0.0f, 360.0f) * Vector3.forward);
            MovingToBase.Add(newMoving);
        }

        public static Vector3 Snap(Vector3 v)
        {
            v.x = Mathf.Floor(v.x) + 0.5f;
            v.y = Mathf.Floor(v.y) + 0.5f;
            return v;
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        public bool TryToBuy(int cost)
        {
            if (_firestoneCount >= cost)
            {
                _firestoneCount -= cost;
                return true;
            }
            return false;
        }
        

        // Update is called once per frame
        void Update()
        {
            var title = NightTime ? "Until morning: " : "Next wave: ";
            WaveCountdown.text = title + ((int) (UntilNextWave - (Time.time - _lastWaveEvent))).ToString();
            if (Time.time - _lastWaveEvent >= UntilNextWave)
            {
                NightTime = !NightTime;
                _lastWaveEvent = Time.time;
            }

            if (Time.time - _lastWaveEvent <= DayNightCycleLightChangeTime & _lastWaveEvent > 0.0f)
            {
                var transitionTime = 1.0f - (DayNightCycleLightChangeTime - (Time.time - _lastWaveEvent)) / DayNightCycleLightChangeTime;
                //Debug.Log("Transition time: " + transitionTime);
                var dayIntensity = 1.0f;
                var nightIntensity = 0.0f;
                var start = NightTime ? dayIntensity : nightIntensity;
                var end = NightTime ? nightIntensity : dayIntensity;
                //MainLight.intensity = Mathf.Lerp(start, end, transitionTime);
                if (MainVolume.profile.TryGet<ColorAdjustments>(out var colorAdjustments))
                {
                    colorAdjustments.postExposure.value = Mathf.Lerp(start, end, transitionTime);
                }
            }

            var newList = new List<MovingCollected>();
            foreach (var obj in MovingToBase)
            {
                if (!obj.MoveAlong(Time.deltaTime))
                {
                    newList.Add(obj);
                }
                else
                {
                    _firestoneCount++;
                }
            }
            MovingToBase = newList;

            FirestoneCount.text = _firestoneCount.ToString();
        }
    }
}
