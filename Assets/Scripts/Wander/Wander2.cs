using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviors
{
    [RequireComponent(typeof(SteeringBasics))]
    public class Wander2 : MonoBehaviour
    {
        public float wanderRadius = 1.2f;

        public float wanderDistance = 2f;

        // 随机的最大位移
        public float wanderJitter = 40f;

        private Vector3 wanderTarget;
        private SteeringBasics steeringBasics;
        private Rigidbody2D rb;

        private void Awake()
        {
            steeringBasics = GetComponent<SteeringBasics>();
            rb = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {
            // 随机一个弧度
            float theta = Random.value * 2 * Mathf.PI;

            wanderTarget = new Vector3(wanderRadius * Mathf.Cos(theta), wanderRadius * Mathf.Sin(theta), 0f);
        }

        // Update is called once per frame
        public Vector3 GetSteering()
        {
            // 一帧的最大转向量
            float jitter = wanderJitter * Time.deltaTime;

            // 每一帧调整目标位置
            wanderTarget += new Vector3(Random.Range(-1f, 1f) * jitter, Random.Range(-1f, 1f) * jitter, 0f);

            // 获得新的
            wanderTarget.Normalize();
            wanderTarget *= wanderRadius;

            Vector3 targetPosition = transform.position + transform.right * wanderDistance + wanderTarget;

            Debug.DrawLine(transform.position, targetPosition, Color.red, 0f, false);

            return steeringBasics.Seek(targetPosition);
        }

        void FixedUpdate()
        {
            Vector3 accel = GetSteering();
            steeringBasics.Steer(accel);
            steeringBasics.LookWhereGoing();
        }
    }
}
