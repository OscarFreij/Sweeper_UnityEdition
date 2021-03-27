using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweeperGrid : MonoBehaviour
{
    public int gridSizeX { get; private set; }
    public int gridSizeY { get; private set; }
    public int bombCount { get; private set; }
    public GameObject[,] grid { get; private set; }
    public Mesh CoverMesh { get; private set; }
    public Mesh BombMesh { get; private set; }
    public Material CoverBaseMaterial { get; private set; }
    public Material CoverFlagedMaterial { get; private set; }

    private int nextId;
    private int totalId;
    private int[] bombTileId;
    public void Init(int gridSizeX, int gridSizeY, int bombCount)
    {
        this.gridSizeX = gridSizeX;
        this.gridSizeY = gridSizeY;
        this.bombCount = bombCount;

        this.grid = new GameObject[this.gridSizeX, this.gridSizeY];
        this.bombTileId = new int[bombCount];

        this.totalId = this.gridSizeX * this.gridSizeY;
        this.nextId = 0;

        this.CoverBaseMaterial = Resources.Load<Material>("Materials/CoverBase");
        this.CoverFlagedMaterial = Resources.Load<Material>("Materials/CoverFlaged");
    }

    public void GenerateGrid()
    {
        for (int i = 0; i < bombCount; i++)
        {
            bool idNew = true;
            int tempId = Random.Range(0, this.totalId);
            do
            {
                foreach (int tileId in bombTileId)
                {
                    if (tileId == tempId)
                    {
                        idNew = false;
                        break;
                    }
                }

                if (idNew)
                {
                    bombTileId[i] = tempId;
                    break;
                }

            }
            while (true);
        }

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Debug.Log($"Creating tile with id {this.nextId} of {this.totalId}");
                grid[x, y] = GameObject.Instantiate(Resources.Load("Prefabs/SweeperGridGameObject"), this.transform.position, Quaternion.identity, this.transform) as GameObject;

                bool nextIsBomb = false;
                foreach (int id in bombTileId)
                {
                    if (id == this.nextId)
                    {
                        nextIsBomb = true;
                        break;
                    }
                }

                Debug.Log($"Init1 for Tile {this.nextId}");
                grid[x, y].GetComponent<SweeperGridObj>().Init1(this.nextId, new Vector2(x, y), nextIsBomb, this.transform);

                this.nextId += 1;
            }
        }

        this.nextId = 0;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Debug.Log($"Init2 for Tile {this.nextId}");
                grid[x, y].GetComponent<SweeperGridObj>().Init2(0);
                this.nextId += 1;
            }
        }
    }
}

public enum BlockState
{
    Closed = 0,
    Open = 1,
    Flaged = 2
}

