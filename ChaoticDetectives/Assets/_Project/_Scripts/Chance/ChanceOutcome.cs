using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class ChanceOutcome
{
    public Sprite sprite;
    public UnityEvent OutcomeEvent;
    public bool isPermanent;

    //whenever we get to stats we will add a stat modifier here
}