using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.up, 25f * Time.deltaTime);
    }
}
