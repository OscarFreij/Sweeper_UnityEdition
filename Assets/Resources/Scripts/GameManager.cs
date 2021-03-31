using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            GameObject.Find("Grid").GetComponent<SweeperGrid>().Init(16, 16, 40);
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
