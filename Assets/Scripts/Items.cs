using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class Items 
{
    public static Dictionary<int, ItemValues> healthList = new Dictionary<int, ItemValues>();
}

public class ItemValues
{
    public int health;
    public SpawnType spawnType;
}
