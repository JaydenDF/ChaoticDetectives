using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class UIChanceEvent : MonoBehaviour {
    [Header("UI Elements")]
    [SerializeField] private GameObject _objectToEnable;
    [SerializeField] private TextMeshPro _neededRoll;
    [SerializeField] private TextMeshPro _outcome;

    [Space(10)]
    [Header("Animation")]
    [SerializeField] private float _animDuration = 1f;
    [SerializeField] private float _animationInterval = 0.5f;
    [SerializeField] private float _waitTime = 1f;

    private ChanceEvent _chanceEvent;
    private ChanceEventStarter _chanceEventStarter;
    private uint _neededRollValue => _chanceEvent.neededRoll;

    private void OnEnable() {
        ChanceEventStarter.OnChanceEvent += OnChanceEvent;
    }

    private void OnDisable() {
        ChanceEventStarter.OnChanceEvent -= OnChanceEvent;
    }

    private void Awake() {
        DisableUI();
    }

    private void OnChanceEvent(object sender, ChanceEvent e)
    {
        Debug.Log("Chance event: " + e.name);
        _objectToEnable.SetActive(true);

        _chanceEvent = e;
        if(sender is ChanceEventStarter chanceEventStarter)
        {
            _chanceEventStarter = chanceEventStarter;
        }

        _neededRoll.text = _neededRollValue.ToString();

        _outcome.text = "0";
    }

    public void Roll()
    {
        int roll = UnityEngine.Random.Range((int)_chanceEvent.minRollPossible, (int)_chanceEvent.maxRollPossible);
        StartCoroutine(AnimateRoll(roll, _animDuration, _animationInterval, _waitTime));
    }

    private IEnumerator AnimateRoll(int roll, float animDuration = 1f,float animationInterval = 0.5f, float waitTime = 0.5f)
    {
        float elapsedTime = 0;
        while (elapsedTime*100 < animDuration)
        {
            Debug.Log("elapsedTime: " + elapsedTime);
            _outcome.text = UnityEngine.Random.Range((int)_chanceEvent.minRollPossible, (int)_chanceEvent.maxRollPossible).ToString();
            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(animationInterval);
        }

        _outcome.text = roll.ToString();
        yield return new WaitForSeconds(waitTime);
        _outcome.color = roll < _neededRollValue ? Color.red : Color.green;
        yield return new WaitForSeconds(waitTime);
        _chanceEventStarter.OnUIRollled((uint)roll);

        DisableUI();
    }

    private void DisableUI()
    {
        _neededRoll.text = "0";
        _outcome.text = "0";
        _chanceEvent = null;
        _chanceEventStarter = null;
        _objectToEnable.SetActive(false);
    }
}