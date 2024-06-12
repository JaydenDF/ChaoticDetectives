using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "CharacterSO", order = 0)]
public class CharacterSO : ScriptableObject
{
    public Sprite characterSprite;
    public string characterName;
    public string characterDescription;
    private const uint _maxStat = 27;
    private const uint _minStat = 0;
    public Stat[] stats =
     {
        new Stat { statType = StatType.Perception, value = 0 },
        new Stat { statType = StatType.Creativity, value = 0 }
    };

 
}
