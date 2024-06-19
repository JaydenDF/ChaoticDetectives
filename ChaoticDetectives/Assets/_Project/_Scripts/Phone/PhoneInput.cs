using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PhoneInput : MonoBehaviour
{
    public UnityEvent OnPhoneUp;
    public UnityEvent OnPhoneDown;

    private const string _inputDown = "n";
    private const string _inputUp = "k";

    private void Update()
    {
        if (Input.GetKey(_inputDown))
        {
            OnPhoneUp?.Invoke();
        }
        else if (Input.GetKey(_inputUp))
        {
            OnPhoneDown?.Invoke();
        }

    }
}
