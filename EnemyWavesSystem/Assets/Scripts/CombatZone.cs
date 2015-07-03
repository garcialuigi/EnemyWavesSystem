using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// This class represents a combat zone.
/// Contains the rules about the activation of a combat zone.
/// In this case, the class test its object position against the camera,
/// lock the camera when the combat zone is activated, and unlock the camera when 
/// it is done.
/// For each fight zone, you need this object, and a main child object derived from CombatBaseObject.
/// When this main child is done, than the Combat Zone is done.
/// This main child is usually a group.
/// </summary>
[ExecuteInEditMode]
public class CombatZone : MonoBehaviour
{
    /// <summary>
    /// Reference to the camera.
    /// </summary>
    [SerializeField]
    private MyCamera theCamera;

    /// <summary>
    /// Reference to the main child object.s
    /// </summary>
    [SerializeField]
    private CombatBaseObject mainChildObject;

    private bool activated;

    private bool done;

    private void Awake()
    {
        activated = false;
        done = false;
    }

    private void Update()
    {
        if (Application.isPlaying && !done && !activated)
        {
            // test the position against the camera
            if (theCamera.transform.position.x > transform.position.x)
            {
                // lock the camera
                theCamera.enabled = false;
                Activate();
            }
        }
    }

    public void Activate()
    {
        activated = true;
        // register the main child Done delegate
        mainChildObject.OnDone += MainChildObjectOnDone;
        mainChildObject.Activate();
    }

    private void MainChildObjectOnDone(CombatBaseObject obj)
    {
        done = true;
        End();
    }

    private void End()
    {
        // unlock the camera
        theCamera.enabled = true;
        gameObject.SetActive(false);
        enabled = false;
    }

    public bool IsDone()
    {
        return done;
    }

    public bool IsActivated()
    {
        return activated;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(0.2f, 1, 0.2f));
    }
}
