using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class AfterTimerEvent : MonoBehaviour
{
    public UnityEvent OnTimerEnd;
    [SerializeField] private float _timerDuration = 3f;
    private bool _timerFinished = false;
    private void OnEnable()
    {
        StartTimer(_timerDuration);
    }
    public void StartTimer(float seconds)
    {
        if(_timerFinished) return;

        StartCoroutine(Timer(seconds));
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnTimerEnd?.Invoke();
        _timerFinished = true;
    }
}