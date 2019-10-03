// 
// MoveController.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.16.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Map.Chara
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveController : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private float _speed = 1.0f;
        [SerializeField] private Rigidbody2D _rigidBody = null;

        private bool _isMoving;         // 動いてるとき
        private Base _trsModel;         // 動いている対象
        //private Vector2 _inputAxis;
        private List<Vector2Int> _moveList = new List<Vector2Int>();

        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void SetModel(Base model)
        {
            _trsModel = model;
        }

        /// <summary>
        /// 動きを止める
        /// </summary>
        public void MoveStop()
        {
            _isMoving = false;
            _moveList.Clear();

            StopAllCoroutines();
        }

        #endregion


        #region private 関数

        /// <summary>
        /// 動く
        /// </summary>
        /// <returns></returns>
        private IEnumerator Move()
        {
            var manager = Manager.Instance;

            foreach (var elm in _moveList)
            {
                var start = manager.CellToWorld(_trsModel.CellPos);
                var finish = manager.CellToWorld(elm);

                var diffVec = finish - start;

                var startTime = Time.timeSinceLevelLoad;

                bool isEnd = false;
                while (!isEnd)
                {
                    yield return null;

                    var diff = Time.timeSinceLevelLoad - startTime;
                    if (diff > manager.CellMoveTime)
                    {
                        diff = 1.0f;
                        isEnd = true;
                    }
                    else
                    {
                        diff /= manager.CellMoveTime;
                    }

                    var newPos = Vector3.Lerp(start, finish, diff);
                    _trsModel.transform.position = newPos;

                    _trsModel.SetDirection(diffVec);
                }

                // 念のために今の座標の中央に合わせる
                manager.CellCenterFit(_trsModel.transform);

                _trsModel.SetCellPos(elm);
            }

            _isMoving = false;
            _moveList.Clear();
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
            if (_trsModel == null)
            {
                return;
            }

            if (_isMoving)
            {
                return;
            }

            var manager = Manager.Instance;

            // x, y の入力
            // 関連付けはInput Managerで行っている
            //            _inputAxis.x = Input.GetAxis("Horizontal");
            //            _inputAxis.y = Input.GetAxis("Vertical");

            Vector2Int moveDir = Vector2Int.zero;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveDir = Vector2Int.left;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                moveDir = Vector2Int.right;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                moveDir = Vector2Int.up;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                moveDir = Vector2Int.down;
            }

            if (moveDir != Vector2Int.zero)
            {
                _trsModel.SetDirection(new Vector3(moveDir.x, moveDir.y, 0.0f));

                var movePos = _trsModel.CellPos + moveDir;
                if (!manager.IsPassable(movePos))
                {
                    // 移動できない
                    return;
                }

                _isMoving = true;

                _moveList.Add(movePos);

                StartCoroutine(Move());
            }
        }

        private void FixedUpdate()
        {
            //_rigidBody.velocity = _inputAxis.normalized * _speed;
//            _rigidBody.MovePosition()
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