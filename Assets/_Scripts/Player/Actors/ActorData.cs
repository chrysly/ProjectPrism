using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorData : ScriptableObject 
{
    [Header("Actor Details")]
    [SerializeField] private string actorName = "Empty";

    // Accessors
    public string ActorName => actorName;

    //test
    [SerializeField] private int test = 0;
    public int Test => test;
}
