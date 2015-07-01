using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CombatGroup : CombatBaseObject
{
    [Header("Limite de FPObjects ativos")]
    public int limit;

    public bool activateAll = false;

    private List<CombatBaseObject> objects = new List<CombatBaseObject>();
    private int cursor = -1; // cursor
    private int doneCount = 0; // contador de quantos ja estao done

    protected override void Initialize()
    {
        objects = new List<CombatBaseObject>();

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                CombatBaseObject temp = child.gameObject.GetComponent<CombatBaseObject>();
                temp.onActivated = ObjectOnActivated;
                temp.onDone = ObjectOnDone;
                objects.Add(temp);
            }
        }

        limit = Mathf.Clamp(limit, 1, objects.Count);
        limit = activateAll ? objects.Count : limit;
        base.Initialize();
    }

    private void ObjectOnActivated(CombatBaseObject obj)
    {
        cursor = -1;
        doneCount = 0;

        for (int i = 0; i < limit; i++)
        {
            ActivateNext();
        }
    }

    private void ObjectOnDone(CombatBaseObject obj)
    {
        doneCount++; // incrementa o contador de done

        if (doneCount == objects.Count)
        { // se o contador de done eh igual ao numero de filhos, entao todos estao "feitos"
            Done(); // fim de comportamento
            return;
        }

        if (cursor + 1 < objects.Count)
        {
            ActivateNext(); // chama a rotina para ativar o proximo baseado no cursor
        }
    }

    private void ActivateNext()
    {
        cursor++;
        if (cursor < objects.Count)
        {
            objects[cursor].Activate();
        }
    }
}

