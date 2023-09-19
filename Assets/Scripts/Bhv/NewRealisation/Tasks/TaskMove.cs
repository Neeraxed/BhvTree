using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using BehaviourTree;
using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation.Tasks
{
    public class TaskMove : Node
    {
        private IMovable movable;
        private IRoute route;

        public TaskMove(IMovable movable, IRoute route)
        {
            this.movable = movable;
            this.route = route;
        }

        public override NodeState Evaluate()
        {
            var destination = route.GetCurrentDestination();

            if (Vector3.Distance(movable.CurrentPosition, destination) > 0.01f)
            {
                movable.Move(destination);

                state = NodeState.RUNNING;
                return state;
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }
}
