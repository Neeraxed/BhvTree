using BehaviourTree;

namespace Assets.Scripts.Bhv.NewRealisation.Conditions
{
    public class CheckIfGameWasPaused : Node
    {
        public override NodeState Evaluate()
        {
            state = GameManager.GamePaused ? NodeState.SUCCESS : NodeState.FAILURE;
            return state;
        }
    }
}
