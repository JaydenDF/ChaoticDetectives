using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AbstractInput : MonoBehaviour
{
    public Action<Vector2> OnDirectionclamped;
    public Action OnClickDown;
    public Action OnClickUp;
    public abstract float GetHorizontalInput();
    public abstract float GetVerticalInput();
    public virtual void DisableInput()
    {
        enabled = false;
    }
    public virtual void EnableInput()
    {
        enabled = true;
    }
}