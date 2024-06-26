using UnityEngine;
using UnityEngine.Events;

public class SoundPlayer : MonoBehaviour {
    public string soundName = "ObjectPickUp";

    public void PlaySound() {
        if(soundName == null) 
        {
            return;
        }
        SoundManager.Instance.PlaySound(soundName);
    }

    public void PlaySoundString(string soundTitle)
    {
        SoundManager.Instance.PlaySound(soundTitle);
    }
}