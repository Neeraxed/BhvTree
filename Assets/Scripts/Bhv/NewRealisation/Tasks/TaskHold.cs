using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using BehaviourTree;
using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation.Tasks
{
    public class TaskHold : Node
    {
        private IHold holding;
        private IRoute route;

        private float holdingTime;
        private float currentHoldingTime;
        private int currentDestinationIndex = 0;

        public TaskHold(IHold holding,IRoute route, float holdingTime)
        {
            this.holding = holding;
            this.route = route;
            this.holdingTime = holdingTime;
        }

        public override NodeState Evaluate()
        {
            currentHoldingTime += Time.deltaTime;

            if (currentHoldingTime > holdingTime)
            {
                currentHoldingTime = 0f;

                route.SelectNewDestination(currentDestinationIndex);
                currentDestinationIndex = (currentDestinationIndex + 1) % route.Waypoints.Length;
                holding.UnHold();

                state = NodeState.SUCCESS;
                return state;
            }

            holding.Hold();

            state = NodeState.RUNNING;
            return state;
        }
    }
}
