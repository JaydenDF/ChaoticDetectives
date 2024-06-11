using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSkipper : MonoBehaviour
{
    public List<Animator> animators = new List<Animator>();

    public void Skip()
    {
        foreach (var animator in animators)
        {
            animator.speed = 1000f;
        }

        StartCoroutine(ResetSpeed());
    }

    private IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (var animator in animators)
        {
            animator.speed = 1f;
        }
    }
}
