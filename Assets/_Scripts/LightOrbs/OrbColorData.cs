using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OrbColorData : MonoBehaviour
{ 
    [SerializeField] [GradientUsage(true)]
    public Gradient rLightBeamGradient, gLightBeamGradient, yLightBeamGradient;
    
    [SerializeField] [GradientUsage(true)] 
    public Gradient rSparksGradient, gSparksGradient, ySparksGradient;
}
