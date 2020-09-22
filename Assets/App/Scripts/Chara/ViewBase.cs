// 
// Base.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.22.
// 
using UnityEngine;
using UnityEngine.Tilemaps;
using Cysharp.Threading.Tasks;
using UniRx;


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
        [SerializeField] private Ling.Const.MoveAIType _moveAIType = default;
        [SerializeField] private Ling.Const.AttackAIType _attackAIType = default;

        private Tilemap _tilemap;
        private Renderer[] _renderers = null;
        private int _mapLevel;

        #endregion


        #region プロパティ


        public CharaType CharaType => _charaType;

        /// <summary>
        /// アニメーション再生中の場合true
        /// </summary>
        public bool IsAnimationPlaying { get; private set; }

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
        }

        /// <summary>
        /// 座標の設定
        /// </summary>
        public void SetCellPos(in Vector2Int pos, bool needsFit = true) =>
            SetCellPos(new Vector3Int { x = pos.x, y = pos.y }, needsFit);

        public void SetCellPos(in Vector3Int pos, bool needsFit = true)
        {
            if (needsFit)
            {
                // 座標の中央に合わせる
                CellCenterFit(pos);
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
        }

        /// <summary>
        /// 指定したSort名とOrder値を設定する
        /// </summary>
        public void SetSortingLayerAndOrder(string sortingName, int order)
		{
            foreach (var renderer in _renderers)
			{
                renderer.sortingLayerName = sortingName;
                renderer.sortingOrder = order;
			}
		}

        /// <summary>
        /// 死亡アニメーションを再生する
        /// </summary>
        public void PlayDeadAnimation()
        {
            // 今は非アクティブにしてみるか
            
            IsAnimationPlaying = false;
        }


        /// <summary>
        /// 再生中のアニメーションが終わるまで待機する
        /// </summary>
        /// <returns></returns>
        public async UniTask WaitForAnimation()
        {
            // WaitXXは必ず1フレーム待機が入るのでそれを避ける
            if (!IsAnimationPlaying) return;

            await UniTask.WaitWhile(() => IsAnimationPlaying);
        }

        #endregion


        #region private 関数


        /// <summary>
        /// 指定したオブジェクトを現在のタイルの中央にぴったりと合わせる
        /// </summary>
        public void CellCenterFit(in Vector3Int cellPosition)
        {
            if (_tilemap == null) return;

            var centerPos = _tilemap.GetCellCenterWorld(cellPosition);

            transform.position = centerPos;
        }

        #endregion


        #region MonoBegaviour

        /// <summary>
        /// 初期処理
        /// </summary>
        void Awake()
        {
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