using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollisionManager : MonoBehaviour
{
    public SpawnableItem[] collideableItems;

    public Animator animator;
    public AnimationClip animationClip;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.Play("LeftClick_anim");
        }
        else if(Input.GetKey(KeyCode.Mouse0))
        {
            animator.Play("LoopedLeftClick_anim");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.name.Contains("Terrain Chunk") && !collision.transform.name.Contains("Map"))
        {
            string name;
            if(!collision.transform.parent.name.Contains("Terrain Chunk"))
            {
                name = collision.transform.parent.name;
                
            }
            else
            {
                name = collision.transform.name;
            }
            print(--Items.healthList[int.Parse(name)].health);
        }
    }
}
