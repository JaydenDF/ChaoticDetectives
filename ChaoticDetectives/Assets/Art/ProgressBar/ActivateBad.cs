using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivateBad : MonoBehaviour
{
    public GameObject Book;
    public GameObject IvyBad;
    public void UpdateProgress()
    {
        if (IvyBad.gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }
}
