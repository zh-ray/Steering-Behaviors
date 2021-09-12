using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviors
{
    [RequireComponent(typeof(SteeringBasics))]
    public class FollowPath : MonoBehaviour
    {
        public float stopRadius = 0.0005f;
        public float pathOffset = 0.71f;
        public float pathDirection = 1f;

        SteeringBasics steeringBasics;
        Rigidbody2D rb;

        void Awake()
        {
            steeringBasics = GetComponent<SteeringBasics>();
            rb = GetComponent<Rigidbody2D>();
        }

        public Vector3 GetSteering()
        {
            return Vector3.zero;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
