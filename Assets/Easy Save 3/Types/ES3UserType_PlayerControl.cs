using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("_model", "_view")]
	public class ES3UserType_PlayerControl : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PlayerControl() : base(typeof(Ling.Chara.PlayerControl)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Ling.Chara.PlayerControl)obj;
			
			writer.WritePrivateFieldByRef("_model", instance);
			writer.WritePrivateFieldByRef("_view", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Ling.Chara.PlayerControl)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "_model":
					reader.SetPrivateField("_model", reader.Read<Ling.Chara.PlayerModel>(), instance);
					break;
					case "_view":
					reader.SetPrivateField("_view", reader.Read<Ling.Chara.PlayerView>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_PlayerControlArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerControlArray() : base(typeof(Ling.Chara.PlayerControl[]), ES3UserType_PlayerControl.Instance)
		{
			Instance = this;
		}
	}
}