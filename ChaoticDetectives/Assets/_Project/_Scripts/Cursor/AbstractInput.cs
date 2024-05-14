using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AbstractInput : MonoBehaviour {
    public Action OnClick;
    public abstract float GetHorizontalInput(); 
    public abstract float GetVerticalInput();
}   