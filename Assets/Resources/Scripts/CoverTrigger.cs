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

            switch (this.transform.parent.GetComponent<SweeperGridObj>().state)
            {
                case BlockState.Closed:
                    this.GetComponent<MeshRenderer>().material.color = this.transform.parent.parent.GetComponent<SweeperGrid>().HoverCoverColor;
                    break;

                case BlockState.Flaged:
                    this.GetComponent<MeshRenderer>().material.color = this.transform.parent.parent.GetComponent<SweeperGrid>().FlagedCoverColor;
                    break;
            }
        }
    }

    void OnMouseEnter()
    {
        if (this.transform.parent.GetComponent<SweeperGridObj>().state == BlockState.Closed)
        {
            this.GetComponent<MeshRenderer>().material.color = this.transform.parent.parent.GetComponent<SweeperGrid>().HoverCoverColor;
        }
    }

    void OnMouseExit()
    {
        if (this.transform.parent.GetComponent<SweeperGridObj>().state == BlockState.Closed)
        {
            this.GetComponent<MeshRenderer>().material.color = this.transform.parent.parent.GetComponent<SweeperGrid>().BaseCoverColor;
        }

        if (this.transform.parent.GetComponent<SweeperGridObj>().state == BlockState.Flaged)
        {
            this.GetComponent<MeshRenderer>().material.color = this.transform.parent.parent.GetComponent<SweeperGrid>().FlagedCoverColor;
        }
    }
}
