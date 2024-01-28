using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DogController : MonoBehaviour
{
    public static DogController instance;
    [SerializeField]
    private bool AutoStart;
    [SerializeField]
    private Rigidbody2D _BodyRb;

    [SerializeField]
    private MoveablePart BackLegController;
    [SerializeField]
    private MoveablePart FrontLegController;
    [SerializeField]
    private MoveablePart TailController;

    [SerializeField]
    private float uprightspringStrength = 30f;
    [SerializeField]
    private float uprightspringDamper = 3f;

    [SerializeField]
    private float GravityScale = 1.2f;
    [SerializeField]
    private float GravityMulVal = 1;

    private bool CanInput;

    private void Awake()
    {
        instance = this;
        if (BackLegController) BackLegController.Setup(this);
        if (FrontLegController) FrontLegController.Setup(this);
        if (TailController) TailController.Setup(this);
    }
    private void Start()
    {
        if (GameEventManager.instance)
        {
            GameEventManager.instance.GameStart.AddListener(OnGameStart);
            GameEventManager.instance.GameOver.AddListener(OnGameOver);
            GameEventManager.instance.StageClear.AddListener(OnStageClear);
        }
        if (AutoStart) OnGameStart();
    }
    private void FixedUpdate()
    {
        KeepUpright();
        GravityMul();
    }

    void OnGameStart()
    {
        SetCanInput(true);
    }

    void OnGameOver()
    {
        SetCanInput(false);
    }

    void OnStageClear()
    {
        SetCanInput(false);
    }

    private void SetCanInput(bool i_canInput)
    {
        CanInput = i_canInput;
    }

    public void LegOutInput(InputAction.CallbackContext context)
    {
        if (context.performed && CanInput)
        {
            if (BackLegController) BackLegController.SetMovement(true);
        }
        else if (context.canceled)
        {
            if (BackLegController) BackLegController.SetMovement(false);
        }
    }

    public void Leg2OutInput(InputAction.CallbackContext context)
    {
        if (context.performed && CanInput)
        {
            if (FrontLegController) FrontLegController.SetMovement(true);
        }
        else if (context.canceled)
        {
            if (FrontLegController) FrontLegController.SetMovement(false);
        }
    }

    public void TailInput(InputAction.CallbackContext context)
    {
        if (context.performed && CanInput)
        {
            if (TailController) TailController.SetMovement(true);
        }
        else if (context.canceled)
        {
            if (TailController) TailController.SetMovement(false);
        }
    }

    void GravityMul()
    {
        if (!this._BodyRb) return;
        this._BodyRb.gravityScale = GravityScale * (this._BodyRb.velocity.y < 0 ? GravityMulVal : 1f);
    }
    void KeepUpright()
    {
        Quaternion _current = transform.rotation;
        Quaternion toGoal = ShortestRotation(Quaternion.identity, _current);
        Vector3 rotAxis;
        float rotDegrees;
        toGoal.ToAngleAxis(out rotDegrees, out rotAxis);
        rotAxis.Normalize();

        float rotRad = rotDegrees * Mathf.Deg2Rad;

        _BodyRb.AddTorque((rotAxis.z * (rotRad * uprightspringStrength)) - (_BodyRb.angularVelocity * uprightspringDamper));
    }


    public Quaternion ShortestRotation(Quaternion a, Quaternion b)
    {
        if (Quaternion.Dot(a, b) < 0) return a * Quaternion.Inverse(Multiply(b, -1));
        else return a * Quaternion.Inverse(b);
    }

    public Quaternion Multiply(Quaternion input, float scalar)
    {
        return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
    }

    public void AddForce(Vector2 i_force)
    {
        if (this._BodyRb) this._BodyRb.AddForce(i_force, ForceMode2D.Impulse);
    }

    public void LegOnGround(bool i_onGround)
    {
        GravityMulVal = Mathf.Clamp(GravityMulVal + (i_onGround ? -0.8f : 0.8f), 1, 3);
    }

    public void RestartInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (StageManager.instance) StageManager.instance.RestartLevel();
        }
    }
}
