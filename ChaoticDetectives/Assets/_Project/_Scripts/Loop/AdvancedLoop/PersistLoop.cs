using UnityEngine;

public class PersistLoop : MonoBehaviour, ILoop, IReset
{
    public bool persist = false;
    private Transform parentTransform;

    private bool _initialPersist;
    private Transform _initialParentTransform;

    private bool _awakeCalled = false;
    private void Awake()
    {
        if  (_awakeCalled) { return; }
        _awakeCalled = true;

       _initialPersist = persist;
        parentTransform = this.transform.parent;
        _initialParentTransform = parentTransform;
    }
    public void Loop(Transform targetLoop)
    {
        if(!_awakeCalled) { Awake(); }

        if (persist == false) { return; }

        this.transform.parent = targetLoop;
    }

    public void persistLoop(bool condition)
    {
        if(!_awakeCalled) { Awake(); }
        
        persist = condition;
    }

    public void Reset()
    {
        if (!_awakeCalled) { Awake(); }

        persist = _initialPersist;
        parentTransform = _initialParentTransform;
        this.transform.parent = parentTransform;
    }
}