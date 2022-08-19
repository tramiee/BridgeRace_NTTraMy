using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasPauseGame : UICanvas
{
   public void HomeButton()
    {
        SimplePool.ReleaseAll();
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        SceneManager.LoadScene("MainMenu");
        Close();
    }

    public void ResumeButton()
    {
        UIManager.Ins.OpenUI(UIID.UICGamePlay);
        Close();
    }
}
