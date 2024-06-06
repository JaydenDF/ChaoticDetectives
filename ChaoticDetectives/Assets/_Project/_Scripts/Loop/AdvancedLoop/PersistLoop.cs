using UnityEngine;

public class PersistLoop : MonoBehaviour, ILoop
{
    public bool persist = false;

    public void Loop(Transform targetLoop)
    {
        if (persist == false) { return; }

        this.transform.parent = targetLoop;
    }

    public void persistLoop(bool condition)
    {
        persist = condition;
    }
}