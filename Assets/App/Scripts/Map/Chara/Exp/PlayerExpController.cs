// 
// PlayerExpController.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.29
// 

using UnityEngine;
using UniRx;
using Zenject;

namespace Ling.Chara.Exp
{
	/// <summary>
	/// 
	/// </summary>
	/// <summary>
	/// プレイヤーなどの累計経験値量でレベルアップするキャラクタ操作
	/// </summary>
	[RequireComponent(typeof(ICharaController))]
	public class PlayerExpController : MonoBehaviour, ICharaExpController
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private MasterData.IMasterHolder _masterHolder;

		private Subject<int> _subject = new Subject<int>();
		private ICharaController _chara;
		private MasterData.Chara.LvTableMaster _lvTableMaster;

		private int _totalExp;		// 累計経験値

		private int _currentLv;		// 現在のレベル
		private int _nextExp;		// 次のレベルに必要な経験値量

		#endregion


		#region プロパティ

		IObservable<int> ICharaExpController.OnLvUp => _subject;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		void ICharaExpController.Add(int exp)
		{
			_totalExp += exp;

			// Lvが上がったら通知する
			if (_totalExp >= _nextExp)
			{
				Apply();
			}
		}

		#endregion


		#region private 関数

		private void Apply()
		{
			var expData = _lvTableMaster.GetDataByExp(_totalExp);

			// 現在のレベル
			_currentLv = expData.Lv;

			// 次必要な経験値量
			_nextExp = _lvTableMaster.GetNextDataByExp(_totalExp).Exp;

			// 現在のレベルを渡す
			_subject.OnNext(_currentLv);
		}

		private void Awake()
		{
			_chara = GetComponent<ICharaController>();

			// キャラクタIDから経験値テーブルを引っ張ってくる
			var lvTableRepository = _masterHolder.PlayerLvTableRepository;
			_lvTableMaster = lvTableRepository.Find(_chara.Model.ID);

			// 現在のレベルと次に必要な経験値量を決める
			Apply();
		}

		#endregion
	}
}