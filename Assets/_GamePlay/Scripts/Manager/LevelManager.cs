using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
   public void Win()
    {
        UIManager.Ins.CloseUI(UIID.UICGamePlay);
        UIManager.Ins.OpenUI(UIID.UICVictory);
    }

    public void Fail()
    {
        UIManager.Ins.CloseUI(UIID.UICGamePlay);
        UIManager.Ins.OpenUI(UIID.UICFail);
    }
}
