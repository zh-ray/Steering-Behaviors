using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviors
{
    [RequireComponent(typeof(SteeringBasics))]
    public class Seek : MonoBehaviour
    {
        public Transform target;
        SteeringBasics steeringBasics;
        // Start is called before the first frame update
        void Start()
        {
            steeringBasics = GetComponent<SteeringBasics>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 accel = steeringBasics.Seek(target.position);

            steeringBasics.Steer(accel);
            steeringBasics.LookWhereGoing();
        }
    }
}
