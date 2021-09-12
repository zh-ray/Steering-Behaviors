using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringBehaviors
{
    [RequireComponent(typeof(SteeringBasics))]
    public class Wander1 : MonoBehaviour
    {
        // 偏移量
        public float wanderOffset = 1.5f;

        // 半径
        public float wanderRadius = 4;

        // 比率
        public float wanderRate = 0.4f;

        private float wanderOrientation = 0;

        private SteeringBasics steeringBasics;

        private Rigidbody2D rb;

        void Awake()
        {
            steeringBasics = GetComponent<SteeringBasics>();
            rb = GetComponent<Rigidbody2D>();
        }
        void FixedUpdate()
        {
            Vector3 accel = GetSteering();
            steeringBasics.Steer(accel);
            steeringBasics.LookWhereGoing();
        }

        public Vector3 GetSteering()
        {
            // 当前物体的方向（弧度）
            float characterOrientation = rb.rotation * Mathf.Deg2Rad;

            // 随机获得一个漫游方向（弧度）
            wanderOrientation += RandomBinomial() * wanderRate;

            // 获得目标方向（弧度）
            float targetOrientation = wanderOrientation + characterOrientation;

            // 当前物体的前方（偏移量）
            Vector3 targetPosition = transform.position + (OrientationToVector(characterOrientation) * wanderOffset);

            // 获得目标位置，c = a + b，即对角线方向
            targetPosition = targetPosition + (OrientationToVector(targetOrientation) * wanderRadius);

            Debug.DrawLine(transform.position, targetPosition, Color.red, 0f, false);

            return steeringBasics.Seek(targetPosition);
        }

        // 获得-1~1之间的随机数
        private float RandomBinomial()
        {
            return Random.value - Random.value;
        }

        // 获得该角度下的单位向量
        private Vector3 OrientationToVector(float orientation)
        {
            return new Vector3(Mathf.Cos(orientation), Mathf.Sin(orientation), 0);
        }
    }
}
