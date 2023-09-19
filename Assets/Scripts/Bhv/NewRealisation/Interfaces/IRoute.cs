using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation.Interfaces
{
    public interface IRoute
    {
        Vector3[] Waypoints { get; }
        Vector3 GetCurrentDestination();
        void SelectNewDestination(int index);
    }
}
