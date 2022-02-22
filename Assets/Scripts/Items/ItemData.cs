using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemData")]
public class ItemData : ScriptableObject
{
    public bool flammable = false;
    public bool fuel = false;
    public bool smeltable = false;

    public ItemData[] breakdownElements;
}
