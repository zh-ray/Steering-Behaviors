using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviors
{
    [RequireComponent(typeof(SteeringBasics))]
    public class Pursuit : MonoBehaviour
    {
        // 预测的时长
        public float maxPrediction = 1f;
        public Rigidbody2D target;

        Rigidbody2D rb;
        SteeringBasics steeringBasics;
        // Start is called before the first frame update
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            steeringBasics = GetComponent<SteeringBasics>();
        }

        public Vector3 GetSteering(Rigidbody2D target)
        {
            // 两者的距离
            Vector3 displacement = (Vector3)target.position - transform.position;
            float distance = displacement.magnitude;

            // 当前的速率
            float speed = rb.velocity.magnitude;

            // 计算预测时间，避免预测的距离大于当前距离
            float prediction;
            if(speed <= distance/maxPrediction)
            {
                prediction = maxPrediction;
            }
            else
            {
                prediction = distance / speed;
            }

            // 获得预测相遇地点
            Vector3 explicitTarget = target.position + target.velocity * prediction;

            return steeringBasics.Seek(explicitTarget);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 accel = GetSteering(target);

            steeringBasics.Steer(accel);
            steeringBasics.LookWhereGoing();
        }
    }
}
