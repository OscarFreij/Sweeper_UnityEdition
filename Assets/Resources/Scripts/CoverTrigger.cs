using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverTrigger : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.parent.GetComponent<SweeperGridObj>().RevealTile();
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            transform.parent.GetComponent<SweeperGridObj>().ToggleFlag();
        }
    }
}
