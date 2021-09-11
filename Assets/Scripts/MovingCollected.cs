using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    // TODO: Make it into a component?
    public class MovingCollected
    {
        public ResourceType ResourceType;
        public GameObject Moving;
        public GameObject Source;
        public GameObject Destination;
        public float elapsed = 0.0f;

        public bool MoveAlong(float delta)
        {
            if (Moving == null || Source == null || Destination == null)
            {
                return false;
            }
            var from = new Vector2(Source.transform.position.x, Source.transform.position.y);
            var to = new Vector2(Destination.transform.position.x, Destination.transform.position.y);
            var vx = to.x - from.x;
            var vy = to.y - from.y;
            var v = new Vector2(vx, vy);
            elapsed += delta;
            v *= elapsed;
            Moving.transform.position = Source.transform.position + new Vector3(v.x, v.y, 0.0f);
            var diffToDest = new Vector2(Moving.transform.position.x - Destination.transform.position.x, Moving.transform.position.y - Destination.transform.position.y);
            var dist = diffToDest.magnitude;
            if (dist <= 1.0f || elapsed >= 1.0f)
            {
                GameObject.Destroy(Moving);
                return true;
            }
            return false;
        }

    }
}
