using UnityEngine;
using System.Collections;

public class CombatBaseObject : MonoBehaviour
{
    protected bool initialized = false;
    protected bool activated = false;
	protected bool done = false;
	protected CombatZone fightPoint;
	public delegate void OnDoneDelegate (CombatBaseObject fpobj);
	public OnDoneDelegate onDone;
	public delegate void OnActivatedDelegate (CombatBaseObject fpobj);
	public OnDoneDelegate onActivated;

	public virtual void Start ()
	{
		fightPoint = GetFightPoint ();
		Initialize ();
	}

	protected virtual void Initialize ()
	{
        initialized = true;
	}

	public void Activate ()
	{
		if (!activated) {
			activated = true;
			if (onActivated != null) {
				onActivated (this);
			}
		}
	}

	protected void Done ()
	{
		done = true;
		if (onDone != null) {
			onDone (this);
		}
	}

    public bool IsInitialized()
    {
        return initialized;
    }

	public bool IsActivated ()
	{
		return activated;
	}

	public bool IsDone ()
	{
		return done;
	}

	private CombatZone GetFightPoint ()
	{
		Transform cursor = gameObject.transform;
		while (true) {
			if (cursor == null) {
				return null;
			}

			if (cursor.GetComponent<CombatZone> () != null) {
				return cursor.GetComponent<CombatZone> ();
			}
			cursor = cursor.parent;
		}
	}
}

