using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public GameClearPanel GameClearPanel;

    [SerializeField]
    private bool IsGameOver;

    private bool IsStart;

    [SerializeField]
    private string NextStage;

    [SerializeField]
    private AudioData ClearAudio;
    [SerializeField]
    private AudioData FailAudio;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (AudioController.instance && !AudioController.instance.IsBGMPlaying())
        {
            AudioController.instance.PlayBgm();
        }
        if (GameEventManager.instance)
        {
            GameEventManager.instance.StageClear.AddListener(OnStageClear);
        }
        PlayerPrefs.SetString("Stage",SceneManager.GetActiveScene().name);
    }

    public void OnGameOver()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        if (GameClearPanel) GameClearPanel.PanelShow(false);
        if (GameEventManager.instance) GameEventManager.instance.GameOver.Invoke();
        if (AudioController.instance) AudioController.instance.PlaySound(FailAudio.clip, FailAudio.volume);
        Invoke("LevelRestart",3f);
    }

    void OnStageClear()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        if (GameClearPanel) GameClearPanel.PanelShow(true);
        if (GameEventManager.instance) GameEventManager.instance.StageClear.Invoke();
        if (AudioController.instance) AudioController.instance.PlaySound(ClearAudio.clip, ClearAudio.volume);
        Invoke("LoadLevel", 3f);
    }

    public void RestartLevel()
    {
        if (!IsGameOver)
            LevelRestart();
    }
    void LevelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(NextStage);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StartInput(InputAction.CallbackContext context)
    {
        if (context.performed && !IsStart)
        {
            IsStart = true;
            if (GameEventManager.instance) GameEventManager.instance.GameStart.Invoke();
            if (GameClearPanel) GameClearPanel.PanelHide();
        }
    }
}
