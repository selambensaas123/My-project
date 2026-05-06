using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float mouseSensitivity = 100f;

    private float rotX;
    private float rotY;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotX = angles.y;
        rotY = angles.x;
    }

    void Update()
    {
        Vector3 move = Vector3.zero;

        if (Keyboard.current.wKey.isPressed) move += transform.forward;
        if (Keyboard.current.sKey.isPressed) move -= transform.forward;
        if (Keyboard.current.dKey.isPressed) move += transform.right;
        if (Keyboard.current.aKey.isPressed) move -= transform.right;

        transform.position += move * moveSpeed * Time.deltaTime;

        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            rotX += mouseDelta.x * mouseSensitivity * Time.deltaTime;
            rotY -= mouseDelta.y * mouseSensitivity * Time.deltaTime;

            rotY = Mathf.Clamp(rotY, -80f, 80f);

            transform.rotation = Quaternion.Euler(rotY, rotX, 0f);
        }
    }
}