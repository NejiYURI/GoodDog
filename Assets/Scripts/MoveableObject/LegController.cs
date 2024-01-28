using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MoveablePart
{
    [SerializeField]
    private LayerMask GroundLayers;
    [SerializeField]
    private float LegUpForce = 1500;
    [SerializeField]
    private float LegDownForce = 1500;
    [SerializeField]
    private float ForwardForce = 8f;
    [SerializeField]
    private HingeJoint2D HingeJoint_Up;
    [SerializeField]
    private HingeJoint2D HingeJoint_Down;

    [SerializeField]
    private AudioData LegAudio;

    [SerializeField]
    private AudioData OnGroundAudio;
    bool IsPlayingSound;
    private Coroutine OnGroundSoundCoroutine = null;
    private bool OnGround;
    public override void SetMovement(bool i_isOn)
    {
        if (!HingeJoint_Up || !HingeJoint_Down) return;
        SetHingeJoint(HingeJoint_Up, (i_isOn ? 1.0f : -1.0f) * LegUpForce);
        SetHingeJoint(HingeJoint_Down, (i_isOn ? -1.0f : 1.0f) * LegDownForce);
        if (i_isOn && AudioController.instance) AudioController.instance.PlaySound(LegAudio.clip, LegAudio.volume);
        if (OnGround && _player) _player.AddForce(Vector2.right * ForwardForce);
    }

    void SetHingeJoint(HingeJoint2D i_joint, float i_force)
    {
        JointMotor2D _motor = i_joint.motor;
        _motor.motorSpeed = i_force;
        i_joint.motor = _motor;
    }

    public void OnFeetCollisionEnter(Collision2D collision)
    {
        if (!OnGround && ((GroundLayers & (1 << collision.gameObject.layer)) != 0))
            SetOnGround(true);
    }

    public void OnFeetCollisionExit(Collision2D collision)
    {
        if (OnGround && ((GroundLayers & (1 << collision.gameObject.layer)) != 0))
            SetOnGround(false);
    }

    void SetOnGround(bool i_val)
    {
        OnGround = i_val;
        if (OnGround)
        {
            if (!IsPlayingSound)
            {
                IsPlayingSound = true;
                if (AudioController.instance) AudioController.instance.PlaySound(OnGroundAudio.clip, OnGroundAudio.volume);
            }
        }
        else if(this.isActiveAndEnabled)
        {
            if (OnGroundSoundCoroutine != null) StopCoroutine(OnGroundSoundCoroutine);
            OnGroundSoundCoroutine = StartCoroutine(GroundSoundCounter());
        }
        if (_player) _player.LegOnGround(OnGround);
    }

    IEnumerator GroundSoundCounter()
    {
        yield return new WaitForSeconds(1.0f);
        IsPlayingSound = false;
    }
}
