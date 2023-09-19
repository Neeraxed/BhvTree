using Assets.Scripts.Bhv.NewRealisation.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Assets/Scripts/Bhv/NewRealisation/ScriptableObjects/New weapon")]
public class WeaponScriptable : ScriptableObject
{
    public string weaponName;

    [SerializeField]
    [Range(1, 10)]
    private int maxAmmoCapacity;
    [SerializeField]
    [Range(5, 10)]
    private int maxAllowedAttacks;

    [SerializeField]
    [Range(1, 100)]
    private int damage;
    [SerializeField]
    [Range(1, 100)]
    private float attackSpeed;
    [SerializeField]
    [Range(1, 10)]
    private float attackRange;
    [SerializeField]
    [Range(0, 2)]
    private float reloadTime;

    public int CurrentAmmoCapacity => currentAmmoCapacity;
    public int MaxAllowedAttacks => maxAllowedAttacks;
    public int Damage => damage;
    public float AttackSpeed => attackSpeed;
    public float AttackRange => attackRange;
    public float ReloadTime => reloadTime;

    public bool HasAmmo => currentAmmoCapacity != 0;

    private int currentAmmoCapacity;

    public void Shoot(IHittable hit)
    {
        hit.TakeHit(Damage);
        currentAmmoCapacity--;
    }

    public void Reload()
    {
        currentAmmoCapacity = maxAmmoCapacity;
    }

    private void OnEnable()
    {
        Reload();
    }
}
