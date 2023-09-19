using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using UnityEngine;

namespace BehaviourTree
{
    public class Delay : Node
    {
        private Node childNode;
        private IFreezable freezable;

        private float currentDelayTime = 0f;
        private float maxDelayTime;

        public Delay(IFreezable freezable, Node childNode, float maxDelayTime)
        {
            this.freezable = freezable;
            this.childNode = childNode;
            this.maxDelayTime = maxDelayTime;
        }

        public override NodeState Evaluate()
        {
            currentDelayTime += Time.deltaTime;

            if (currentDelayTime > maxDelayTime)
            {
                currentDelayTime = 0f;
                freezable.UnFreeze();

                return childNode.Evaluate();
            }

            freezable.Freeze();

            state = NodeState.RUNNING;
            return state;
        }

    }
}
