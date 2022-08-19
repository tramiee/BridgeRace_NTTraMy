using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasVictory : UICanvas
{
    public void HomeButton()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        SceneManager.LoadScene("MainMenu");
        Close();
    }

    public void NextButton()
    {
        int currentLevel = PlayerPrefs.GetInt("Level") + 1;
        PlayerPrefs.SetInt("Level", currentLevel);
        SimplePool.ReleaseAll();
        UIManager.Ins.OpenUI(UIID.UICGamePlay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Close();
    }
}
