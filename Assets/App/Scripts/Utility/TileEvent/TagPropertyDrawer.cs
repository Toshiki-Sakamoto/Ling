// 
// TagPropertyDrawer.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.10.21.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

namespace Ling.Utility.TileEvent
{
	/// <summary>
	/// 
	/// </summary>
	[CustomPropertyDrawer(typeof(TagAttribute))]
	public class TagPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType == SerializedPropertyType.String)
			{
				EditorGUI.BeginProperty(position, label, property);

				var attrib = attribute as TagAttribute;

				if (attrib.UseDefaultTagFieldDrawer)
				{
					property.stringValue =
						EditorGUI.TagField(position, label, property.stringValue);
				}
				else
				{
					var tagList = new List<string>();

					tagList.Add("<All Tags allowed>");
					tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);

					var propertyString = property.stringValue;
					var index = -1;

					if (propertyString == "")
					{
						index = 0;
					}
					else
					{
						for (var i = 1; i < tagList.Count; ++i)
						{
							if (tagList[i] == propertyString)
							{
								index = i;
								break;
							}
						}
					}

					index = EditorGUI.Popup(position, label.text, index, tagList.ToArray());

					if (index >= 1)
					{
						property.stringValue = tagList[index];
					}
					else
					{
						property.stringValue = "";
					}

				}

				EditorGUI.EndProperty();
			}
		}
	}
}
#endif