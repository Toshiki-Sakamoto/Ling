//
// TileEvent.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.10.04
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


namespace Ling.Utility.TileEvent
{
	/// <summary>
	/// 
	/// </summary>
    [System.Serializable]
    public class TileEvent
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        [SerializeField] private string _name;  // タイル名
        [SerializeField] private EventTriggerType _trigger;
        [SerializeField] private UnityEvent _onEvent;
        

        private TileBase _tile = null;
        private int _posX;
        private int _posY;
        private float _worldX;
        private float _worldY;

        #endregion


        #region プロパティ

        #endregion


		#region コンストラクタ, デストラクタ

		#endregion


        #region public, protected 関数

        #endregion


        #region private 関数

        #endregion
    }
}
