// 
// MapDrawView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.04.20
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ling._Debug.Builder
{
	/// <summary>
	/// 
	/// </summary>
	public class MapDrawView : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Transform _root = null;
		[SerializeField] private SpriteRenderer _mapSprite = null;  // 
		[SerializeField] private Color[] _colors = null;

		private int _width, _height;
		private SpriteRenderer[] _drawSprites;
		private Map.Builder.Const.BuilderType _builderType;
		private Coroutine _coroutine;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void Setup(int width, int height, Map.Builder.Const.BuilderType builderType)
		{
			if (_coroutine != null)
			{
				StopCoroutine(_coroutine);
				_coroutine = null;
			}

			if (_drawSprites != null)
			{
				foreach (var obj in _drawSprites)
				{
					GameObject.Destroy(obj.gameObject);
				}
			}

			_width = width;
			_height = height;
			_builderType = builderType;
			_drawSprites = new SpriteRenderer[width * height];

			switch (_builderType)
			{
				case Map.Builder.Const.BuilderType.Split:
					SetUp_Split();
					break;

				default:
					break;
			}
		}

		public void DrawUpdate(Map.Builder.IBuilder builder)
		{
			switch (_builderType)
			{
				case Map.Builder.Const.BuilderType.Split:
					_coroutine = StartCoroutine(DrawUpdate_Split(builder));
					break;

				default:
					break;
			}
		}

		#endregion


		#region private 関数

		private void SetUp_Split()
		{
			var width = _mapSprite.bounds.size.x;
			var height = _mapSprite.bounds.size.y;

			Utility.Log.Print($"Width {width}, Height{height}");

			var startVector = new Vector2(width * (_width * -0.5f) + width * 0.5f,
										  height * (_height * 0.5f) + height * 0.5f);

			// 全てのオブジェクトをSpriteにする
			for (int i = 0; i < _width * _height; ++i)
			{
				var x = i % _width;
				var y = i / _height;

				var drawSprite = _drawSprites[i] = GameObject.Instantiate(_mapSprite, _root);
				drawSprite.transform.localPosition = new Vector3(startVector.x + x * width, startVector.y - y * height, 0.0f);
				drawSprite.gameObject.SetActive(true);

				drawSprite.color = _colors[0];
			}
		}

		private IEnumerator DrawUpdate_Split(Map.Builder.IBuilder builder)
		{
			var splitBuilder = builder as Map.Builder.Split.Builder;
			if (splitBuilder == null) yield break;

			void Draw(RectInt rect, int colorIndex)
			{ 
				for (int y = rect.yMin; y <= rect.yMax; ++y)
				{
					for (int x = rect.xMin; x <= rect.xMax; ++x)
					{
						var index = y * _width + x;

                        if (index >= _drawSprites.Length)
                        {
							int i = 0;
							i = i;
                        }
						var draw = _drawSprites[index];
						draw.color = _colors[colorIndex];
					}
				}
			}

			// 一つ先にすすめる
			var enumerator = builder.ExecuteDebug();

			while (enumerator.MoveNext())
			{
				// 区画情報
				var mapRect = splitBuilder.MapRect;

				for (int i = 0; i < mapRect.RectCount; ++i)
				{
					var data = mapRect[i];

					// 区画から処理
					Draw(data.rect, i);
				}

				yield return new WaitForSeconds(enumerator.Current);
			}
		}

		#endregion


		#region MonoBegaviour

		#endregion
	}
}