using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using BehaviourTree;
using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation.Tasks
{
    public class TaskAttack : Node
    {
        private IAttack attack;
        private IDetecting detecting;
        private WeaponScriptable weaponScriptable;

        private float currentShootingTime = 0;

        public TaskAttack(IDetecting detecting, IAttack attack, WeaponScriptable weaponScriptable)
        {
            this.detecting = detecting;
            this.attack = attack;
            this.weaponScriptable = weaponScriptable;
        }

        public override NodeState Evaluate()
        {
            //var target = detecting.CurrentEnemy;
            var target = GetData("target") as IHittable;

            if (target != null)
            {
                currentShootingTime += Time.deltaTime;

                if (currentShootingTime > weaponScriptable.AttackSpeed)
                {
                    currentShootingTime = 0;

                    attack.Attack(target);

                    if (target.Health <= 0)
                        ClearData("target");

                    state = NodeState.RUNNING;
                    return state;
                }
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }
}
