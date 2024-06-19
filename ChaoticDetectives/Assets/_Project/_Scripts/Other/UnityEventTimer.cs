using UnityEngine;

public class UnityEventTimer : MonoBehaviour {
    public UnityEngine.Events.UnityEvent OnTimerStart;
    public UnityEngine.Events.UnityEvent OnTimerEnd;
    public float delayTime;

    private void OnEnable() {
        StartCoroutine(StartTimer());
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    private System.Collections.IEnumerator StartTimer() {
        yield return new WaitForSeconds(delayTime);
        OnTimerEnd.Invoke();
    }

    public void StartTimerNow() {
        StartCoroutine(StartTimer());
    }
}