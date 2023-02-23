using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [NonSerialized] public int inkFileCount = 0;
    public TextAsset[] inkFileArray;
    [NonSerialized] public int inkFileSize = 0;
    void Start() {
        inkFileSize = (inkFileArray.Length - 1);
    }
}