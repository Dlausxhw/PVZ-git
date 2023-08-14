using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.ComponentModel;
using UnityEngine.Events;
using System.Linq;


[CustomPropertyDrawer(typeof(PlantType))]
public class PlantEnumDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		PlantType previousValue = (PlantType)property.enumValueIndex;
		EditorGUI.PropertyField(position, property, label);
		if (previousValue != (PlantType)property.enumValueIndex)
		{
			Card cardComponent = property.serializedObject.targetObject as Card;
			if (cardComponent != null)
			{
				// Handle other properties
				SerializedProperty darkBgProperty = property.serializedObject.FindProperty("darkBg");
				if(darkBgProperty != null)
					darkBgProperty.objectReferenceValue = cardComponent.transform.Find("dark").gameObject;
				SerializedProperty progressBarProperty = property.serializedObject.FindProperty("progressBar");
				if(progressBarProperty != null)
					progressBarProperty.objectReferenceValue = cardComponent.transform.Find("progress").gameObject;
				SerializedProperty plantPrefabProperty = property.serializedObject.FindProperty("plantPrefab");
				AssetDatabase.Refresh();
				// Update values based on enum
				switch ((PlantType)property.enumValueIndex)
				{
					case PlantType.customize:
						break;
					case PlantType.Peashooter:
						SerializedProperty intervalProperty = property.serializedObject.FindProperty("interval");
						SerializedProperty consumeProperty = property.serializedObject.FindProperty("consume");
						if(intervalProperty != null) intervalProperty.floatValue = 7.5f;
						if(consumeProperty != null) consumeProperty.intValue = 100;
						if(plantPrefabProperty != null)
							plantPrefabProperty.objectReferenceValue = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Peashooter.prefab");
						break;

					case PlantType.SunFlower:
						SerializedProperty intervalPropertySF = property.serializedObject.FindProperty("interval");
						SerializedProperty consumePropertySF = property.serializedObject.FindProperty("consume");
						if(intervalPropertySF != null) intervalPropertySF.floatValue = 4.5f;
						if(consumePropertySF != null) consumePropertySF.intValue = 50;
						if(plantPrefabProperty != null)
							plantPrefabProperty.objectReferenceValue = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/SunFlower.prefab");
						break;
				}
			}
		}
	}
}
