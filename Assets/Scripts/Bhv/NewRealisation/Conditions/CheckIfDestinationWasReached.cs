using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using BehaviourTree;
using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation.Conditions
{
    public class CheckIfDestinationWasReached : Node
    {
        private IMovable movable;
        private IRoute route;

        public CheckIfDestinationWasReached(IMovable movable, IRoute route)
        {
            this.movable = movable;
            this.route = route;
        }

        public override NodeState Evaluate()
        {
            var destination = route.GetCurrentDestination();

            if (Vector3.Distance(movable.CurrentPosition, destination) < 0.01f)
            {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}
