using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Zombie), true)]  // 注意添加true参数，这允许自定义编辑器也能用于子类
public class ZombieEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Zombie zombie = target as Zombie;  // 使用as操作符

		if (zombie == null)
			return;

		// Display default inspector properties
		DrawDefaultInspector();

		// Display health bar
		EditorGUILayout.Space(10);
		EditorGUILayout.LabelField("Health Bar:");
		float healthPercentage = zombie.currentHP / zombie.healthPoint;
		EditorGUI.ProgressBar(EditorGUILayout.GetControlRect(), healthPercentage, $"{zombie.currentHP}/{zombie.healthPoint}");
		EditorGUILayout.Space(10);
	}
}
