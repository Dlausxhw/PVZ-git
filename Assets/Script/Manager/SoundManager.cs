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
		audioSource.Play();
	}
	public void StopBGM() => audioSource.Stop();
	public void PlaySound(string path, float volume = 1.0f)
	{
		audioSource.PlayOneShot(LoadAudio(path), volume);
	}
}
