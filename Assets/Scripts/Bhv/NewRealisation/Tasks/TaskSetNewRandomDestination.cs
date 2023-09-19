using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using BehaviourTree;
using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation.Tasks
{
    public class TaskSetNewRandomDestination : Node
    {
        private IRoute route;

        private int currentDestinationIndex = 0;

        public TaskSetNewRandomDestination(IRoute route)
        {
            this.route = route;
        }

        public override NodeState Evaluate()
        {
            route.SelectNewDestination(currentDestinationIndex);

            currentDestinationIndex = Random.Range(0, route.Waypoints.Length - 1);

            state = NodeState.SUCCESS;
            return state;
        }
    }
}
