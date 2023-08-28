using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public static float originPosX = 0;
    public static float previewPosX = 334;
    public static float duration = 1f;
    public static CameraController Instance;
	private void Awake()
	{
		Instance = this;
	}
	public Tweener startPreview()
    {
        ChooseCardPanel.Instance.panelUp();
        ChooseCardPanel.Instance.startPanelPreview();
        AllCardPanel.Instance.startPanelPreview();
        return transform.DOMoveX(previewPosX, duration);
    }
    public Tweener endPreview()
    {
        ChooseCardPanel.Instance.endPanelPreview();
        AllCardPanel.Instance.endPanelPreview();
        return transform.DOMoveX(originPosX, duration);
    }
}
