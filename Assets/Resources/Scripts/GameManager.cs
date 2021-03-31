using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector2 gridSize { get; set; }
    public int bombAmount { get; set; }
    public MovementManager MM {get; set;}
    public SoundManager SM { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        this.gridSize = new Vector2(16, 16);
        this.bombAmount = 40;

        this.MM = GameObject.Find("Main Camera").GetComponent<MovementManager>();

        try
        {
            this.MM.Init(1.5f, 1.5f, this.gridSize);
            GameObject.Find("Grid").GetComponent<SweeperGrid>().Init((int)this.gridSize.x, (int)this.gridSize.y, this.bombAmount);
            GameObject.Find("Grid").GetComponent<SweeperGrid>().GenerateGrid();
        }
        catch (System.Exception e)
        {
            Debug.LogError("ERROR SYSTEM CONFIG\n"+ e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
