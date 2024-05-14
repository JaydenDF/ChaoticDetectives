using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour, IInteractable
{
    [SerializeField] private List<bool> objectives = new List<bool>();

    public void OnClick()
    {
        if(objectives.Count > 0)
        {
            for(int i = 0; i < objectives.Count; i++)
            {
                if (objectives[i] == false) 
                {
                    break;
                } else if (objectives[i] == true && i == objectives.Count -1)
                {
                    Debug.Log("Clicked!");
                }
            }
        }
    }

    public void OnHoverEnter()
    {
        Debug.Log("Hovering!");
    }

    public void OnHoverExit()
    {
        Debug.Log("Not hovering!");
    }
}
