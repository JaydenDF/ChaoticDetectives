using System.Collections.Generic;
using UnityEngine;

public class LoopContainer : MonoBehaviour {
    public List<ILoop> GetLoopableComponents()
    {
        List<ILoop> loopableComponents = new List<ILoop>();
        foreach(Transform child in transform)
        {
            ILoop loopableComponent = child.GetComponent<ILoop>();
            if (loopableComponent != null)
            {
                loopableComponents.Add(loopableComponent);
            }
        }

        return loopableComponents;
    }

    public void Loop(Transform targetLoop)
    {
        List<ILoop> loopableComponents = GetLoopableComponents();
        foreach (ILoop loopableComponent in loopableComponents)
        {
            loopableComponent.Loop(targetLoop);
        }
    }
}