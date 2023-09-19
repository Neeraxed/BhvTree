using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using BehaviourTree;

namespace Assets.Scripts.Bhv.NewRealisation.Conditions
{
    public class CheckIfNotShootRequestedTimes : Node
    {
        private WeaponScriptable weaponScriptable;
        private IAttack attack;

        public CheckIfNotShootRequestedTimes(WeaponScriptable weaponScriptable, IAttack attack)
        {
            this.weaponScriptable = weaponScriptable;
            this.attack = attack;
        }

        public override NodeState Evaluate()
        {
            state = attack.ShotTimes < weaponScriptable.MaxAllowedAttacks ?
                NodeState.SUCCESS :
                NodeState.FAILURE;
            return state;
        }
    }
}
