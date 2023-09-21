namespace Assets.Scripts.Bhv.NewRealisation.Interfaces
{
    public interface IDetecting
    {
        void SetDetected(IHittable hit);

        IHittable CurrentEnemy { get; }
        float DetectRange { get; }
        int OwnLayerMask { get; } 
    }
}
