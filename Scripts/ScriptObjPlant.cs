using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class ScriptObjPlant : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artwork;
}