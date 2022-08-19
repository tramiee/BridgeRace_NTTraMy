using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMainMenu : UICanvas
{
    public void NewGameButton()
    {
        PlayerPrefs.SetInt("Level", 1);
        UIManager.Ins.OpenUI(UIID.UICGamePlay);
        SceneManager.LoadScene("Level01");
        Close();
    }

    public void ContinuesButton()
    {
        int currentLevel = PlayerPrefs.GetInt("Level");
        UIManager.Ins.OpenUI(UIID.UICGamePlay);
        SceneManager.LoadScene(currentLevel);
        Close();

    }
    public void QuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
