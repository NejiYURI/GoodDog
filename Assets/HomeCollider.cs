using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeCollider : HitInteractiveObject
{
    protected override void OnPlayerHit()
    {
        if (GameEventManager.instance)
            GameEventManager.instance.StageClear.Invoke();
    }
}
