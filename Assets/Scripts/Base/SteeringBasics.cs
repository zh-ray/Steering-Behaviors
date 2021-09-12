using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviors
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SteeringBasics : MonoBehaviour
    {
        [Header("General")]
        // 最大速率
        public float maxSpeed = 3.5f;
        // 最大加速度
        public float maxAcceleration = 10f;
        // 转向速度
        public float turnSpeed = 20f;

        [Header("Arrive")]
        // 达到区域的半径
        public float targetRadius = 0.005f;

        // 减速区域半径
        public float slowRadius = 1f;
        // 到达期望速度所需时间
        public float timeToExpect = 0.1f;

        private Rigidbody2D rb;
        private Queue<Vector2> velocitySamples = new Queue<Vector2>();

        [Header("Look Direction Smoothing")]
        public bool smoothing = true;
        public int numSamplesForSmoothing = 5;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // 前往目标位置的加速度
        public Vector3 Arrive(Vector3 targetPosition)
        {
            Debug.DrawLine(transform.position, targetPosition, Color.cyan, 0f, false);

            // 得到速度
            Vector3 targetVelocity = targetPosition - transform.position;
            targetVelocity.z = 0;

            float dist = targetVelocity.magnitude;

            // 判断是否已经到达指定区域内
            if (dist < targetRadius)
            {
                rb.velocity = Vector2.zero;
                return Vector2.zero;
            }

            // 计算速率，减速或全速
            float targetSpeed;
            if (dist > slowRadius)
            {
                targetSpeed = maxSpeed;
            }
            else
            {
                targetSpeed = maxSpeed * (dist / slowRadius);
            }

            // 实际速度
            targetVelocity.Normalize();
            targetVelocity *= targetSpeed;

            // 计算线性加速度 (下一帧速度 - 当前帧速度) / 期望时间
            Vector3 acceleration = targetVelocity - new Vector3(rb.velocity.x, rb.velocity.y, 0);
            acceleration *= 1 / timeToExpect;

            // 限制加速度
            if (acceleration.magnitude > maxAcceleration)
            {
                acceleration.Normalize();
                acceleration *= maxAcceleration;
            }

            return acceleration;
        }

        public Vector3 Seek(Vector3 targetPosition)
        {
            Vector3 acceleration = targetPosition - transform.position;
            acceleration.z = 0;

            acceleration.Normalize();

            acceleration *= maxAcceleration;

            return acceleration;
        }

        public void Steer(Vector3 linearAcceleration)
        {
            Vector3 temp = rb.velocity;
            temp += linearAcceleration * Time.deltaTime;
            rb.velocity = temp;

            //限制速度
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }

        public void LookWhereGoing()
        {
            Vector2 direction = rb.velocity;

            if (smoothing)
            {
                if (velocitySamples.Count == numSamplesForSmoothing)
                {
                    velocitySamples.Dequeue();
                }

                velocitySamples.Enqueue(rb.velocity);
                direction = Vector2.zero;

                foreach (Vector2 v in velocitySamples)
                {
                    direction += v;
                }

                direction /= velocitySamples.Count;
            }

            LookAtDirection(direction);
        }

        public void LookAtDirection(Vector2 direction)
        {
            direction.Normalize();

            // 如果方向非零，则直接向其看齐
            if (direction.sqrMagnitude > 0.001f)
            {
                float toRotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                float rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, toRotation, Time.deltaTime * turnSpeed);
                transform.rotation = Quaternion.Euler(0, 0, rotation);
            }
        }
    }
}
