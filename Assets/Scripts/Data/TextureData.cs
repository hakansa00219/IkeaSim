using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TextureData : UpdatableData
{
    public Color[] baseColors;
    [Range(2,100)]
    public float blendPower;
    [Range(0,1)]
    public float[] baseStartHeights;
    float savedMinHeight;
    float savedMaxHeight;
    public void ApplyToMaterial(Material material)
    {
        for (int i = 0; i < baseColors.Length; i++)
        {
            baseColors[i].a = 1;
            material.SetColor("_color" + (i + 1), baseColors[i]);
            material.SetFloat("_height" + (i + 1), baseStartHeights[i]);
        }
        material.SetFloat("_blendPower", blendPower);
        UpdateMeshHeights(material, savedMinHeight, savedMaxHeight);
    }
  
    public void UpdateMeshHeights(Material material, float minHeight, float maxHeight)
    {

        savedMinHeight = minHeight;
        savedMaxHeight = maxHeight;
        
        material.SetFloat("_minHeight", minHeight);
        material.SetFloat("_maxHeight", maxHeight);
    }
}
