using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardInput : AbstractInput
{

    public UnityEvent OnClickDownEvent;
    public UnityEvent OnClickUpEvent;
    private void Update()
    {
        if(enabled == false) return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            OnClickDown?.Invoke();
            OnClickDownEvent?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            OnClickUp?.Invoke();
            OnClickUpEvent?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            OnDirectionclamped?.Invoke(new Vector2(GetHorizontalInput(), GetVerticalInput()));
        }
    }

    public override float GetHorizontalInput()
    {
        if (enabled == false) return 0;

        if (Input.GetKey(KeyCode.A))
        {
            return -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public override float GetVerticalInput()
    {
        if (enabled == false) return 0;

        if (Input.GetKey(KeyCode.S))
        {
            return -1;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}