using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using Assets.Scripts.Bhv.NewRealisation.Conditions;
using Assets.Scripts.Bhv.NewRealisation.Tasks;
using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Scripts.Bhv.NewRealisation
{
    public class RainingMan : EnemyBase, IMovable, IAttack, IDetecting, IHittable
    {
        public WeaponScriptable weaponScriptable;
        public LayerCombatRules combatRules;

        public int OwnLayerMask => gameObject.layer;
        public int Damage => weaponScriptable.Damage;
        public float Speed { get; private set; } = 3f;
        public float DetectRange { get; private set; } = 5f;
        public int ShotTimes { get; private set; }

        public Vector3 CurrentPosition => transform.position;

        public IHittable CurrentEnemy => currentEnemy;

        [SerializeField] private int health;
        private IHittable currentEnemy;

        protected override void Awake()
        {
            base.Awake();
            Health = health;
        }
        protected override Node SetUpTree()
        {
            Node root = new Selector(new List<Node>()
            {
                new CheckIfGameWasPaused(),
                new Sequence(new List<Node>
                {
                    new CheckIfEnemyInAttackRange(this, weaponScriptable),
                    new TaskAttack(this, this,weaponScriptable)
                }),
                new Sequence(new List<Node>
                {
                    new CheckIfEnemyInDetectRange(this, this),
                    new TaskGoToTarget(this),
                }),
                new Sequence(new List<Node>
                {
                    new Inverter(new CheckIfDestinationWasReached(this, route)),
                    new TaskMove(this, route),
                }),
                new TaskSetNewRandomDestination(route),
            }); ;

            #region Old tree
            //Node root = new Selector(new List<Node>()
            //{
            //    new CheckIfGameWasPaused(),
            //    new Sequence(new List<Node>
            //    {
            //        new CheckIfDestinationWasReached(this, route),
            //        new Selector(new List<Node>
            //        {
            //            new Sequence(new List<Node>
            //            {
            //                new CheckIfEnemyInAttackRange(this,this),
            //                new Selector(new List<Node>
            //                {
            //                    new Sequence(new List<Node>
            //                    {
            //                        new CheckIfNotShootRequestedTimes(weaponScriptable, this),
            //                        new Selector(new List<Node>
            //                        {
            //                            new Sequence(new List<Node>
            //                            {
            //                                new CheckIfHasAmmo(weaponScriptable),
            //                                new TaskAttack(this,this, weaponScriptable)
            //                            }),
            //                            new Delay(this, new TaskReload(this), weaponScriptable.ReloadTime)
            //                        })
            //                    }),
            //                    new Sequence(new List<Node>
            //                    {
            //                        new Delay(this, new TaskSetNewDestination(route), 2f),
            //                        new TaskMove(this,route)
            //                    })
            //                })
            //            }),
            //            new Delay(this, new TaskSetNewDestination(route), 2f)
            //        })
            //    }),
            //    new TaskMove(this, route),
            //});
            #endregion

            return root;
        }

        public void Move(Vector3 destination)
        {
            LogCurrentState("Moving");
            transform.position = Vector3.MoveTowards(transform.position, destination, Speed * Time.deltaTime);
        }

        public void Attack(IHittable hittable)
        {
            LogCurrentState("Attacking");
            weaponScriptable.Shoot(hittable);
            ShotTimes++;
        }

        public void Reload()
        {
            LogCurrentState("Reloading");
            weaponScriptable.Reload();
        }

        public void SetDetected(IHittable hit)
        {
            currentEnemy = hit;
        }

        private void LogCurrentState(string message)
        {
            text.UpdateText(message);
        }
    }
}
