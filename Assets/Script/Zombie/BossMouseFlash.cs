using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BossMouseFlash : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private Material bossMouseMat;
	private Material defaultMat;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		defaultMat = spriteRenderer.material;
		AssetDatabase.Refresh();
		Shader bossMouseShader = Shader.Find("Custom/BossMouseShader");
		if (bossMouseShader == null)
		{
			Debug.LogError("BossMouseShader not found! Ensure it's present and correctly named in the project.");
			return;
		}
		bossMouseMat = new Material(bossMouseShader);
		spriteRenderer.material = bossMouseMat;
	}

	public void FlashWhite(float strength = 1.0f)
	{
		bossMouseMat.SetFloat("_BeAttack", strength);
	}

	public void ResetFlash()
	{
		bossMouseMat.SetFloat("_BeAttack", 0f);
	}
}
