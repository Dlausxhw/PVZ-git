using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class BaseUIManager
{
	private static BaseUIManager _instance;
	private Transform _uiRoot;
	// ·�������ֵ�
	private Dictionary<string, string> pathDict;
	// Ԥ�Ƽ������ֵ�
	private Dictionary<string, GameObject> prefabDict;
	// �Ѵ򿪽���Ļ����ֵ�
	public Dictionary<string, BasePanel> panelDict;

	public static BaseUIManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new BaseUIManager();
			}
			return _instance;
		}
	}

	public Transform UIRoot
	{
		get
		{
			if (_uiRoot == null)
			{
				_uiRoot = GameObject.Find("Canvas").transform;
				return _uiRoot;
			};
			return _uiRoot;
		}
	}

	private BaseUIManager()
	{
		InitDicts();
	}

	private void InitDicts()
	{
		prefabDict = new Dictionary<string, GameObject>();
		panelDict = new Dictionary<string, BasePanel>();

		pathDict = new Dictionary<string, string>()
		{
			{UIConst.MainMenuPanel, "Menu/MainMenuPanel"},
			{UIConst.UserPanel, "Menu/UserPanel"}
		};
	}

	public BasePanel OpenPanel(string name)
	{
		BasePanel panel = null;
		// ����Ƿ��Ѵ�
		if (panelDict.TryGetValue(name, out panel))
		{
			Debug.LogError("�����Ѵ�: " + name);
			return null;
		}

		// ���·���Ƿ�����
		string path = "";
		if (!pathDict.TryGetValue(name, out path))
		{
			Debug.LogError("�������ƴ��󣬻�δ����·��: " + name);
			return null;
		}

		// ʹ�û���Ԥ�Ƽ�
		GameObject panelPrefab = null;
		if (!prefabDict.TryGetValue(name, out panelPrefab))
		{
			string realPath = "Prefab/Panel/" + path;
			panelPrefab = Resources.Load<GameObject>(realPath) as GameObject;
			prefabDict.Add(name, panelPrefab);
		}

		// �򿪽���
		GameObject panelObject = GameObject.Instantiate(panelPrefab, UIRoot, false);
		panel = panelObject.GetComponent<BasePanel>();
		panelDict.Add(name, panel);
		panel.OpenPanel(name);
		return panel;
	}

	public bool ClosePanel(string name)
	{
		BasePanel panel = null;
		if (!panelDict.TryGetValue(name, out panel))
		{
			Debug.LogError("����δ��: " + name);
			return false;
		}

		panel.ClosePanel();
		// panelDict.Remove(name);
		return true;
	}

	// 	public void ShowTip(string tip)
	// 	{
	// 		MenuTipPanel menuTipPanel = OpenPanel(UIConst.MenuTipPanel) as MenuTipPanel;
	// 	menuTipPanel.InitTip(Globals.TipCreateOne);
	// }
}

public class UIConst
{
	// menu panels
	public const string AllCardPanel = "AllCardPanel";
	public const string MenuPanel = "MenuPanel";
	public const string MainMenuPanel = "MainMenuPanel";
	public const string MenuTipPanel = "MenuTipPanel";
	public const string NewUserPanel = "NewUserPanel";
	public const string UserPanel = "UserPanel";
	public const string ReNameUserPanel = "ReNameUserPanel";
}