using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrawInGrid : MonoBehaviour
{
    [Header("Tilemaps")]
    [SerializeField] Tilemap walkableTilemap;
    [SerializeField] Tilemap impassableTilemap;

    [Header("Walkable Tile")]
    [SerializeField] TileMapping walkable_TM;

    [Header("Impassable Tiles")]
    [SerializeField] TileMapping impassable_TM;

    [SerializeField] SelectTile selectTile;

    Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = camera.nearClipPlane;
            Vector3 worldPosition = camera.ScreenToWorldPoint(mousePos, Camera.MonoOrStereoscopicEye.Mono);

            if (selectTile.tileSelected == 0)
            {
                CreateWalkable((int)worldPosition.x, (int)worldPosition.y);
            }
            else if (selectTile.tileSelected == 1)
            {
                CreateImpassable((int)worldPosition.x, (int)worldPosition.y);
            }
        }
    }

    void CreateImpassable(int x, int y)
    {
        Vector3Int tempPos = new Vector3Int(x, y, 0);
        impassableTilemap.SetTile(tempPos, impassable_TM.tile);
    }

    void CreateImpassable(int x, int y, Tile tile)
    {
        Vector3Int tempPos = new Vector3Int(x, y, 0);
        impassableTilemap.SetTile(tempPos, tile);
    }

    void CreateWalkable(int x, int y)
    {
        Vector3Int tempPos = new Vector3Int(x, y, 0);
        walkableTilemap.SetTile(tempPos, walkable_TM.tile);
    }

    public void CreateTextAsset()
    {
        string temporaryTextFileName = "Assets/Resources/test.txt";
        File.WriteAllText(temporaryTextFileName, "blabla");

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;
        print(textAsset.text);
        // AssetDatabase.CreateAsset(textAsset, "Assets/Resources/TextAsset.txt");
        // Application.dataPath + 
        // AssetDatabase.CreateAsset(textAsset, "Assets/Resources/test.txt");
    }
}
