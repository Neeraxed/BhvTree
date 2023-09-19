using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation.Interfaces
{
    public interface IHittable
    {
        float Health { get; }
        Vector3 TargetPosition { get; }
        void TakeHit(int damage);
    }
}
