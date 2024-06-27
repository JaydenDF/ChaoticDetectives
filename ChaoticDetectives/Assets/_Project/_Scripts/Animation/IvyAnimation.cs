using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(changeIvySprite(1f));
    }
    private IEnumerator changeIvySprite(float delay = 2)
    {
        if (_animator != null)
        {
            
            yield return new WaitForSeconds(delay);
            IvySeesYou();
        }
    }

    private void IvySeesYou()
    {
        _animator.SetBool("NeedsToReset", false);
        _animator.SetBool("IvySeesYou", true);
    }

    public void ResetIvyAni()
    {
        _animator.SetBool("IvySeesYou", false);
        _animator.SetBool("NeedsToReset", true);
    }

    private void OnDisable()
    {
        ResetIvyAni();
    }
}
