using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CreditBox : HitInteractiveObject
{
    private bool IsShow;
    public Transform DialogueBox;
    private void Start()
    {
        if (DialogueBox)
            DialogueBox.DOScale(Vector3.zero, 0);
    }
    protected override void OnPlayerHit()
    {
        if (IsShow || !DialogueBox) return;
        StartCoroutine(ShowIenumerator());
    }

    IEnumerator ShowIenumerator()
    {
        IsShow = true;
        DialogueBox.DOScale(Vector3.one, 0.3f);
        yield return new WaitForSeconds(5);
        DialogueBox.DOScale(Vector3.zero, 0.3f);
        IsShow = false;
    }
}
