using UnityEngine;

namespace AI.Steering
{
    public class Pursuit : Steering
    {
        private Vector3 tempPoint;
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(tempPoint, 0.2f);
        }

        public override Vector3 GetForce()
        {
            var toTarget = target.position - transform.position;
            var angle = Vector3.Angle(target.forward, toTarget);
            if(angle > 20 && angle < 160)
            {
                var targetSpeed = target.GetComponent<Vehicle>().currentForce.magnitude;
                var time = toTarget.magnitude / (targetSpeed + vehicle.currentForce.magnitude);

                var runDistance = targetSpeed * time;

                var intercepPoint = target.position + target.forward * runDistance;
                tempPoint = intercepPoint;

                expectForce = (intercepPoint - transform.position).normalized * speed;
            }
            else
            {
                expectForce = toTarget.normalized * speed;
            }

            return (expectForce - vehicle.currentForce) * weight;
        }
    }
}
