using Unity.VisualScripting;
using UnityEngine;

public class KeyboardInput : AbstractInput
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnClick?.Invoke();
        }

        GetHorizontalInput();
        GetVerticalInput();
    }

    public override float GetHorizontalInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            OnDirectionclamped?.Invoke(Vector2.right);
            return -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            OnDirectionclamped?.Invoke(Vector2.left);
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
            OnDirectionclamped?.Invoke(Vector2.down);
            return -1;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            OnDirectionclamped?.Invoke(Vector2.up);
            return 1;
        }
        else
        {
            return 0;
        }
    }

}