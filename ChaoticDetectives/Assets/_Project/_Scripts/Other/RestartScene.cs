using UnityEngine;

public class RestartScene : MonoBehaviour {

    [ContextMenu("Restart")]
    public void Restart() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void RestartAfterSeconds(float seconds) {
        StartCoroutine(RestartAfterSecondsCoroutine(seconds));
    }

    private System.Collections.IEnumerator RestartAfterSecondsCoroutine(float seconds) {
        yield return new WaitForSeconds(seconds);
        Restart();
    }

    private void Update() {
        if(Input.GetKeyDown("r"))
        {
            Restart();
        }
    }
}