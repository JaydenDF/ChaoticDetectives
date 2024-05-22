using System;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public static Action OnLooped;


    private void Update()
    {
        
    }

    private void Loop()
    {
        OnLooped?.Invoke();
    }
}