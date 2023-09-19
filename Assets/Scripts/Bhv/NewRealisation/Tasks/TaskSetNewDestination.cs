using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using BehaviourTree;
using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation.Tasks
{
    public class TaskSetNewDestination : Node
    {
        private IRoute route;

        private int currentDestinationIndex = 0;

        public TaskSetNewDestination(IRoute route)
        {
            this.route = route;
        }

        public override NodeState Evaluate()
        {
            route.SelectNewDestination(currentDestinationIndex);

            currentDestinationIndex = (currentDestinationIndex + 1) % route.Waypoints.Length;

            state = NodeState.SUCCESS;
            return state;
        }
    }
}
