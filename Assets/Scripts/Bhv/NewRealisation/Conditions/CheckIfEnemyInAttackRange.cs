using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using BehaviourTree;
using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation.Conditions
{
    public class CheckIfEnemyInAttackRange : Node
    {
        private IMovable movable;
        private WeaponScriptable weaponScriptable;

        public CheckIfEnemyInAttackRange(IMovable movable, WeaponScriptable weaponScriptable)
        {
            this.movable = movable;
            this.weaponScriptable = weaponScriptable;
        }

        public override NodeState Evaluate()
        {
            var hits = parent.parent.GetData("target") as IHittable;

            if (hits == null)
            {
                state = NodeState.FAILURE;
                return state;
            }

            if ((movable.CurrentPosition - hits.TargetPosition).magnitude < weaponScriptable.AttackRange)
            {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}
