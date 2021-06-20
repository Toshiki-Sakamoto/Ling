using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute()]
	public class ES3UserType_AINormalAttack : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_AINormalAttack() : base(typeof(Ling.AI.Attack.AINormalAttack)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Ling.AI.Attack.AINormalAttack)obj;
			
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Ling.AI.Attack.AINormalAttack)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_AINormalAttackArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_AINormalAttackArray() : base(typeof(Ling.AI.Attack.AINormalAttack[]), ES3UserType_AINormalAttack.Instance)
		{
			Instance = this;
		}
	}
}