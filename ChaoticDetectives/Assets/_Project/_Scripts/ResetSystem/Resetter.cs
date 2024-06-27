using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Resetter : MonoBehaviour
{
    public UnityEvent OnReset;
    List<IReset> _resetables = new List<IReset>();
    private void Awake()
    {
        _resetables = FindAllComponents<IReset>(true);
        var detectStart = FindAllComponents<IDetectStart>(true);
        foreach (var start in detectStart) { start.DetectStart(); }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            Reset();
        }
    }

    [ContextMenu("Reset")]
    private void Reset()
    {
        foreach (var resetable in _resetables) { resetable.Reset(); }
        OnReset?.Invoke();
    }

    public static List<IReset> FindAllComponents<IReset>(bool includeInactive)
    {
        List<IReset> results = new List<IReset>();
        MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>(includeInactive);
        foreach (var monoBehaviour in monoBehaviours)
        {
            if (monoBehaviour is IReset resetable)
            {
                results.Add(resetable);
            }
        }
        return results;
    }

    public void ResetAfterSeconds(float seconds)
    {
        StartCoroutine(ResetAfterSecondsCoroutine(seconds));
    }

    public IEnumerator ResetAfterSecondsCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Reset();
    }
}

