using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonologueAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Inventory _inventory;
    public float delayTime;

    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    public void Show()
    {
        _animator.SetBool("needsToShowMono", true);
    }

    public void Hide()
    {
        _animator.SetBool("needsToShowMono", false);
    }

    private void OnEnable()
    {
        if (_inventory != null)
        {
            _inventory.OnItemAdded.AddListener(OnCollected);
        }


        MonologueSystem.Instance.MonologueText += Show;
        UIChanceEvent.OnChanceEventEnd += Hide;
    }

    private void OnDisable()
    {
        if (_inventory != null)
        {
            _inventory.OnItemAdded.RemoveListener(OnCollected);
        }

        MonologueSystem.Instance.MonologueText -= Show;
        UIChanceEvent.OnChanceEventEnd -= Hide;
    }

    private void Show(string obj)
    {
        StartMonologueAnimation();
    }

    private void StartMonologueAnimation()
    {
        if (_animator != null)
        {
            Show();
        }
    }

    private void OnCollected()
    {
        StartCoroutine(ShowAndHideAfterDelay(delayTime));
    }

    private IEnumerator ShowAndHideAfterDelay(float delay = 2f)
    {
        if (_animator != null)
        {
            Show();
            yield return new WaitForSeconds(delay);
            Hide();
        }
    }
}
