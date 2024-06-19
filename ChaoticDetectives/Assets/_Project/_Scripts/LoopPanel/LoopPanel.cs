using System.Collections.Generic;
using UnityEngine;

public class LoopPanel : MonoBehaviour
{
    public static LoopPanel Instance { get; private set; }
    private Animator _animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _animator = GetComponent<Animator>();
    }

    public void EnablePanel(bool value)
    {
        _animator.SetBool("Hidden", value);
    }
}
