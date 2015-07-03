using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Class that represents a basic enemy in the Combat Zone System.
/// Die (gets done) when collide with the player.
/// </summary>
public class CombatZoneEnemy : CombatBaseObject
{
    [SerializeField]
    private GameObject theGraphicObject;

    [SerializeField]
    private Collider theCollider;

    [SerializeField]
    private ChaseBehavior chaseBehavior;

    protected override void Initialize()
    {
        theCollider.enabled = false;
        theGraphicObject.SetActive(false);
        chaseBehavior.enabled = false;

        OnActivate = ObjectOnActivated;
        OnDone += ObjectOnDone;

        base.Initialize();
    }

    private void ObjectOnActivated(CombatBaseObject obj)
    {
        theCollider.enabled = true;
        theGraphicObject.SetActive(true);
        chaseBehavior.enabled = true;
    }

    private void ObjectOnDone(CombatBaseObject obj)
    {
        gameObject.SetActive(false);
        theGraphicObject.SetActive(false);
        chaseBehavior.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            // I am Dead! done!
            Done();
        }
    }
}