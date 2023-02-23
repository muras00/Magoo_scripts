using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class ScriptObjRecipe : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artwork;

    public ScriptObjPlant plant1;
    public ScriptObjPlant plant2;
}
