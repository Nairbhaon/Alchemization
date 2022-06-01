using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float speed;
    Camera cam;
    Vector3 currentPos;
    Vector3 pastPos;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        pastPos = currentPos;
        currentPos = Input.mousePosition;

        if(Input.GetMouseButton(2))
            transform.position -= (currentPos - pastPos) * cam.orthographicSize * speed;

        cam.orthographicSize -= Input.mouseScrollDelta.y;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 3, 100);
    }
}
