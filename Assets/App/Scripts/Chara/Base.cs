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
using UnityEngine.UI;


namespace Ling.Chara
{
    /// <summary>
    /// プレイヤーや敵の元になるクラス
    /// </summary>
    public abstract class Base : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数
        
        [SerializeField] private Animator _animator = default;
        [SerializeField] private Vector3Int _vecCellPos = default; // マップ上の自分の位置
        [SerializeField] private MoveController _moveController = default;
        [SerializeField] private CharaStatus _status = default;

        private Tilemap _tilemap;

        #endregion


        #region プロパティ

        /// <summary>
        /// マップ上の現在の位置
        /// </summary>
        public Vector3Int CellPos { get { return _vecCellPos; } }

        /// <summary>
        /// 動きを管理する
        /// </summary>
        public MoveController MoveController => _moveController;

        /// <summary>
        /// 移動することができないタイルフラグ
        /// これ以外は移動できるとする
        /// </summary>
        /// <returns></returns>
        public virtual Map.TileFlag CanNotMoveTileFlag =>
            Map.TileFlag.None | Map.TileFlag.Wall;

        #endregion


        #region public, protected 関数

        public void Setup(CharaStatus status)
		{
            _status = status;

            // 死亡時
            status.IsDead.Where(isDead_ => isDead_)
                .Subscribe(_ =>
                {

                });
        }

        /// <summary>
        /// Tilemap情報を設定する
        /// </summary>
        /// <param name="tilemap"></param>
        public void SetTilemap(Tilemap tilemap)
        {
            _tilemap = tilemap;

            MoveController.SetTilemap(tilemap);
        }

        /// <summary>
        /// 座標の設定
        /// </summary>
        /// <param name="pos"></param>
        public void SetCellPos(Vector3Int pos)
        {
            _vecCellPos = pos;

            // 座標の中央に合わせる
            CellCenterFit();
        }

        /// <summary>
        /// ワールド空間の座標を設定
        /// </summary>
        /// <param name="worldPos"></param>
        public void SetWorldPos(Vector3 worldPos) =>
            transform.position = worldPos;

        /// <summary>
        /// 向き
        /// </summary>
        /// <param name="dir"></param>
        public void SetDirection(Vector3 dir)
        {
            _animator.SetFloat("x", dir.x);
            _animator.SetFloat("y", dir.y);
        }

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