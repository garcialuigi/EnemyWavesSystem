using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Represents a group.
/// This object orchestrate the activation of child objects.
/// </summary>
public class CombatGroup : CombatBaseObject
{
    /// <summary>
    /// How many objects it will tend to maintain activated.
    /// </summary>
    [SerializeField]
    private int limit;

    /// <summary>
    /// If yes, this will make the group ignore the limite variable, and activate all the child objects.
    /// </summary>
    [SerializeField]
    private bool activateAll = false;

    private List<CombatBaseObject> objects = new List<CombatBaseObject>();

    private int cursor = -1;

    private int doneCount = 0;

    protected override void Initialize()
    {
        // channel with the base class
        OnActivate = ObjectOnActivated;

        // setup the child objects
        objects = new List<CombatBaseObject>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                CombatBaseObject temp = child.gameObject.GetComponent<CombatBaseObject>();
                // register the done callback
                temp.OnDone += ChildObjectOnDone;
                objects.Add(temp);
            }
        }

        limit = Mathf.Clamp(limit, 1, objects.Count);
        limit = activateAll ? objects.Count : limit;
        base.Initialize();
    }

    /// <summary>
    /// The first activation.
    /// Called by the channel with the base class.
    /// </summary>
    private void ObjectOnActivated(CombatBaseObject obj)
    {
        cursor = -1;
        doneCount = 0;

        for (int i = 0; i < limit; i++)
        {
            objects[++cursor].Activate();
        }
    }

    /// <summary>
    /// Channel callback with every child object.
    /// When every child object gets done, this function will be called.
    /// </summary>
    /// <param name="obj"></param>
    private void ChildObjectOnDone(CombatBaseObject obj)
    {
        if (++doneCount < objects.Count)
        {
            // call the next
            if (++cursor < objects.Count)
            {
                objects[cursor].Activate();
            }
        }
        else
        {
            // this group is done
            Done();
        }
    }
}

