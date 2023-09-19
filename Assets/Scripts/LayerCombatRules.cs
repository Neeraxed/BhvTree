using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combat rules")]
public class LayerCombatRules : ScriptableObject
{
    [SerializeField]
    private List<EnemyLayer> layers = new List<EnemyLayer>();

    public int GetDetectionLayersTo(string key)
    {
        var found = layers.Find(layer => layer.mainLayer == key);

        if (found != null)
            return LayerMask.GetMask(found.layersToAttack);

        return -1;
    }
}

[Serializable]
public class EnemyLayer
{
    public string mainLayer;
    public string[] layersToAttack;
}
