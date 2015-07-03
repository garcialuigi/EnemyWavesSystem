using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Superclass of all the child objects of a Combat Zone.
/// Groups also derive from this.
/// </summary>
public class CombatBaseObject : MonoBehaviour
{
    private bool initialized = false;
    
    private bool activated = false;
    
    private bool done = false;

    /// <summary>
    /// Reference to the combat zone(...parent).
    /// </summary>
    protected CombatZone combateZone;

    /// <summary>
    /// Channel delegate, called by the Done function.
    /// </summary>
    public Action<CombatBaseObject> OnDone;

    /// <summary>
    /// Channel delegate, called by the Activate function.
    /// </summary>
    public Action<CombatBaseObject> OnActivate;

    public void Start()
    {
        combateZone = FindTheCombatZone();
        Initialize();
    }

    protected virtual void Initialize()
    {
        initialized = true;
    }

    public void Activate()
    {
        if (!activated)
        {
            activated = true;
            // call the channel
            if (OnActivate != null)
            {
                OnActivate(this);
            }
        }
    }

    protected void Done()
    {
        done = true;
        // call the channel
        if (OnDone != null)
        {
            OnDone(this);
        }
    }

    public bool IsInitialized()
    {
        return initialized;
    }

    public bool IsActivated()
    {
        return activated;
    }

    public bool IsDone()
    {
        return done;
    }

    private CombatZone FindTheCombatZone()
    {
        CombatZone theCombatZoneObject = null;
        Transform cursor = gameObject.transform;
        while (theCombatZoneObject == null)
        {
            if (cursor == null)
            {
                break;
            }
            theCombatZoneObject = cursor.GetComponent<CombatZone>();
            cursor = cursor.parent;
        }
        return theCombatZoneObject;
    }
}