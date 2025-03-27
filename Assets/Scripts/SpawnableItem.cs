using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Spawnable Item", menuName = "Items/Spawnable Item")]
public class SpawnableItem : ScriptableObject
{
    public string name;
    public GameObject[] typeOfObjectsToSpawn;
    public int countToSpawnEachChunk;
    public float minSpawnHeightLimit;
    public float maxSpawnHeightLimit;
    public Properties properties;

    private int index = 0;


    public float meshHeight
    {
        get
        {
            MeshRenderer[] meshes = typeOfObjectsToSpawn[index].GetComponentsInChildren<MeshRenderer>();
            float meshHeight = 0;
            foreach (MeshRenderer m in meshes)
            {
                meshHeight += m.bounds.size.y;
            }
            return meshHeight;
        }
        
    }
    [System.Serializable]
    public struct Properties
    {
        public int health;
        public SpawnType spawnType;
        public bool respawnable;
        public bool destroyable;
        public bool dropLoots;
        public GameObject[] lootsObjects;
    }
}

public enum SpawnType
{
    OakTree,
    BananaTree,
    Iron
}



