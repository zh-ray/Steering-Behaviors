using UnityEngine;

namespace AI.Steering
{
    public class Seek : Steering
    {
        public override Vector3 GetForce()
        {
            if (target == null)
            {
                return Vector3.zero;
            }
            var expectForce = (target.position - transform.position).normalized * speed;
            var realForce = (expectForce - vehicle.currentForce) * weight;
            return realForce;
        }
    }
}
