using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item")]
public class ItemScriptable : ScriptableObject
{
    [Serializable]
    public enum ItemType
    {
        Consumable, Throwable
    }
    
    [BoxGroup("Item Settings")]
    public ItemType type;
    
    [Serializable]
    public enum ConsumableType
    {
        Speed
    }
    
    [BoxGroup("Item Settings")]
    [ShowIf("type", ItemType.Consumable)] public ConsumableType consumableType;
    
    [BoxGroup("Item Settings")]
    [ShowIf("type", ItemType.Consumable)] public float consumableValue;

    [BoxGroup("Item Settings")]
    [ShowIf("type", ItemType.Throwable)]
    public GameObject itemPrefab;
}
