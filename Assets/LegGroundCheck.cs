using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegGroundCheck : MonoBehaviour
{
    public LegController LegController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LegController) LegController.OnFeetCollisionEnter(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (LegController) LegController.OnFeetCollisionExit(collision);
    }
}
