using UnityEngine;
//Class that processes the player vision based on the mouse movement
public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    private float xSensitivity = 30f;
    private float ySensitivity = 30f;
    public void ProcessLook(Vector2 input)
    {
        (float mouseX, float mouseY) = (input.x, input.y);
        xRotation -= (mouseY* Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}