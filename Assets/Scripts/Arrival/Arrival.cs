using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviors
{
    [RequireComponent(typeof(SteeringBasics))]
    public class Arrival : MonoBehaviour
    {
        public Vector3 targetPosition;
        public Transform targetTransform;

        private SteeringBasics steeringBasics;

        private void Start()
        {
            steeringBasics = GetComponent<SteeringBasics>();
        }

        private void FixedUpdate()
        {
            if (targetTransform != null)
            {
                targetPosition = targetTransform.position;
            }
            Vector3 accel = steeringBasics.Arrive(targetPosition);

            steeringBasics.Steer(accel);

            steeringBasics.LookWhereGoing();
        }
    }

}
