﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnanBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition + Vector3.forward * 20f);
    }
}
