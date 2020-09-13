// 
// MoveController.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.16.
// 

using UnityEngine;
using UnityEngine.Tilemaps;
using UniRx;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using Ling.Utility.Extensions;

namespace Ling.Chara
{
    /// <summary>
    /// 移動コントローラー
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharaMover : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private float _speed = 1.0f;
        [SerializeField] private Rigidbody2D _rigidBody = null;

        private bool _isMoving;         // 動いてるとき
        private ICharaController _chara;         // 動いている対象
        private Vector3Int _startPos, _endPos;
        private Vector2Int _vector2IntEndPos;
        private Tilemap _tilemap;

        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void SetModel(ICharaController chara) =>
            _chara = chara;

        public void SetTilemap(Tilemap tilemap)
        {
            _tilemap = tilemap;
        }

        /// <summary>
        /// 指定したセルに移動させる
        /// </summary>
        /// <param name="cellPos"></param>
        public System.IObservable<AsyncUnit> SetMoveCellPos(in Vector2Int endPos) =>
            SetMoveCellPos(_chara.Model.CellPosition.Value, endPos);

        public System.IObservable<AsyncUnit> SetMoveCellPos(in Vector2Int startPos, in Vector2Int endPos)
        {
            MoveStop();

            _vector2IntEndPos = endPos;

            _startPos = startPos.ToVector3Int();
            _endPos = endPos.ToVector3Int();

            return Move().ToObservable();
        }

        /// <summary>
        /// 動きを止める
        /// </summary>
        public void MoveStop()
        {
            _isMoving = false;
            //_moveList.Clear();

            StopAllCoroutines();
        }

        #endregion


        #region private 関数

        /// <summary>
        /// 動きの処理
        /// </summary>
        /// <returns></returns>
        private async UniTask Move()
        {
            //foreach (var elm in _moveList)
            {
                var start = _tilemap.GetCellCenterWorld(_startPos);
                var finish = _tilemap.GetCellCenterWorld(_endPos);

                var diffVec = finish - start;

                _chara.Model.SetDirection(new Vector2Int(Mathf.RoundToInt(diffVec.x), Mathf.RoundToInt(diffVec.z)));

                await _chara.View.transform.DOMove(finish, 0.15f).SetEase(Ease.Linear);

                // 見た目だけ反映させる
                _chara.Model.SetCellPosition(_vector2IntEndPos, reactive: true, sendEvent: false);
            }

            _isMoving = false;
        }

        #endregion


        #region MonoBegaviour


        private void Start()
        {
            _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }


        /// <summary>
        /// 更新処理
        /// </summary>
        void Update()
        {
            if (_chara == null)
            {
                return;
            }

            if (_isMoving)
            {
                return;
            }
        }

#       endregion
	}
}