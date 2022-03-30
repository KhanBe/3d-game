using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCam : MonoBehaviour
{
    private Camera MainCamera;

    void Start()
    {
        MainCamera = Camera.main;
    }
    void Update()
    {
        transform.rotation = MainCamera.transform.rotation;
    }
}
