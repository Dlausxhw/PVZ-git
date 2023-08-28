using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPanel : BasePanel
{
    public Button BtnOk;
    public Button BtnCancel;
	private void Awake()
	{
		BtnOk.onClick.AddListener(OnBtnOk);
		BtnCancel.onClick.AddListener(OnBtnCancel);
	}

	private void OnBtnOk()
	{
		Debug.Log(">>>>>>>>>>>>>>>>>> on btn ok");
		ClosePanel();
	}
	private void OnBtnCancel()
	{
		Debug.Log(">>>>>>>>>>>>>>>>>> on btn cancel");
		ClosePanel();
	}
}
