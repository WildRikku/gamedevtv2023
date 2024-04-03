using System;
using System.Collections.Generic;
using UnityEngine;

public enum ModuleType
{
    Connector,
    MainEngine,
    StrafeEngine,
    DirectionEngine,
    Cockpit,
    Weapon
}


[Serializable]
public class Modules
{
    public string moduleName;
    public GameObject modulePrefabs;
    public GameObject hangarPrefab;
    public Sprite modulSprite;
    public ModuleType moduleType;
    public bool canLeft = false;
    public bool canRight = false;
    public bool canFront = false;
    public bool canBack = false;
    public ModuleValues moduleValues = new();
}


[CreateAssetMenu(fileName = "ModuleList", menuName = "Scriptable Objects/ModuleList")]
public class ModuleList : ScriptableObject
{
    public List<Modules> moduls;
}
