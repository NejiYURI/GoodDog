using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameClearPanel : MonoBehaviour
{
    [SerializeField,TextArea(3,5)]
    private string SuccessTitle = "Good Dog!";
    [SerializeField]
    private string FailedTitle = "Bad Dog...";

    [SerializeField]
    private TextMeshProUGUI MainLabel;
    private void Start()
    {
        
    }
    public void PanelHide()
    {
        this.transform.DOScale(Vector3.zero, 0.4f);
    }
    public void PanelShow(bool IsSuccess)
    {
        this.transform.DOScale(Vector3.one, 0.4f);
        if (MainLabel) MainLabel.text = IsSuccess ? SuccessTitle : FailedTitle;
    }
}
