using System;
using UnityEngine;

public class ChanceEventStarter : MonoBehaviour, IInteractable
{
    public static EventHandler<ChanceEvent> OnChanceEvent;

    [SerializeField] private ChanceEvent _chanceEvent;
    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }    
    public void OnClick()
    {
        ChanceEvent();
    }

    public void OnHoverEnter()
    {
    }

    public void OnHoverExit()
    {
    }

    public void OnUIRollled(uint roll){
        ReactToOutcome(_chanceEvent.GetOutcomeFromRoll(roll));
    }
    protected void ChanceEvent()
    {
        OnChanceEvent?.Invoke(this,_chanceEvent);
    }
    protected void ReactToOutcome(ChanceOutcome outcome)
    {
        _spriteRenderer.sprite = outcome.sprite;
    }
}