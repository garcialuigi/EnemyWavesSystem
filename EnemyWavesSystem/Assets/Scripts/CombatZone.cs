using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CombatZone : MonoBehaviour
{

    //=========================================================================== Delegates
    public delegate void OnFightPointDone();
    public OnFightPointDone onFightPointDone;
    //=====================================================================================

    [Header("Camera e area de luta")]
    public GameObject _camera;

    protected CombatBaseObject mainFpo;

    protected bool activated = false;

    protected bool done = false;

    void Awake()
    {
        activated = false;
        done = false;
    }

    public void Activate()
    {
        activated = true;

        mainFpo.onDone += MainFpoOnDone;
        mainFpo.Activate();
    }

    private void End()
    {
        gameObject.SetActive(false);
        enabled = false;
    }


    void Update()
    {
        if (done)
            return;


        if (Application.isPlaying)
        {
            if (!activated)
            {
                if (_camera.transform.position.x > transform.position.x)
                {
                    Activate();
                }
            }
        }
    }

    /// <summary>
    /// Callback de quando o fight point object principal foi acabado(done).
    /// </summary>
    protected void MainFpoOnDone(CombatBaseObject obj)
    {
        done = true;
        End();
    }

    public bool IsDone()
    {
        return done;
    }

    public bool IsActivated()
    {
        return activated;
    }

    //######################################################################################################################## 
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, new Vector3(0.2f, 1, 0.2f));
    }
}
