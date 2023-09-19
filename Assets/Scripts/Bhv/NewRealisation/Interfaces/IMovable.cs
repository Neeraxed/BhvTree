using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation.Interfaces
{
    public interface IMovable
    {
        Vector3 CurrentPosition { get; }
        void Move(Vector3 destination);
    }
}
