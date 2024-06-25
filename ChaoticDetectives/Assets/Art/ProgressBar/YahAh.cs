using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YahAh : MonoBehaviour
{
    public GameObject GameObject;
    public GameObject IvyGood;
    public void UpdateProgress()
    {
        if (IvyGood.gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }
}
