using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;

[Serializable]
public class TileMapping
{
    public char character;
    public Tile tile;
}

[Serializable]
public class LevelMap
{
    public int xSize;
    public int ySize;
    public TextAsset textAsset;
}

public class TilesetManager : MonoBehaviour
{
    public static TilesetManager _instance;

    [Header("Walkable Tile")]
    [SerializeField] TileMapping walkable_TM;

    [Header("Impassable Tiles")]
    [SerializeField] TileMapping impassable_TM;
    [SerializeField] TileMapping borderTopLeft_TM, borderTopRight_TM, borderBottomLeft_TM, borderBottomRight_TM, borderTop_TM, borderBottom_TM, borderLeft_TM, borderRight_TM;


    [Header("Walked Tile")]
    [SerializeField] Tile walked;

    [Header("Tilemaps")]
    [SerializeField] Tilemap walkableTilemap;
    [SerializeField] Tilemap impassableTilemap;

    [Header("Player Initial Position")]
    [SerializeField] Transform playerInitialPosition;

    [Header("Board Size")]
    [SerializeField] int xSize;
    [SerializeField] int ySize;

    [Header("Walkable Points")]
    [SerializeField] float walkablePoints = 0;
    [SerializeField] float maxWalkablePoints = 0;

    [SerializeField] List<LevelMap> mapfiles = new List<LevelMap>();
    [SerializeField] int levelIndex = 0;

    void Awake()
    {
        _instance = this;
    }

    public void Init()
    {
        SetupBoard();
    }

    public void SetLevelIndex(int _index)
    {
        levelIndex = _index;
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

    void SetupBoard()
    {
        xSize = mapfiles[levelIndex].xSize;
        ySize = mapfiles[levelIndex].ySize;

        string[] rows = Regex.Split(mapfiles[levelIndex].textAsset.text, "\r\n|\r|\n");

        int iIndex = 0;
        int jIndex = 0;


        print("Calling Setup Board");
        int initialX = -(xSize + 1) / 2, initialY = (ySize - 1) / 2;
        int endX = initialX + xSize, endY = initialY - ySize;
        //Impassable
        for (int x = initialX; x < endX; x++)
        {
            for (int y = initialY; y > endY; y--)
            {
                // print("Character: " + rows[iIndex][jIndex]);
                if (rows[iIndex][jIndex] == impassable_TM.character)
                {
                    CreateImpassable(x, y);
                    // print("Impassable-Fill:" + x + "," + y);
                }
                else if (rows[iIndex][jIndex] == borderTopLeft_TM.character)
                {
                    CreateImpassable(x, y, borderTopLeft_TM.tile);
                    // print("Impassable-BTL:" + x + "," + y);
                }
                else if (rows[iIndex][jIndex] == borderTopRight_TM.character)
                {
                    CreateImpassable(x, y, borderTopRight_TM.tile);
                    // print("Impassable-BTR:" + x + "," + y);
                }
                else if (rows[iIndex][jIndex] == borderBottomLeft_TM.character)
                {
                    CreateImpassable(x, y, borderBottomLeft_TM.tile);
                    // print("Impassable-BBL:" + x + "," + y);
                }
                else if (rows[iIndex][jIndex] == borderBottomRight_TM.character)
                {
                    CreateImpassable(x, y, borderBottomRight_TM.tile);
                    // print("Impassable-BBR:" + x + "," + y);
                }
                // else if (rows[iIndex][jIndex] == borderTop_TM.character)
                // {
                //     CreateImpassable(x, y, borderTop_TM.tile);
                // }
                // else if (rows[iIndex][jIndex] == borderBottom_TM.character)
                // {
                //     CreateImpassable(x, y, borderBottom_TM.tile);
                // }
                // else if (rows[iIndex][jIndex] == borderLeft_TM.character)
                // {
                //     CreateImpassable(x, y, borderLeft_TM.tile);
                // }
                // else if (rows[iIndex][jIndex] == borderRight_TM.character)
                // {
                //     CreateImpassable(x, y, borderRight_TM.tile);
                // }

                //Walkable
                if (rows[iIndex][jIndex] == walkable_TM.character)
                {
                    CreateWalkable(x, y);
                    walkablePoints++;
                    // print("Walkable:" + x + "," + y);
                }

                //Player
                if (rows[iIndex][jIndex] == 'X')
                {
                    playerInitialPosition.localPosition = new Vector3((initialX) + jIndex + .5f, (initialY) - iIndex + .5f, 0);
                    walkablePoints++;
                    // print("PlayerPos:" + x + "," + y);
                }
                iIndex++;
            }
            jIndex++;
            // jIndex = 0;
            iIndex = 0;
        }

        maxWalkablePoints = walkablePoints;
    }

    public void SetWalked(Vector3Int pos)
    {
        walkableTilemap.SetTile(pos, null);
        impassableTilemap.SetTile(pos, walked);
        walkablePoints--;

        if (walkablePoints <= 0)
            GameManager._instance.EndGame();
    }

    public void ResetWalked(Vector3Int pos, bool _ = false)
    {
        walkableTilemap.SetTile(pos, walkable_TM.tile);
        impassableTilemap.SetTile(pos, null);
        walkablePoints++;

        if (_)
            SetWalked(pos);
    }

    public float GetWalkablePoints()
    {
        return walkablePoints;
    }

    public float GetWalkableScale()
    {
        return walkablePoints / maxWalkablePoints;
    }
}
