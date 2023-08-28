using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Zombie), true)]  // ע�����true�������������Զ���༭��Ҳ����������
public class ZombieEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Zombie zombie = target as Zombie;  // ʹ��as������

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
