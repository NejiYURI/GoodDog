using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitInteractiveObject : MonoBehaviour
{
    protected DogController _player;

    [SerializeField]
    private LayerMask TargetLayer;

    public UnityEvent GameStartEvent;
    private void Start()
    {
        if (GameEventManager.instance) GameEventManager.instance.GameStart.AddListener(OnGameStart);
    }

    void OnGameStart()
    {
        GameStartEvent.Invoke();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionCheck(collision);
    }

    virtual public void CollisionCheck(Collision2D collision)
    {
        if ((TargetLayer & (1 << collision.gameObject.layer)) != 0)
        {
            if (DogController.instance)
            {
                _player = DogController.instance;
                OnPlayerHit();
            }
        }
    }

    virtual protected void OnPlayerHit()
    {

    }
}
