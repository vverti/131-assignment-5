using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;
[CreateAssetMenuAttribute(fileName =  "New Warrior Data",menuName = "Character data/Warrior")]
public class WarriorData : CharacterData
{
    public WarriorWpnType wpnType;
    public WarriorClassType classType;
}
