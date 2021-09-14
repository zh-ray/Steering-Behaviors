using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SteeringBehaviors
{
    [Serializable]
    public class LinePath
    {
        public Vector3[] nodes;

        [NonSerialized]
        public float maxDist;
        [NonSerialized]
        public float[] distances;

        public Vector3 this[int i]
        {
            get
            {
                return nodes[i];
            }

            set
            {
                nodes[i] = value;
            }
        }

        public int Length
        {
            get
            {
                return nodes.Length;
            }
        }

        public Vector3 EndNode
        {
            get
            {
                return nodes[nodes.Length - 1];
            }
        }

        public LinePath(Vector3[] nodes)
        {
            this.nodes = nodes;
            CalcDistances();
        }

        public void CalcDistances()
        {
            distances = new float[nodes.Length];
            distances[0] = 0;

            for (var i = 1; i < nodes.Length; i++)
            {
                distances[i] = distances[i - 1] + Vector3.Distance(nodes[i - 1], nodes[i]);
            }

            maxDist = distances[distances.Length - 1];
        }

        public void Draw()
        {
            for (int i = 0; i < nodes.Length - 1; i++)
            {
                Debug.DrawLine(nodes[i], nodes[i + 1], Color.cyan, 0.0f, false);
            }
        }

        public float GetParam(Vector3 position, Rigidbody2D rb)
        {
            int closestSegment = GetClosestSegment(position);

            float param = this.distances[closestSegment] + GetParamFromSegment(position, nodes[closestSegment], nodes[closestSegment + 1], rb);

            return param;
        }

        public int GetClosestSegment(Vector3 position)
        {
            float closestDist = DistToSegment(position, nodes[0], nodes[1]);
            int closestSegment = 0;

            for (int i = 1; i < nodes.Length - 1; i++)
            {
                float dist = DistToSegment(position, nodes[i], nodes[i + 1]);

                if(dist <= closestDist)
                {
                    closestDist = dist;
                    closestSegment = i;
                }
            }
            return closestSegment;
        }


    }
}
