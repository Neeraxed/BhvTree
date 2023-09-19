using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using BehaviourTree;
using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation.Tasks
{
    public class TaskGoToTarget : Node
    {
        private IMovable movable;

        public TaskGoToTarget(IMovable movable)
        {
            this.movable = movable;
        }

        public override NodeState Evaluate()
        {
            var targetPosition = (GetData("target") as IHittable);

            if (targetPosition == null)
            {
                state = NodeState.FAILURE;
                return state;
            }

            if (Vector3.Distance(movable.CurrentPosition, targetPosition.TargetPosition) > 0.01f)
            {
                movable.Move(targetPosition.TargetPosition);

                state = NodeState.RUNNING;
                return state;
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }
}
