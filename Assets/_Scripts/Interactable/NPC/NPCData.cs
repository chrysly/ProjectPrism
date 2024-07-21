using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Actor Data/NPC")]
public class NPCData : ScriptableObject
{
    [SerializeField] private string _npcName;
    [SerializeField] private List<Sprite> _imageSeq;

    // accessors
    public string NPCName => _npcName;
    public List<Sprite> ImageSeq => _imageSeq;

}
