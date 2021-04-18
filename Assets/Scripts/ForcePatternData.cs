using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ForcePatternData : ScriptableObject
{
    [SerializeField]
    private AnimationCurve m_xForceOverTime;
    private AnimationCurve XForceOverTime => m_xForceOverTime;
    [SerializeField]
    private AnimationCurve m_yForceOverTime;
    private AnimationCurve YForceOverTime => m_yForceOverTime;

    [SerializeField]
    [Range(0.01f, 10f)]
    private float m_xForceFactor = 0.1f;
    [SerializeField]
    [Range(0.01f, 10f)]
    private float m_yForceFactor = 0.1f;

    [SerializeField]
    private float m_secondsLength;
    private float SecondsLength => m_secondsLength;

    [SerializeField]
    private bool m_loop;


    public Vector2 GetForceAtTime(float timeSinceSpawn)
    {
        float timeFraction = timeSinceSpawn % SecondsLength;

        float x = XForceOverTime.Evaluate(timeFraction);
        float y = YForceOverTime.Evaluate(timeFraction);

        return new Vector2(x * m_xForceFactor, y * m_yForceFactor);
    }
}
