using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Info Data", menuName = "Scriptable Object/Info Data", order = 2)]
public class InfoData : ScriptableObject
{
    [SerializeField] private string infoName;
    public string InfoName { get { return infoName; } }
    [SerializeField] private string infoDialog;
    public string InfoDialog { get { return infoDialog; } }
}
