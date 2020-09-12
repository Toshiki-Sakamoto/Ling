// 
// Base.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.22.
// 
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;
using Ling.Utility.Extensions;
using Cysharp.Threading.Tasks;


namespace Ling.Chara
{
    /// <summary>
    /// プレイヤーや敵の元になるクラス
    /// </summary>
    public abstract class ViewBase : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private CharaType _charaType = default;
        [SerializeField] private Animator _animator = default;
        [SerializeField] private Vector3Int _vecCellPos = default; // マップ上の自分の位置
        [SerializeField] private MoveController _moveController = default;
        [SerializeField] private Ling.Const.MoveAIType _moveAIType = default;
        [SerializeField] private Ling.Const.AttackAIType _attackAIType = default;

        private Tilemap _tilemap;
        private Renderer[] _renderers = null;
        private int _mapLevel;
        #endregion


        #region プロパティ

        public CharaType CharaType => _charaType;

        /// <summary>
        /// マップ上の現在の位置
        /// </summary>
        public Vector3Int CellPos { get { return _vecCellPos; } }

        /// <summary>
        /// 動きを管理する
        /// </summary>
        public MoveController MoveController => _moveController;

        /// <summary>
        /// 向きベクトル
        /// </summary>
        public Vector2 Dir { get; private set; } = new Vector2(0f, 1f);

        #endregion


        #region public, protected 関数
        
        /// <summary>
        /// Tilemap情報を設定する
        /// </summary>
        /// <param name="tilemap"></param>
        public void SetTilemap(Tilemap tilemap, int mapLevel)
        {
            _tilemap = tilemap;
            _mapLevel = mapLevel;

            MoveController.SetTilemap(tilemap);
        }

        /// <summary>
        /// 座標の設定
        /// </summary>
        public void SetCellPos(in Vector2Int pos, bool needsFit = true) =>
            SetCellPos(new Vector3Int { x = pos.x, y = pos.y }, needsFit);

        public void SetCellPos(Vector3Int pos, bool needsFit = true)
        {
            _vecCellPos = pos;

            if (needsFit)
            {
                // 座標の中央に合わせる
                CellCenterFit();
            }
        }

        /// <summary>
        /// ワールド空間の座標を設定
        /// </summary>
        /// <param name="worldPos"></param>
        public void SetWorldPos(in Vector3 worldPos) =>
            transform.position = worldPos;

        /// <summary>
        /// 向き
        /// </summary>
        public void SetDirection(in Vector2 dir)
        {
            _animator.SetFloat("x", dir.x);
            _animator.SetFloat("y", dir.y);

            Dir = dir;
        }

        public void SetSortingLayerAndOrder(string sortingName, int order)
		{
            foreach (var renderer in _renderers)
			{
                renderer.sortingLayerName = sortingName;
                renderer.sortingOrder = order;
			}
		}

        /// <summary>
        /// 現在位置から指定した数を足して移動する
        /// </summary>
        public System.IObservable<AsyncUnit> MoveAtAddPos(in Vector2Int addCellPos) =>
            MoveController.SetMoveCellPos(CellPos + addCellPos.ToVector3Int());

        public System.IObservable<AsyncUnit> Move(in Vector2Int startPos, in Vector2Int endPos) =>
            MoveController.SetMoveCellPos(startPos.ToVector3Int(), endPos.ToVector3Int());

        #endregion


        #region private 関数


        /// <summary>
        /// 指定したオブジェクトを現在のタイルの中央にぴったりと合わせる
        /// </summary>
        public void CellCenterFit()
        {
            //var cellPos = _tilemap.WorldToCell(transform.position);
            var centerPos = _tilemap.GetCellCenterWorld(_vecCellPos);

            transform.position = centerPos;
        }

        #endregion


        #region MonoBegaviour

        /// <summary>
        /// 初期処理
        /// </summary>
        void Awake()
        {
            if (_moveController == null)
            {
                _moveController = GetComponent<MoveController>();
            }

            _moveController.SetModel(this);

            _renderers = GetComponentsInChildren<Renderer>();
        }

        /// <summary>
        /// 更新前処理
        /// </summary>
        void Start()
        {
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        void Update()
        {
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