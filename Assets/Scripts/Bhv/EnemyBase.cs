using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using BehaviourTree;
using System;
using TMPro;
using UnityEngine;
namespace Assets.Scripts.Bhv.NewRealisation
{
    public class EnemyBase : BehaviourTree.Tree, IHittable
    {
        [SerializeField] protected TMP_Text text;

        public float Health { get; set; }

        public Vector3 TargetPosition => transform.position;

        public event Action<EnemyBase> OnDie;

        protected RouteBase route;

        protected virtual void Awake()
        {
            text.UpdateText($"Current health: {Health}");
        }

        protected virtual void Die()
        {
            OnDie?.Invoke(this);
        }

        public void TakeHit(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                Die();
            text.UpdateText($"Current health: {Health}");
        }

        protected override Node SetUpTree()
        {
            throw new System.NotImplementedException();
        }

        public void SetRoute(RouteBase route)
        {
            this.route = route;
        }
    }
}