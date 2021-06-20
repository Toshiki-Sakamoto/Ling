using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("_destination", "_destinationRoutes", "_nextMovePos", "_prevPos", "_waitCount", "Param1")]
	public class ES3UserType_AINormalTracking : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_AINormalTracking() : base(typeof(Ling.AI.Move.AINormalTracking)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Ling.AI.Move.AINormalTracking)obj;
			
			writer.WritePrivateField("_destination", instance);
			writer.WritePrivateField("_destinationRoutes", instance);
			writer.WritePrivateField("_nextMovePos", instance);
			writer.WritePrivateField("_prevPos", instance);
			writer.WritePrivateField("_waitCount", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Ling.AI.Move.AINormalTracking)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "_destination":
					reader.SetPrivateField("_destination", reader.Read<Utility.ValueObject<UnityEngine.Vector2Int>>(), instance);
					break;
					case "_destinationRoutes":
					reader.SetPrivateField("_destinationRoutes", reader.Read<System.Collections.Generic.List<UnityEngine.Vector2Int>>(), instance);
					break;
					case "_nextMovePos":
					reader.SetPrivateField("_nextMovePos", reader.Read<Utility.ValueObject<UnityEngine.Vector2Int>>(), instance);
					break;
					case "_prevPos":
					reader.SetPrivateField("_prevPos", reader.Read<Utility.ValueObject<UnityEngine.Vector2Int>>(), instance);
					break;
					case "_waitCount":
					reader.SetPrivateField("_waitCount", reader.Read<System.Int32>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_AINormalTrackingArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_AINormalTrackingArray() : base(typeof(Ling.AI.Move.AINormalTracking[]), ES3UserType_AINormalTracking.Instance)
		{
			Instance = this;
		}
	}
}