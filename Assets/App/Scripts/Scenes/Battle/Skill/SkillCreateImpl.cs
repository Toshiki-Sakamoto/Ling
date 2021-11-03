//
// SkillCreateImpl.cs
// ProductName Ling
//
// Created by  on 2021.09.11
//

using Ling.MasterData.Skill;
using Cysharp.Threading.Tasks;
using System.Threading;
using Zenject;
using Ling.Map;
using Utility.Extensions;
using UniRx;
using System;
using System.Linq;
using Ling.Chara;
using Utility;

namespace Ling.Scenes.Battle.Skill
{
	/// <summary>
	/// スキル作成
	/// </summary>
	public class SkillCreateImpl
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Map.MapManager _mapManager;
		[Inject] private Common.Effect.IEffectManager _effectManager;
		[Inject] private Chara.CharaManager _charaManager;

		private Chara.ICharaController _chara, _target;
		private SkillMaster _skill;
		private Map.SearchResult _searchResult;

		private Subject<Chara.ICharaController> _onTargetSubject = new Subject<Chara.ICharaController>();

		#endregion


		#region プロパティ

		public IObservable<Chara.ICharaController> OnTarget => _onTargetSubject;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(Chara.ICharaController chara, SkillMaster skill, Map.SearchResult result)
		{
			_chara = chara;
			_skill = skill;
			_searchResult = result;
		}

		public Common.Effect.EffectPlayer Build()
		{
			var effect = _effectManager.CreatePlayer(_skill);

			ApplyMove(effect);

			var effectEntity = _skill.Effect;
			if (effectEntity.Target == Const.TargetType.Self)
			{
				_onTargetSubject.OnNext(_chara);
			}

			ApplySortingLayer(effect);

			return effect;
		}

		#endregion


		#region private 関数

		private void ApplyMove(Common.Effect.EffectPlayer player)
		{
			var searcher = _chara.FindTileDataMap(_mapManager).Seacher;
			var effectEntity = _skill.Effect;

			switch (effectEntity.Range)
			{
				// 足元
				case RangeType.Foot:
				{
					// エフェクトには特に何もしない
				}
				break;

				case RangeType.Line:
				case RangeType.LinePnt:	// 貫通
				{
					// 壁まで
					var tiles = searcher.SearchLines(_chara.CellPos, _chara.Model.Dir.Value, Const.TileFlag.Wall);
					var startPos = _chara.CellToWorld(_mapManager);
					var endTile = tiles.SecondLast();
					if (endTile == null)
					{
						// null
					}

					var endPos = _chara.CellToWorld(endTile.Pos, _mapManager);
					if (startPos == endPos)
					{
						// 開始地点と終了地点が同じ場所は何もできない
						break;
					}

					// 間にあったターゲットを保持する
					foreach (var tile in tiles)
					{
						var target = _charaManager.FindCharaInPos(_chara.Level, tile.Pos);
						if (target == null) continue;

						// ターゲットが全てなら関係なく登録
						ApplyTarget(target);
					}
					
					Common.Effect.IEffectMoveCore move = new Common.Effect.EffectMoveCoreConstantLiner();
					move.SetStartPos(startPos);
					move.SetEndPos(endPos);
					move.SetSpeed(effectEntity.Speed);
					
					player.Mover.RegisterCore(move);
					player.Mover.SetEase(effectEntity.Ease);
				}
				break;
			}
		}

		public void ApplyTarget(Chara.ICharaController target)
		{
			var effectEntity = _skill.Effect;

			switch (effectEntity.Target)
			{
				// 関係なく登録
				case Const.TargetType.All:
				{
					_onTargetSubject.OnNext(target);
				}
				break;

				case Const.TargetType.Ally:
				case Const.TargetType.Enemy:
				{
					var targetCharaType = _chara.Model.ConvertTargetCharaType(effectEntity.Target);
					if (targetCharaType != target.Model.CharaType) break;

					_onTargetSubject.OnNext(target);
				}
				break;
			}
		}

		private void ApplySortingLayer(Common.Effect.EffectPlayer player)
		{
			if (player == null) return;

			var changer = player.gameObject.GetOrAddComponent<Utility.Renderer.SortingLayerChanger>();
			var groundTilemap = _mapManager.FindGroundTilemap(_chara.Level);

			changer.LayerName = groundTilemap.LayerName;
		}

		#endregion
	}
}
