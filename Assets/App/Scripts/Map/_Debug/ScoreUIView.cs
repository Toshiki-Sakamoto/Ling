// 
// ScoreUIView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.09.06
// 

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Ling;

namespace Ling.Map._Debug
{
	/// <summary>
	/// Debug用にTileViewの上にUIを表示させる
	/// </summary>
	public class ScoreUIView : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Text _scoreText = default;
		[SerializeField] private Text _posText = default;

		private Tilemap _tileMap;
		private Vector3Int _cellPosition;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void SetTileData(Tilemap tileMap, in Vector3Int position)
		{
			_tileMap = tileMap;
			_cellPosition = position;

			SetPosText();
		}

		public void SetScore(int score)
		{
			gameObject.SetActive(true);
			_scoreText.text = score.ToString();
		}

		public void SetPosText()
		{
			_posText.text = _cellPosition.ToVector2Int().ToString();
		}

		public void SetTextColor(Color color) =>
			_scoreText.color = color;


		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 更新処理
		/// </summary>
		void Update()
		{
			var worldPos = _tileMap.GetCellCenterWorld(_cellPosition);
			var screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);

			transform.position = screenPos.ToVector3();
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		void OnDestoroy()
		{
		}

		#endregion
	}
}