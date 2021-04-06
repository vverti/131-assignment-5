using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;
[CreateAssetMenuAttribute(fileName = "New Mage Data", menuName = "Character data/Mage")]
public class MageData : CharacterData
{
    public MageDmgType dmgType;
    public MageWpnType wpnType;
}
