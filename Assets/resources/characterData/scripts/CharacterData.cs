using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : ScriptableObject
{
    public GameObject prefab;
    public float maxHealth,
                 maxEnergy,
                 critChance,
                 power;
    public string name;
}
