using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarBad : MonoBehaviour
{
    public Sprite _badSprite;
    public Sprite _goodSprite;
    public List<GameObject> books = new List<GameObject>();
    private int index = 0;
    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        LoopMaster.OnLooped += Loop;
    }

    private void OnDestroy()
    {
        LoopMaster.OnLooped -= Loop;
    }

    private void Loop()
    {
        if (index < books.Count - 1)
        {
            books[index].SetActive(true);
            index++;
        }
    }
    public void BadEnding()
    {
        books[index].GetComponent<SpriteRenderer>().sprite = _badSprite;
    }

    public void GoodEnding()
    {
        books[index].GetComponent<SpriteRenderer>().sprite = _goodSprite;
    }
}
