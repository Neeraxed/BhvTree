﻿using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using Assets.Scripts.Bhv.NewRealisation.Conditions;
using Assets.Scripts.Bhv.NewRealisation.Tasks;
using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Bhv.NewRealisation
{
    public class PatrolMan : BehaviourTree.Tree, IMovable, IAttack, IDetecting, IFreezable, IHittable
    {
        public RouteBase route;
        public TMP_Text text;
        public WeaponScriptable weaponScriptable;
        public LayerCombatRules combatRules;

        public int Damage => weaponScriptable.Damage;
        public float Speed { get; private set; } = 2f;
        public float DetectRange { get; private set; } = 5f;
        public int ShotTimes { get; private set; }

        public Vector3 CurrentPosition => transform.position;

        public IHittable CurrentEnemy => currentEnemy;

        public int OwnLayerMask => gameObject.layer;

        public float Health { get; private set; }

        public Vector3 TargetPosition => transform.position;


        private IHittable currentEnemy;

        protected override Node SetUpTree()
        {
            Node root = new Selector(new List<Node>()
            {
                new CheckIfGameWasPaused(),
                new Sequence(new List<Node>
                {
                    new CheckIfDestinationWasReached(this, route),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            new CheckIfEnemyInAttackRange(this, weaponScriptable),
                            new Sequence(new List<Node>
                            {
                                new CheckIfNotShootRequestedTimes(weaponScriptable, this),
                                new Selector(new List<Node>
                                {
                                    new Sequence(new List<Node>
                                    {
                                        new CheckIfHasAmmo(weaponScriptable),
                                        new TaskAttack(this,this, weaponScriptable)
                                    }),
                                    new Delay(this, new TaskReload(this), weaponScriptable.ReloadTime)
                                })
                            })
                        }),

                        new Sequence(new List<Node>
                        {
                            new CheckIfEnemyInDetectRange(this, this),
                            new TaskGoToTarget(this)
                        }),

                        new Delay(this, new TaskSetNewDestination(route), 2f)
                    })
                }),
                new TaskMove(this, route),
            });;

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

        private float currentHoldingTime = 0f;

        public void Freeze()
        {
            currentHoldingTime += Time.deltaTime;

            LogCurrentState($"Holding {currentHoldingTime.ToString("F2")} sec");
        }

        public void UnFreeze()
        {
            currentHoldingTime = 0f;
        }

        public void SetDetected(IHittable hit)
        {
            currentEnemy = hit;
        }

        private void LogCurrentState(string message)
        {
            text.UpdateText(message);
        }

        private void Awake()
        {
            Health = 500;
            text.UpdateText($"Current health: {Health}");
        }
        public void TakeHit(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Die();
            }

            text.UpdateText($"Current health: {Health}");
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
