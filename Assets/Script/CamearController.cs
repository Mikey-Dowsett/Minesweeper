using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamearController : MonoBehaviour
{
    private Camera cam;
    private BoxCollider2D box;
    private float max;

    private Vector3 startPos;

    private void Start(){
        cam = GetComponent<Camera>();
        box = GetComponent<BoxCollider2D>();
        max = cam.orthographicSize;
        startPos = transform.position;
    }
    
    void Update()
    {
        cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 4;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 1f, max);
        box.size = new Vector2(cam.orthographicSize*2, cam.orthographicSize*2);

        transform.position += new Vector3(Input.GetAxis("Horizontal")*Time.deltaTime*4, Input.GetAxis("Vertical")*Time.deltaTime*4);

        if(Input.GetKeyDown(KeyCode.Space)){
            ResetCam();
        }
    }

    public void ResetCam(){
        transform.position = startPos;
        cam.orthographicSize = max;
    }
}
