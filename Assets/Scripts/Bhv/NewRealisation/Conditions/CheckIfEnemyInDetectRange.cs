using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using BehaviourTree;
using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation.Conditions
{
    public class CheckIfEnemyInDetectRange : Node
    {
        private Collider[] targetBuffer = new Collider[2];
        private IDetecting detecting;
        private IMovable movable;
        private int enemyLayerMask; //=  1 << LayerMask.NameToLayer("Enemy");

        public CheckIfEnemyInDetectRange(IDetecting detecting, IMovable movable)
        {
            this.detecting = detecting;
            this.movable = movable;
            enemyLayerMask = SideChoice.ChooseWhomToHit(detecting.OwnLayerMask);
        }

        public override NodeState Evaluate()
        {
            var hits = Physics.OverlapSphereNonAlloc(movable.CurrentPosition, detecting.DetectRange, targetBuffer, enemyLayerMask);

            if (hits > 0)
            {
                var target = targetBuffer[0].GetComponent<IHittable>();

                parent.parent.SetData("target", target);

                detecting.SetDetected(target);

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}
