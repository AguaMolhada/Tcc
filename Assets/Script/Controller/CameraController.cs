using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class used in the camera moviment.
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Speed that the camera will move
    /// </summary>
    public float panSpeed = 20f;
    /// <summary>
    /// Ammout distance of the border that the camera will move when using the mouse
    /// </summary>
    public float PanBorderThickness = 10f;

    public float ScrollSpeed = 20;
    private void Update()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height -PanBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= PanBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - PanBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= PanBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * ScrollSpeed * 100f * Time.deltaTime;

        transform.position = pos;
    }
}
