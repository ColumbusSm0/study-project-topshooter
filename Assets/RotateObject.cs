using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    private GameObject EsteObjeto;

    // Start is called before the first frame update
    void Start()
    {
        EsteObjeto = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        EsteObjeto.gameObject.transform.Rotate(Vector3.up, 25f * Time.deltaTime);
    }
}
