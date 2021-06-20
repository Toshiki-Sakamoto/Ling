using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("_cellPosition", "_param", "Type", "Status", "CellIndex", "Dir")]
	public class ES3UserType_EnemyModel : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_EnemyModel() : base(typeof(Ling.Chara.EnemyModel)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Ling.Chara.EnemyModel)obj;
			
			writer.WritePrivateField("_cellPosition", instance);
			writer.WritePrivateField("_param", instance);
			writer.WritePrivateProperty("Type", instance);
			writer.WritePrivateProperty("Status", instance);
			writer.WritePrivateProperty("CellIndex", instance);
			writer.WritePrivateProperty("Dir", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Ling.Chara.EnemyModel)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "_cellPosition":
					reader.SetPrivateField("_cellPosition", reader.Read<Ling.Common.ReactiveProperty.Vector2IntReactiveProperty>(), instance);
					break;
					case "_param":
					reader.SetPrivateField("_param", reader.Read<Ling.Chara.CharaModel.Param>(), instance);
					break;
					case "Type":
					reader.SetPrivateProperty("Type", reader.Read<Ling.Const.EnemyType>(), instance);
					break;
					case "Status":
					reader.SetPrivateProperty("Status", reader.Read<Ling.Chara.CharaStatus>(), instance);
					break;
					case "CellIndex":
					reader.SetPrivateProperty("CellIndex", reader.Read<System.Int32>(), instance);
					break;
					case "Dir":
					reader.SetPrivateProperty("Dir", reader.Read<Ling.Common.ReactiveProperty.Vector2IntReactiveProperty>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_EnemyModelArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_EnemyModelArray() : base(typeof(Ling.Chara.EnemyModel[]), ES3UserType_EnemyModel.Instance)
		{
			Instance = this;
		}
	}
}