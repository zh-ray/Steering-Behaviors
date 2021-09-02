using UnityEngine;

namespace AI.Steering
{
    public class AILocomotion : Vehicle
    {
        private void Update()
        {
            Rotation();
            Movement();
        }

        public void Rotation()
        {
            if (currentForce != Vector3.zero)
            {
                var dir = Quaternion.LookRotation(currentForce);
                dir.x = 0;
                dir.y = 0;

                transform.rotation = Quaternion.Lerp(transform.rotation, dir, rotationSpeed * Time.deltaTime);
            }
        }

        public void Movement()
        {
            currentForce += finalForce * Time.deltaTime;
            currentForce = Vector3.ClampMagnitude(currentForce, maxSpeed);

            transform.position += currentForce * Time.deltaTime;
        }
    }
}
