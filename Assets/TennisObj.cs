using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TennisObj : HitInteractiveObject
{
    public Vector2 StartForce;
    public void AddForce()
    {
        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().AddForce(StartForce, ForceMode2D.Impulse);
        }
    }
    protected override void OnPlayerHit()
    {
        if (GameEventManager.instance)
            GameEventManager.instance.StageClear.Invoke();
    }
}
