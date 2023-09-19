using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation
{
    public class RouteBase : MonoBehaviour, IRoute
    {
        [SerializeField]
        private List<Transform> waypoints;

        public Vector3[] Waypoints
        {
            get
            {
                var list = new List<Vector3>();

                foreach (var waypoint in waypoints)
                {
                    if (waypoint != null)
                        list.Add(waypoint.position);
                }
                return list.ToArray();
            }
        }

        private int currentIndex = 0;

        public void SelectNewDestination(int index)
        {
            currentIndex = index;
        }

        public Vector3 GetCurrentDestination()
        {
            return Waypoints[currentIndex];
        }
    }
}
