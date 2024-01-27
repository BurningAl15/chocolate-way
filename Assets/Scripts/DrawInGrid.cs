using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AldhaDev.TileData
{
    public class DrawInGrid : MonoBehaviour
    {
        [Header("Tilemaps")]
        [SerializeField] private Tilemap walkableTilemap;
        [SerializeField] private Tilemap impassableTilemap;

        [Header("Walkable Tile")]
        [SerializeField] private TileMapping walkable_TM;

        [Header("Impassable Tiles")]
        [SerializeField] private TileMapping impassable_TM;

        [SerializeField] private SelectTile selectTile;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            OnTouchScreen();
        }

        private void OnTouchScreen()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = _camera.nearClipPlane;
            Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePos, Camera.MonoOrStereoscopicEye.Mono);

            if (selectTile.tileSelected == 0)
            {
                CreateWalkable((int)worldPosition.x, (int)worldPosition.y);
            }
            else if (selectTile.tileSelected == 1)
            {
                CreateImpassable((int)worldPosition.x, (int)worldPosition.y);
            }
        }

        private void CreateImpassable(int x, int y)
        {
            Vector3Int tempPos = new Vector3Int(x, y, 0);
            impassableTilemap.SetTile(tempPos, impassable_TM.tile);
        }

        private void CreateImpassable(int x, int y, Tile tile)
        {
            Vector3Int tempPos = new Vector3Int(x, y, 0);
            impassableTilemap.SetTile(tempPos, tile);
        }

        private void CreateWalkable(int x, int y)
        {
            Vector3Int tempPos = new Vector3Int(x, y, 0);
            walkableTilemap.SetTile(tempPos, walkable_TM.tile);
        }

        public void CreateTextAsset()
        {
            string temporaryTextFileName = "Assets/Resources/test.txt";
            File.WriteAllText(temporaryTextFileName, "blabla");

            // AssetDatabase.SaveAssets();
            // AssetDatabase.Refresh();
            TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;
            if (textAsset != null) print(textAsset.text);
            // AssetDatabase.CreateAsset(textAsset, "Assets/Resources/TextAsset.txt");
            // Application.dataPath + 
            // AssetDatabase.CreateAsset(textAsset, "Assets/Resources/test.txt");
        }
    }
}
