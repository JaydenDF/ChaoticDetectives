using System;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public static Action OnLooped;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Loop();
        }
    }

    private void Loop()
    {
        OnLooped?.Invoke();
    }
}