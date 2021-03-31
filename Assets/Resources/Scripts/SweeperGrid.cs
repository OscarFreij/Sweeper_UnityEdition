using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweeperGrid : MonoBehaviour
{
    public int gridSizeX { get; private set; }
    public int gridSizeY { get; private set; }
    public int bombCount { get; private set; }
    public GameObject[,] grid { get; private set; }
    public int[,] bombNeighbors { get; private set; }
    public Mesh CoverMesh { get; private set; }
    public Mesh BombMesh { get; private set; }
    public Material CoverBaseMaterial { get; private set; }
    public Color BaseCoverColor { get; private set; }
    public Color HoverCoverColor { get; private set; }
    public Color FlagedCoverColor { get; private set; }

    private int nextId;
    private int totalId;
    private int[] bombTileId;
    public void Init(int gridSizeX, int gridSizeY, int bombCount)
    {
        this.gridSizeX = gridSizeX;
        this.gridSizeY = gridSizeY;
        this.bombCount = bombCount;

        this.grid = new GameObject[this.gridSizeX, this.gridSizeY];
        
        this.bombNeighbors = new int[this.gridSizeX, this.gridSizeY];
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                this.bombNeighbors[i, j] = 0;
            }
        }

        this.bombTileId = new int[bombCount];

        this.totalId = this.gridSizeX * this.gridSizeY;
        this.nextId = 0;

        this.CoverBaseMaterial = Resources.Load<Material>("Materials/CoverBase");

        Color temp = new Color();
        ColorUtility.TryParseHtmlString("#414141", out temp);
        this.BaseCoverColor = temp;
        ColorUtility.TryParseHtmlString("#414480", out temp);
        this.HoverCoverColor = temp;
        ColorUtility.TryParseHtmlString("#418041", out temp);
        this.FlagedCoverColor = temp;
    }

    public void GenerateGrid()
    {
        for (int i = 0; i < bombCount; i++)
        {
            bool continueSearch;
            int tempId;
            do
            {
                continueSearch = false;
                tempId = Random.Range(0, this.totalId);

                foreach (int id in bombTileId)
                {
                    if (id == tempId)
                    {
                        continueSearch = true;
                        Debug.Log("Id already exists in array of bomb Id's. Generating new id...");
                        break;
                    }
                }

            } while (continueSearch);

            bombTileId[i] = tempId;
        }


        Debug.Log("Bomb id array generation complete!");

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Debug.Log($"Creating tile with id {this.nextId} of {this.totalId}");
                this.grid[x, y] = GameObject.Instantiate(Resources.Load("Prefabs/SweeperGridGameObject"), this.transform.position, Quaternion.identity, this.transform) as GameObject;
                
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
                this.grid[x, y].GetComponent<SweeperGridObj>().Init1(this.nextId, new Vector2(x, y), nextIsBomb, this.transform);

                if (nextIsBomb)
                {
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            try
                            {
                                this.bombNeighbors[x+i, y+j] += 1;
                            }
                            catch
                            {
                                //Tile dose not exist or is outside of bounds.
                            }
                        }
                    }
                }

                this.nextId += 1;
            }
        }

        this.nextId = 0;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Debug.Log($"Init2 for Tile {this.nextId}");
                this.grid[x, y].GetComponent<SweeperGridObj>().Init2(this.bombNeighbors[x, y]);
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

