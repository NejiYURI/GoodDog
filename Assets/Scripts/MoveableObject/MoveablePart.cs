using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePart : MonoBehaviour
{
    protected DogController _player;
    public void Setup(DogController i_player)
    {
        _player = i_player;
    }
    private void Start()
    {
        SetMovement(false);
    }
    public virtual void SetMovement(bool i_isOn)
    {
        
    }
}
