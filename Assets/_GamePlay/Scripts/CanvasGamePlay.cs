using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{
    public Text levelText;

    private void OnEnable()
    {
        levelText.text = "Level: " + PlayerPrefs.GetInt("Level").ToString();
    }
    public void PauseGameButton()
    {
        UIManager.Ins.OpenUI(UIID.UICPauseGame);
        Close();
    }
}
