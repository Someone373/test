using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightController : MonoBehaviour
{
    public Light m_light;
    // Start is called before the first frame update
    void Start()
    {
        m_light = GetComponent<Light>();
        m_light.intensity = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
