using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingKiwi : MonoBehaviour
{
    Animator animator;
    AnimationClip animationClip;
    float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        waitTime = animationClip.length + 6f;
        InvokeRepeating("BlinkingShopKiwi", 6f, waitTime);
    }

    void PlayAnimation()
    {
        animator.Play("MeteorBreaking");
    }

}
    
