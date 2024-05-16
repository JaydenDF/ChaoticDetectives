using UnityEngine;

public interface IHighlitable
{
    void OnHoverEnter();
    void OnHoverExit();

}
public class Highlitable : MonoBehaviour, IHighlitable
{
    public void OnHoverEnter()
    {
        Debug.Log("Hovering! " + gameObject.name);
    }

    public void OnHoverExit()
    {
        Debug.Log("Not hovering! " + gameObject.name);
    }
}