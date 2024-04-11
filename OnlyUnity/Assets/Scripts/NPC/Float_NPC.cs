using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float_NPC : MonoBehaviour
{
    [SerializeField] private float floatSpeed;
    [SerializeField] private float maxUp;
    [SerializeField] private float maxDown;
    [SerializeField] private bool goingUp = true;

    
    void Update()
    {
        if (goingUp && transform.localPosition.y < maxUp)
        {
            transform.Translate(Vector3.up * floatSpeed * Time.deltaTime, Space.World);
            if (transform.localPosition.y >= maxUp)
                goingUp = false;
        }
        else if (!goingUp && transform.localPosition.y > maxDown)
        {
            transform.Translate(-Vector3.up * floatSpeed * Time.deltaTime, Space.World);
            if (transform.localPosition.y <= maxDown)
                goingUp = true;
        }
    }
}
