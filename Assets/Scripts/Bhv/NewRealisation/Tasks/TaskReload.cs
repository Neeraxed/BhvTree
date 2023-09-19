using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using BehaviourTree;

namespace Assets.Scripts.Bhv.NewRealisation.Tasks
{
    public class TaskReload : Node
    {
        private IAttack attack;

        public TaskReload(IAttack attack)
        {
            this.attack = attack;
        }

        public override NodeState Evaluate()
        {
            attack.Reload();

            state = NodeState.SUCCESS;
            return state;
        }
    }
}
