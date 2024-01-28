using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string StartScene;
    private string ContinueScene;
    public GameObject ContinueButton;
    public GameObject QuitButton;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_WEBGL
        if (QuitButton) QuitButton.SetActive(false);
#endif
        if (AudioController.instance && AudioController.instance.IsBGMPlaying())
        {
            AudioController.instance.StopBgm();
        }
        GetContinue();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(StartScene);

    }

    void GetContinue()
    {
        string stage = PlayerPrefs.GetString("Stage");
        if (!string.IsNullOrEmpty(stage))
        {
            ContinueScene = stage;
            if (ContinueButton) ContinueButton.SetActive(true);
        }
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(ContinueScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
