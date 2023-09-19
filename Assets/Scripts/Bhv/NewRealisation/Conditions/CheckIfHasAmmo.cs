using BehaviourTree;

namespace Assets.Scripts.Bhv.NewRealisation.Conditions
{
    public class CheckIfHasAmmo : Node
    {
        private WeaponScriptable weaponScriptable;

        public CheckIfHasAmmo(WeaponScriptable weaponScriptable)
        {
            this.weaponScriptable = weaponScriptable;
        }

        public override NodeState Evaluate()
        {
            state = weaponScriptable.HasAmmo ? NodeState.SUCCESS : NodeState.FAILURE;

            return state;
        }
    }
}
