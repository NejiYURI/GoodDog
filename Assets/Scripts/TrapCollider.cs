using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollider : HitInteractiveObject
{
    public GameObject GameOverObj;
    public override void CollisionCheck(Collision2D collision)
    {
        base.CollisionCheck(collision);
        if (GameOverObj != null && collision.gameObject == GameOverObj)
        {
            if (StageManager.instance) StageManager.instance.OnGameOver();
        }
    }
    protected override void OnPlayerHit()
    {
        if (StageManager.instance) StageManager.instance.OnGameOver();
    }
}
