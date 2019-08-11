using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Vector3 mouseOriginPoint;
    private Vector3 offset;
    private bool dragging;
    private int vertical;
    private int horizontal;
    public float speed;
    private int rotate;
    public float rotateSpeed;
    private Vector3 pivot;


    private void Update()
    {
        //Zoom In/Out
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * (Camera.main.orthographicSize), 2.5f, 50f);

        #region Camera Rotation
        //Raycast to the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        

        if (Physics.Raycast(ray, out hit))
        {
            pivot = hit.point;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            rotate = 1;
            transform.RotateAround(pivot, Vector3.up * rotate, rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rotate = -1;
            transform.RotateAround(pivot, Vector3.up * rotate, rotateSpeed);
        }

        #endregion

        #region Keyboard Pan Camera Controls 
        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1;
            //this.transform.Translate(Vector3.left * speed * Time.deltaTime);
            transform.position += this.transform.up * speed;


        }
        else if (Input.GetKey(KeyCode.S))
        {
            vertical = -1;
            transform.position += this.transform.up * vertical * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Da");
            horizontal = -1;
            transform.position += this.transform.right * horizontal * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
            transform.position += this.transform.right * horizontal * speed;
        }
        #endregion

        #region Middle Mouse Button Pan Camera Control
        if (Input.GetMouseButton(2))
        {
            offset = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            if (!dragging)
            {
                dragging = true;
                mouseOriginPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            dragging = false;
        }
        if (dragging)
            transform.position = mouseOriginPoint - offset;

        #endregion
    }

}
