using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailController : MoveablePart
{
    [SerializeField]
    private float TailForce = 10000;
    [SerializeField]
    private HingeJoint2D TailHingeJoint;
    public override void SetMovement(bool i_isOn)
    {
        if (!TailHingeJoint) return;
        SetHingeJoint(TailHingeJoint, (i_isOn ? 1.0f : -1.0f) * TailForce);
    }
    void SetHingeJoint(HingeJoint2D i_joint, float i_force)
    {
        JointMotor2D _motor = i_joint.motor;
        _motor.motorSpeed = i_force;
        i_joint.motor = _motor;
    }
}
