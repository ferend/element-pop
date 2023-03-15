using System;
using UnityEngine;

namespace Game.Entity
{
    [ExecuteInEditMode]
    public class Trajectory : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Vector2 or = transform.position;
            Vector2 dir = transform.up * 2F;
            Ray ray = new Ray(or, dir);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(or,or+dir);

            for (int i = 0; i < 3; i++)
            {
                if (Physics.Raycast (ray, out RaycastHit hit))
                {
                    Vector2 reflected = Reflection(ray.direction, hit.normal);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(hit.point, (Vector2)hit.point+reflected);
                    ray.direction = reflected;
                    ray.origin = hit.point;
                }
                else
                {
                    break;
                }
            }
        
        }

        private Vector2 Reflection(Vector2 inDir, Vector2 n)
        {
            float proj = Vector2.Dot(inDir, n);
            return inDir - 2 * proj * n;
        }
    }
}
