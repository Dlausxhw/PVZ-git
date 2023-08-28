using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
	private  AudioSource audioSource;
	private Dictionary<string, AudioClip> dictAudio;

	private void Awake()
	{
		Instance = this;
		audioSource = GetComponent<AudioSource>();
		dictAudio = new Dictionary<string, AudioClip>();
	}
	public AudioClip LoadAudio(string path) => (AudioClip)Resources.Load(path);
	private AudioClip GetAudio(string path)
	{
		if (!dictAudio.ContainsKey(name))
			dictAudio[path] = LoadAudio(path);
		return dictAudio[path];
	}
	public void PlayBGM(string name, float volume = 1.0f)
	{
		audioSource.Stop();
		audioSource.clip = GetAudio(name);
		audioSource.loop = true;
		audioSource.Play();
	}
	public void StopBGM(){
		audioSource.loop = false;
		audioSource.Stop();
	}
	public void PlaySound(string path, float volume = 1.0f)
	{
		audioSource.PlayOneShot(LoadAudio(path), volume);
	}
	public IEnumerator PlayThenCallback(float delay, System.Action callback)
	{
		yield return new WaitForSeconds(delay);
		callback?.Invoke();
	}

	public void PlaySoundCallBack(string name, System.Action onEnd, float volume = 1.0f)
	{
		PlaySound(name, volume);
		StartCoroutine(PlayThenCallback(GetAudio(name).length, onEnd));
	}

	public void PlaySoundTimeCallback(string name, float seconds, System.Action callback, float volume = 1.0f)
	{
		PlaySound(name, volume);
		StartCoroutine(PlayThenCallback(seconds, callback));
	}

	public float GetCurrentTime() => audioSource.time;

	public float GetRemainingTime() => audioSource.clip.length - audioSource.time;

	public float GetClipLength() => audioSource.clip.length;

	public void FadeOutBGMAndPlayNewOne(string newBgmPath)
	{
		// 使用 DOTween 将音量逐渐降低到 0
		DOTween.To(() => audioSource.volume, x => audioSource.volume = x, 0, 1f)
			.OnComplete(() => {
				PlayBGM(newBgmPath); // 播放新的 BGM
				DOTween.To(() => audioSource.volume, x => audioSource.volume = x, 1f, 0f);
				//audioSource.volume = 1.0f; // 将音量重置回 1
			});
	}

	public void OkReadyStartSound(System.Action onEnd, GameObject StartText, float volume = 1.0f)
	{
		PlaySound(Globals.S_Relllsetplant, volume);
		var startText = new Dictionary<string, GameObject>() {
			{"Ok", StartText.transform.Find("Ok").gameObject},
			{"Ready", StartText.transform.Find("Ready").gameObject},
			{"Start", StartText.transform.Find("Start").gameObject}
		};
		StartCoroutine(PlayThenCallback(0f, () => {
			startText["Ok"].SetActive(true);
			startText["Ready"].SetActive(false);
			startText["Start"].SetActive(false);
		}));
		StartCoroutine(PlayThenCallback(0.5f, () => {
			startText["Ok"].SetActive(false);
			startText["Ready"].SetActive(true);
			startText["Start"].SetActive(false);
		}));
		StartCoroutine(PlayThenCallback(1f, () => {
			startText["Ok"].SetActive(false);
			startText["Ready"].SetActive(false);
			startText["Start"].SetActive(true);
		}));
		StartCoroutine(PlayThenCallback(GetAudio(Globals.S_Relllsetplant).length, 
		() => {
			startText["Ok"].SetActive(false);
			startText["Ready"].SetActive(false);
			startText["Start"].SetActive(false);
		}));
		StartCoroutine(PlayThenCallback(GetAudio(Globals.S_Relllsetplant).length, onEnd));
	}
}
