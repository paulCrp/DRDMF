using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject linkedCube;
    private Vector3 cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        //cameraPosition = transform.position;
        linkedCube.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = cameraPosition;
    }
}
