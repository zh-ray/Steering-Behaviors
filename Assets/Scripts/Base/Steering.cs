using UnityEngine;

namespace AI.Steering
{
    public abstract class Steering : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 expectForce;
        [HideInInspector]
        public Vehicle vehicle;
        public Transform target;
        public float speed = 5;
        public int weight = 1;

        private void Start()
        {
            vehicle = GetComponent<Vehicle>();
            if(speed == 0)
            {
                speed = vehicle.maxSpeed;
            }
        }

        abstract public Vector3 GetForce();
    }
}
