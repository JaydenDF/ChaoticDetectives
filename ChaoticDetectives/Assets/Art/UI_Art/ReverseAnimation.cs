using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReverseAnimation : MonoBehaviour
{
    public Button reverseButton;
    public Animator animator;
    public GameObject turnOff;
    public float delayBeforeDeactivation = 2.6f; 

    private void Start()
    {
        reverseButton.onClick.AddListener(ReverseButton);
    }

    private void ReverseButton()
    {
     
        string stateName = "Run"; 

        animator.SetFloat("SpeedMultiplier", -1);
        animator.Play(stateName, 0, 2f);
        StartCoroutine(Deactivate());
    }

    private IEnumerator Deactivate()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeDeactivation);

        // Deactivate the GameObject
        turnOff.SetActive(false);
    }
}
