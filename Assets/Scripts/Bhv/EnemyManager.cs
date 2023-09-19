using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IHittable
{
    [SerializeField]
    private TMP_Text text;

    public float Health { get ; private set; }

    public Vector3 TargetPosition => transform.position;

    private void Awake()
    {
        Health = 300;
        text.UpdateText($"Current health: {Health}");
    }

    private void Die()
    {
        Destroy(gameObject);
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
}
