using UnityEngine;

public class ActivateEndScreen : MonoBehaviour {
    public GameObject endScreen;
    public void Activate(int seconds) {
        StartCoroutine(ActivateAfterSecondsCoroutine(seconds));
    }

    private System.Collections.IEnumerator ActivateAfterSecondsCoroutine(int seconds = 3) {
        yield return new WaitForSeconds(seconds);
        endScreen.SetActive(true);
    }
}