using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingPanel : MonoBehaviour
{
    public Slider slider;
    public GameObject BtnStart;
    public float curPorgress;
    public float loadingTime = 2f;
    public bool really = false;
    AsyncOperation operation;
    // Start is called before the first frame update
    void Start()
    {
        BtnStart.SetActive(false);
        BtnStart.GetComponent<Button>().onClick.AddListener(OnBtnStart);
        curPorgress = 0.0f;
        slider.value = curPorgress;
        if (really)
        {
            operation = SceneManager.LoadSceneAsync("Menu");
            operation.allowSceneActivation = false;
        }
    }

    private void OnBtnStart()
    {
        if(!really)
			SceneManager.LoadScene("Menu");
        else operation.allowSceneActivation = true;
		DOTween.Clear();

	}
    private void OnSliderValueChange(float value)
    {
        slider.value = value;
        if(value >= 1.0f)
			BtnStart.SetActive(true);
    }
	private void Update()
	{
        if(!really)
        {
			curPorgress += Time.deltaTime / loadingTime;
			if (curPorgress > 1.0f)
				curPorgress = 1;
			OnSliderValueChange(curPorgress);
		} else {
            curPorgress = Mathf.Clamp01(operation.progress / 0.9f);
            OnSliderValueChange(curPorgress);
        }
		
	}
}
