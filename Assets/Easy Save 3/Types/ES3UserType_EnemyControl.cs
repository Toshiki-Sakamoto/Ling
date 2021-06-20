using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("_model")]
	public class ES3UserType_EnemyControl : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_EnemyControl() : base(typeof(Ling.Chara.EnemyControl)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Ling.Chara.EnemyControl)obj;
			
			writer.WritePrivateFieldByRef("_model", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Ling.Chara.EnemyControl)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "_model":
					reader.SetPrivateField("_model", reader.Read<Ling.Chara.EnemyModel>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_EnemyControlArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_EnemyControlArray() : base(typeof(Ling.Chara.EnemyControl[]), ES3UserType_EnemyControl.Instance)
		{
			Instance = this;
		}
	}
}