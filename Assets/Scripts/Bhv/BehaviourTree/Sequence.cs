using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree
{
    public class Sequence : Node
    {
        public Sequence()
            : base() { }
        public Sequence(List<Node> children)
            : base(children) { }

        public Sequence(params Node[] children)
            : base(children.ToList()) { }

        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (var node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }
            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}
