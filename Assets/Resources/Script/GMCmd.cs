using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMCmd : BasePanel
{
	private void Start()
	{
		OpenMainMenu();
	}
	public static void OpenMainMenu()
    {
        BaseUIManager.Instance.OpenPanel(UIConst.MainMenuPanel);
    }
}
