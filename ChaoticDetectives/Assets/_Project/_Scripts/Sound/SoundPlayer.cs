using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    public string soundName;


    public void PlaySound() {
        SoundManager.Instance.PlaySound(soundName);
    }
}