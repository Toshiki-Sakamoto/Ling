// 
// AStarScoreTileView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.08.25
// 

using UnityEngine;
using UnityEngine.UI;

namespace Ling._Debug.Algorithm
{
	/// <summary>
	/// AStarスコアをTileViewの上に表示する
	/// </summary>	
	public class AStarScoreTileView : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Text _text = default;
		[SerializeField] private Utility.UI.ObjectFollower _objectFollower = default;

		#endregion


		#region プロパティ

		public Utility.Algorithm.Astar.Node Node { get; set; }

		#endregion


		#region public, protected 関数

		public void SetScore(int score) =>
			_text.text = score.ToString();

		public void SetTextColor(Color color) =>
			_text.color = color;

		public void SetFollowTarget(Transform target) =>
			_objectFollower.SetTarget(target);

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}