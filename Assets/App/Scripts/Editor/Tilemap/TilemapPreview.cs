// 
// TilemapPreview.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.16.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


namespace Ling.Editor
{
    /// <summary>
    /// 
    /// </summary>
    [CustomPreview(typeof(Tilemap))]
    public class TilemapPreview : ObjectPreview
    {
        private static readonly float PREVIEW_CELL_SIZE = 24.0f;    // プレビュー１マスのサイズ
        private static readonly int PREVIEW_MARGIN = 1;             // プレビューの１マスのマージン
        private static readonly string NO_TILE_SPRITE_PATH = "Image/TilemapPreview/red";  // タイルがない場合のSpriteのResources下パス
        private static readonly string BASE_POSITION_SPRITE_PATH = "Image/TilemapPreview/green";  //基準点の場合のSpriteのResources下パス
        private static readonly GUIContent PreviewTitle = new GUIContent("Tilemap");        // プレビューのタイトル

        private Tilemap _tilemap = null;



        public override bool HasPreviewGUI()
        {
            return true;
        }

        public override GUIContent GetPreviewTitle()
        {
            return PreviewTitle;
        }

        public override void Initialize(Object[] targets)
        {
            base.Initialize(targets);

            foreach(Tilemap elm in targets)
            {
                _tilemap = elm;
                break;
            }
        }

        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            var origin = _tilemap.origin;
            var size = _tilemap.size;
            var contents = new List<GUIContent>();

            // 3D座標からUI座標にする
            for (int y = size.y - 1; y >= 0; --y)
            {
                for (int x = 0; x < size.x; ++x)
                {
                    var gridPos = new Vector3Int(origin.x + x, origin.y + y, 0);
                    var sprite = _tilemap.GetSprite(gridPos);

                    // タイルが設定されていない場合
                    if (sprite == null)
                    {
                        sprite = Resources.Load<Sprite>(NO_TILE_SPRITE_PATH);
                    }

                    var content = new GUIContent(string.Format("{0},{1}", gridPos.x, gridPos.y),
                                                    AssetPreview.GetAssetPreview(sprite));

                    contents.Add(content);
                }
            }

            var style = new GUIStyle();
            style.fixedWidth = PREVIEW_CELL_SIZE;
            style.fixedHeight = PREVIEW_CELL_SIZE;
            style.margin = new RectOffset(PREVIEW_MARGIN, PREVIEW_MARGIN, PREVIEW_MARGIN, PREVIEW_MARGIN);
            style.imagePosition = ImagePosition.ImageOnly;

            GUI.SelectionGrid(r, -1, contents.ToArray(), size.x, style);

            var basePositionSprite = Resources.Load<Sprite>(BASE_POSITION_SPRITE_PATH);
            var center = new Rect(r.x - origin.x * PREVIEW_CELL_SIZE - origin.x * PREVIEW_MARGIN,
                r.y - origin.y * PREVIEW_CELL_SIZE - origin.y * PREVIEW_MARGIN,
                PREVIEW_CELL_SIZE, PREVIEW_CELL_SIZE);

            GUI.DrawTexture(center, basePositionSprite.texture);
        }
    }
}