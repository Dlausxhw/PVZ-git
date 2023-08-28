using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : BasePanel
{
    public Button btnChangeUser;
	private void Awake()
	{
		btnChangeUser.onClick.AddListener(OnBtnChangeUser);
	}

	private void OnBtnChangeUser()
	{
		BaseUIManager.Instance.OpenPanel(UIConst.UserPanel);
	}
}
