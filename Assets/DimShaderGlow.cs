using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Polybrush;

public class DimShaderGlow : MonoBehaviour
{
    public Material myMaterial;
    private Color emissionColor;
    // Start is called before the first frame update
    void Start()
    {
        // myMaterial = GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        float test = 0.0f;
        emissionColor = new Color(11,67,6,100);
        test += Time.deltaTime;
        float s = Mathf.PingPong (Time.time, 1f);
        myMaterial.SetColor("_EmissionColor", emissionColor * 0.1f);
        Debug.Log(s);
        
    }
}
