using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NaAh : MonoBehaviour
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
