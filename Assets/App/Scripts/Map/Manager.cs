// 
// Manager.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.22.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


namespace Ling.Map
{
    /// <summary>
    /// 
    /// </summary>
    public class Manager : Utility.MonoSingleton<Manager> 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private Chara.Player _player = null;
        [SerializeField] private Grid _worldGrid = null;
        [SerializeField] private List<Tilemap> _colliderMaps = null;    // 現在読み込んでいるマップ

        #endregion


        #region プロパティ

        /// <summary>
        /// Cell間の移動スピード
        /// </summary>
        public float CellMoveTime { get; private set; }

        #endregion


        #region public, protected 関数

        /// <summary>
        /// マップを読み込む
        /// </summary>
        /// <param name="mapPrefab"></param>
        public void Load(GameObject mapPrefab)
        {
        }

        /// <summary>
        /// 指定した位置が通行可能かどうか
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool IsPassable(Vector2Int pos)
        {
            var vec3Pos = new Vector3Int(pos.x, pos.y, 0);

            foreach (var elm in _colliderMaps)
            {
                if (elm.GetColliderType(vec3Pos) != Tile.ColliderType.None)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Cellの座標からWorldPositionに変化させる
        /// </summary>
        /// <param name="tilePos"></param>
        /// <returns></returns>
        public Vector3 CellToWorld(Vector2Int tilePos)
        {
            return _worldGrid.GetCellCenterWorld(new Vector3Int(tilePos.x, tilePos.y, 0));
        }

        /// <summary>
        /// 指定したオブジェクトを現在のタイルの中央にぴったりと合わせる
        /// </summary>
        public void CellCenterFit(Transform trs)
        {
            var cellPos = _worldGrid.WorldToCell(trs.position);
            var centerPos = _worldGrid.GetCellCenterWorld(cellPos);

            trs.position = centerPos;
        }

        #endregion


        #region private 関数

        #endregion


        #region MonoBegaviour

        /// <summary>
        /// 初期処理
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            CellMoveTime = Const.CellMoveTime;

            // 最初からPlayerがついている場合は座標を初期化する
            if (_player != null)
            {
                CellCenterFit(_player.transform);
            }
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