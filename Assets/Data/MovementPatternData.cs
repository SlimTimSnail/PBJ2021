using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPatternData : ScriptableObject
{
    [SerializeField]
    private AnimationCurve m_xForceOverTime;
    public AnimationCurve XForceOverTime => m_xForceOverTime;
    [SerializeField]
    private AnimationCurve m_yForceOverTime;
    public AnimationCurve YForceOverTime => m_yForceOverTime;

    [SerializeField]
    private float m_secondsLength;
    public float SecondsLength => m_secondsLength;
}
