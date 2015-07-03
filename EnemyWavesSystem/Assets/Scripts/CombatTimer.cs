using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CombatTimer : CombatBaseObject
{
    [Range(0.1f, 100)]
    public float seconds;

    protected override void Initialize()
    {
        OnActivate = ObjectOnActivated;
        base.Initialize();
    }

    private void ObjectOnActivated(CombatBaseObject obj)
    {
        StartCoroutine(CoroutineBehavior());
    }

    private IEnumerator CoroutineBehavior()
    {
        yield return new WaitForSeconds(seconds);
        Done();
    }
}


