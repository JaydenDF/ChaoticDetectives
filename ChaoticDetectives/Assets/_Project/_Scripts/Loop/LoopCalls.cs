using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
[RequireComponent(typeof(AudioSource))]
public class LoopCalls : MonoBehaviour
{
    public UnityEvent OnEndCallEnded;

    [SerializeField] private List<AudioClip> _calls = new List<AudioClip>();
    [SerializeField] private AudioClip _endCallGood;
    [SerializeField] private AudioClip _endCallBad;
    [SerializeField] private AudioClip _ringSound;
    private bool _goodEnding = false;
    private bool _canRecieveCall = true;
    private int _loopCount = 0;
    private bool _startPlayed = false;

    private void OnEnable()
    {
        LoopMaster.OnLooped += Loop;
    }

    private void OnDisable()
    {
        LoopMaster.OnLooped -= Loop;
    }

    private void Loop()
    {
        _loopCount++;
    }

    public void PlayCall()
    {
        if (_loopCount < _calls.Count && !_startPlayed && _canRecieveCall) 
        {
            _startPlayed = true;
            _canRecieveCall = false;
            AudioSource.PlayClipAtPoint(_calls[_loopCount], Camera.main.transform.position);
        }
    }

    public void PlayEndCallGood()
    {
        //first play the ring sound and when it ends play the end call
        GetComponent<AudioSource>().PlayOneShot(_ringSound);
        StartCoroutine(PlayEndCallAfterSeconds(_ringSound.length, true));
    }

    private System.Collections.IEnumerator PlayEndCallAfterSeconds(float seconds, bool good)
    {
        yield return new WaitForSeconds(seconds);
        if (good)
        {
            GetComponent<AudioSource>().PlayOneShot(_endCallGood);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(_endCallBad);
        }

        StartCoroutine(EndCallAfterSeconds(_endCallGood.length));
    }

    private System.Collections.IEnumerator EndCallAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnEndCallEnded.Invoke();
    }

    public void PlayEndCallBad()
    {
        //first play the ring sound and when it ends play the end call
        GetComponent<AudioSource>().PlayOneShot(_ringSound);
        StartCoroutine(PlayEndCallAfterSeconds(_ringSound.length, false));
    }

    public void PlayEndCall()
    {
        _startPlayed = false;
        if (_goodEnding)
        {
            PlayEndCallGood();
        }
        else
        {
            PlayEndCallBad();
        }
    }

    public void SetGoodEnding(bool good)
    {
        _goodEnding = good;
    }

    public void CanRecieveCall(bool can)
    {
        _canRecieveCall = can;
    }

}