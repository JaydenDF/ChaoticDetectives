using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAnimation : MonoBehaviour
{
    [SerializeField]private Animator _animator;

    private void Awake()
    {
    }

    public void Show()
    {
        _animator.SetBool("needsToAppear", true);
    }

    public void Hide()
    {
        _animator.SetBool("needsToAppear", false);
    }

    private void OnEnable()
    {
        Items.OnCollected += OnCollected;
    }

    private void OnDisable()
    {
        Items.OnCollected -= OnCollected;
    }

    private void OnCollected()
    {
        StartCoroutine(ShowAndHideAfterDelay(2f));
    }

    private IEnumerator ShowAndHideAfterDelay(float delay = 2)
    {
        Show();
        Debug.Log("SHOWN");
        yield return new WaitForSeconds(delay);
        Hide();
        Debug.Log("HDDEN");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Show();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Hide();
    }
}
