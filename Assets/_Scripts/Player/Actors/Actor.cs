using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent Actor Class
/// </summary>
public class Actor : MonoBehaviour 
{
    #region Data Attributes
    [SerializeField] private ActorData _data;
    #endregion Data Attributes

    #region Accessors
    public ActorData Data => Data;
    #endregion Accessors
}
