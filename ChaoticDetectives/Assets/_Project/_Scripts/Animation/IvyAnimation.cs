using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyAnimation : MonoBehaviour
{
    private Animator _animator;
    bool hasBeenPlayed = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        changeIvySprite();
    }

    private void Update()
    {
        if (!hasBeenPlayed)
        {
            StartCoroutine(changeIvySprite(1f));
        }
    }

    private IEnumerator changeIvySprite(float delay = 2)
    {
        if (_animator != null)
        {
            ResetIvyAni();
            yield return new WaitForSeconds(delay);
            IvySeesYou();
            hasBeenPlayed = true;
        }
    }

    private void IvySeesYou()
    {
        _animator.SetBool("IvySeesYou", true);
    }

    public void ResetIvyAni()
    {
        _animator.SetBool("IvySeesYou", false);
        hasBeenPlayed = false;
    }

    private void OnDisable()
    {
        ResetIvyAni();
    }
}
