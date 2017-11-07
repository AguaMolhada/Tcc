using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class used in the camera moviment.
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Speed that the camera will move.
    /// </summary>
    public float panSpeed = 10f;
    /// <summary>
    /// Ammout distance of the border that the camera will move when using the mouse.
    /// </summary>
    public float PanBorderThickness = 10f;
    /// <summary>
    /// Ammout zoom speed.
    /// </summary>
    public float ScrollSpeed = 20;
    /// <summary>
    /// Min camera position.
    /// </summary>
    public Vector3 MinPos;
    /// <summary>
    /// Max camera position.
    /// </summary>
    public Vector3 MaxPos;

    private void Update()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height -PanBorderThickness)
        {
            pos.z += (panSpeed * pos.y / 10) * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= PanBorderThickness)
        {
            pos.z -= (panSpeed * pos.y / 10) * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - PanBorderThickness)
        {
            pos.x += (panSpeed * pos.y / 10) * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= PanBorderThickness)
        {
            pos.x -= (panSpeed * pos.y / 10) * Time.deltaTime;
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * ScrollSpeed* 100f * Time.deltaTime;

        transform.position = Ultility.ClampVector3(pos, MinPos, MaxPos);
    }
}
