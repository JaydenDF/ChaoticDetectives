using System.Collections;
using System.Collections.Generic;
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
        _inventory.OnItemAdded.AddListener(OnCollected);
    }

    private void OnDisable()
    {
        _inventory.OnItemAdded.RemoveListener(OnCollected);
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