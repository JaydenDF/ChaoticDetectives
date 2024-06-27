using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton

    private static SoundManager instance;


    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<SoundManager>();
                    singletonObject.name = "SoundManager (Singleton)";
                }
            }
            return instance;
        }
    }
    #endregion
    private AudioSource _audioSource;

    [SerializeField] private List<SoundFile> _soundFiles;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(string name)
    {
        SoundFile soundFile = _soundFiles.Find(s => s.name == name);
        if (soundFile == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        AudioSource.PlayClipAtPoint(soundFile.clip, Camera.main.transform.position);
    }
}

[System.Serializable]
public class SoundFile
{
    public string name;
    public AudioClip clip;
}