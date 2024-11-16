using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamagePayload 
{
    public enum Severity{
        soft = 1,
        strong = 2
    }
    public float damage;
    public Vector3 position;
    public Severity severity;
}
