using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public HeightMapSettings heightMap;
    public TextureData textureData;
    public MeshSettings meshSettings;

    public GameObject mapGenerator;

    public SpawnableItem[] spawnableItems;
    public static int ID = 1;

    public IEnumerator SpawnAfterChunkVisible(TerrainChunk chunk)
    {
        foreach(SpawnableItem item in spawnableItems)
        {
            int loop = item.countToSpawnEachChunk;
            GameObject chunkObject = mapGenerator.transform.Find(chunk.nameOfChunk).gameObject;

            chunkObject.GetComponent<MeshCollider>().sharedMesh = TerrainChunk.savedMeshes[chunk.nameOfChunk];
            while (loop > 0)
            {
                Vector3 raycastPosition = new Vector3(
                    Random.Range(meshSettings.meshWorldSize * (chunk.coord.x - 1), meshSettings.meshWorldSize * (chunk.coord.x + 1)),
                    100,
                    Random.Range(meshSettings.meshWorldSize * (chunk.coord.y - 1), meshSettings.meshWorldSize * (chunk.coord.y + 1)));
                RaycastHit hit;
                int selectedRandomTypeIndex = Random.Range(0, item.typeOfObjectsToSpawn.Length);

                if (Physics.Raycast(raycastPosition, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
                {
                    if (hit.collider.name == chunk.nameOfChunk)
                    {
                        if (hit.point.y > heightMap.heightMultiplier * item.minSpawnHeightLimit && hit.point.y <= heightMap.heightMultiplier * item.maxSpawnHeightLimit)
                        {
                            float scaleMultiplier = Random.Range(1f, 4f);
                            GameObject spawnedObject;
                            switch (item.name)
                            {
                                case "Iron":
                                    spawnedObject = Instantiate(item.typeOfObjectsToSpawn[selectedRandomTypeIndex],
                                    new Vector3(hit.point.x, hit.point.y + item.typeOfObjectsToSpawn[selectedRandomTypeIndex].GetComponent<MeshRenderer>().bounds.size.y / 3f * scaleMultiplier,
                                    hit.point.z), Quaternion.identity);
                                    break;
                                default:
                                    spawnedObject = Instantiate(item.typeOfObjectsToSpawn[selectedRandomTypeIndex], new Vector3(hit.point.x, hit.point.y - 0.05f * scaleMultiplier, hit.point.z), Quaternion.identity);
                                    break;
                            }
                            spawnedObject.transform.parent = chunkObject.transform;
                            spawnedObject.transform.localScale *= scaleMultiplier;

                            spawnedObject.name = ID.ToString();
                            Items.healthList.Add(ID++, new ItemValues { health = item.properties.health, spawnType = item.properties.spawnType });

                            loop--;
                        }
                    }
                    if(loop % 5 == 0)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }       
    }
}
