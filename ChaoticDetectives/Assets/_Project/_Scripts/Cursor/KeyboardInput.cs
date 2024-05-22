using Unity.VisualScripting;
using UnityEngine;

public class KeyboardInput : AbstractInput
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnClickDown?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            OnClickUp?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            OnDirectionclamped?.Invoke(new Vector2(GetHorizontalInput(), GetVerticalInput()));
        }
    }

    public override float GetHorizontalInput()
    {
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