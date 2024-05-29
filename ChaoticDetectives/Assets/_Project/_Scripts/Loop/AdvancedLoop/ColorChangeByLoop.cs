using UnityEngine;

public class ColorChangeByLoop : MonoBehaviour, ILoop
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public bool persist = false;
    public void Loop(Transform targetLoop)
    {
        if(persist == false) {return;}
        Debug.Log("Looping");

        Color newColor = new Color(Random.value, Random.value, Random.value);
        spriteRenderer.color = newColor;
        this.transform.parent = targetLoop;
    }
}