using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float speed { get; private set; }
    public float boost { get; private set; }
    public Vector2 borderLimits { get; private set; }
    public Vector3 spawnPosition { get; private set; }

    public void Init(float speed, float boost, Vector2 borderLimits)
    {
        this.speed = speed;
        this.boost = boost;

        this.borderLimits = borderLimits;
        this.spawnPosition = new Vector3(borderLimits.x / 2, 12, borderLimits.y / 2);

        this.transform.position = this.spawnPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float sTemp = this.speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            sTemp *= this.boost;
        }

        float hTemp = Input.GetAxis("Horizontal") * sTemp;
        float vTemp = Input.GetAxis("Vertical") * sTemp;
        
        if (this.transform.position.x < 0)
        {
            hTemp = Mathf.Clamp(hTemp, 0, 1);
        }
        else if (this.transform.position.x > this.borderLimits.x)
        {
            hTemp = Mathf.Clamp(hTemp, -1, 0);
        }

        if (this.transform.position.z < 0)
        {
            vTemp = Mathf.Clamp(vTemp, 0, 1);
        }
        else if (this.transform.position.z > this.borderLimits.y)
        {
            vTemp = Mathf.Clamp(vTemp, -1, 0);
        }

        this.transform.position += new Vector3(hTemp, 0, vTemp);



    }
}
