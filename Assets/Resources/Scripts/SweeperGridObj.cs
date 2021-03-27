using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweeperGridObj : MonoBehaviour
{
    // Public variables
    public int id { get; private set; }
    public Vector2 pos { get; private set; }
    public bool isBomb { get; private set; }
    public BlockState state { get; private set; }
    public Transform grid { get; private set; }


    public void Init1(int id, Vector2 pos, bool isBomb, Transform grid)
    {
        this.id = id;
        this.pos = pos;
        this.transform.position = new Vector3(this.pos.x, 0, this.pos.y);
        this.name = $"Tile-[{this.id}] [{this.pos.x}:{this.pos.y}]";
        this.isBomb = isBomb;
        this.state = BlockState.Closed;
        this.grid = grid;

        this.transform.parent = this.grid;
    }

    public void Init2(int neighborBombCount)
    {
        Transform contentObject = transform.Find("Content");
        if (this.isBomb)
        {
            GameObject.Destroy(contentObject.Find("NumberCanvas").gameObject);
        }
        else
        {
            GameObject.Destroy(contentObject.Find("Bomb").gameObject);
            if (neighborBombCount > 0)
            {
                contentObject.Find("NumberCanvas").Find("Number").GetComponent<UnityEngine.UI.Text>().text = neighborBombCount.ToString();
            }
            else
            {
                GameObject.Destroy(contentObject.Find("NumberCanvas").gameObject);
            }
        }
    }

    public void RevealTile()
    {
        switch (state)
        {
            case BlockState.Closed:
                GameObject.Destroy(transform.Find("Cover").gameObject);
                if (isBomb)
                {
                    Debug.Log($"{this.name} was opened! It was a bomb!");
                }
                else
                {
                    Debug.Log($"{this.name} was opened! It was NOT a bomb!");
                }
                state = BlockState.Open;
                break;

            case BlockState.Open:
                Debug.LogWarning($"{this.name} is already open!");
                break;

            case BlockState.Flaged:
                Debug.Log($"{this.name} is flaged! It can not be opened!");
                break;

            default:
                Debug.LogError($"{this.name} reveal failed! Unknown reason!");
                break;
        }
    }

    public void ToggleFlag()
    {
        switch (state)
        {
            case BlockState.Closed:
                this.transform.Find("Cover").GetComponent<MeshRenderer>().material = this.grid.GetComponent<SweeperGrid>().CoverFlagedMaterial;
                state = BlockState.Flaged;
                Debug.Log($"{this.name} is now flaged!");
                break;
            case BlockState.Open:
                Debug.LogError($"{this.name} flag toggle failed! Cover already open!");
                break;
            case BlockState.Flaged:
                this.transform.Find("Cover").GetComponent<MeshRenderer>().material = this.grid.GetComponent<SweeperGrid>().CoverBaseMaterial;
                state = BlockState.Closed;
                Debug.Log($"{this.name} is now un-flaged!");
                break;
            default:
                Debug.LogError($"{this.name} flag toggle failed! Unknown reason!");
                break;
        }
    }
}