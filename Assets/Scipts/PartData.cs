using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartData", menuName = "Data")]
public class PartData : ScriptableObject
{
    public Color defaultColor;
    public string partName;
    public string particleName;
    public string poolName;
}
