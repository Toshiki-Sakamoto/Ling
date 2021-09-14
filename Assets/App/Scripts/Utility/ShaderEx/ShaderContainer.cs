// 
// ShaderManager.cs  
// ProductName Ling
//  
// Created by  on 2021.09.14
// 

using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Rendering;

namespace Utility.ShaderEx
{
	public interface IShaderContainer
	{
		/// <summary>
		/// シェーダーを取得する
		/// </summary>
		bool TryGetValue(string key, out Shader shader);

		/// <summary>
		/// シェーダーを取得、または生成する
		/// </summary>
		Shader GetOrCreateCache(string key);
	}

	/// <summary>
	/// シェーダーコンテナ
	/// </summary>
	public class ShaderContainer : IShaderContainer
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private Dictionary<string, Shader> _cacheDict = new Dictionary<string, Shader>();

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public bool TryGetValue(string key, out Shader shader)
		{
			return _cacheDict.TryGetValue(key, out shader);			
		}

		public Shader GetOrCreateCache(string key)
		{
			if (TryGetValue(key, out var shader))
			{
				return shader;
			}

			shader = Shader.Find(key);
			if (shader == null)
			{
				Utility.Log.Error($"シェーダーが見つからない {key}");
				return null;
			}

			_cacheDict.Add(key, shader);

			return shader;
		}

		#endregion


		#region private 関数

		#endregion
	}
}