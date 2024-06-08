using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUIAnimations : MonoBehaviour {
  
    private CharacterSO _currentCharacterSO;
    private GameObject[] _children => GetChildren();

    private GameObject[] GetChildren()
    {
        var children = new List<GameObject>();
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
        return children.ToArray();
    }

    private void Awake() {
    }
    public void MakeFirstSibling(int index)
    {
        var children = _children;

        for (int i = 0; i < _children.Length; i++)
        {
            if (i == index)
            {
                children[i].transform.SetAsFirstSibling();
            }
        }
    }

    public void SetCharacterToCurrentSO()
    {
        var firstChild = _children[0];
        CharacterUI characterUI = firstChild.GetComponent<CharacterUI>();
        characterUI.characterSO = _currentCharacterSO;
    }
}