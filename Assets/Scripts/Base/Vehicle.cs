using UnityEngine;

namespace AI.Steering
{
    public class Vehicle : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 currentForce;
        [HideInInspector]
        public Vector3 finalForce;
        [HideInInspector]
        public Steering[] steerings;
        public float rotationSpeed = 5;
        public float maxSpeed = 10;
        public float mass = 1;
        public float computerInternal = 0.2f;
        public bool isPlane = false;

        void Start()
        {
            steerings = GetComponents<Steering>();
        }

        public void ComputeFinalForce()
        {
            finalForce = Vector3.zero;
            for (int i = 0; i < steerings.Length; i++)
            {
                if(steerings[i].enabled)
                {
                    finalForce += steerings[i].GetForce();
                }
            }

            if(finalForce == Vector3.zero)
            {
                currentForce = Vector3.zero;
            }
            if(isPlane)
            {
                finalForce.z = 0;
            }
            finalForce = Vector3.ClampMagnitude(finalForce, maxSpeed);
            finalForce /= mass;
        }
        private void OnEnable()
        {
            InvokeRepeating("ComputeFinalForce", 0, computerInternal);
        }
        private void OnDisable()
        {
            CancelInvoke("ComputeFinalForce");
        }
    }
}
