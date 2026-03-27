using UnityEngine;
using UnityEngine.InputSystem;

public class FlightController : MonoBehaviour
{
    [SerializeField] private float pitchSpeed = 30f;
    [SerializeField] private float yawSpeed = 30f;
    [SerializeField] private float rollSpeed = 30f;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float verticalSpeed = 10f;

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        float pitch = 0f;
        float yaw = 0f;
        float roll = 0f;

        if (Keyboard.current.upArrowKey.isPressed)
            pitch = -1f;

        if (Keyboard.current.downArrowKey.isPressed)
            pitch = 1f;

        if (Keyboard.current.leftArrowKey.isPressed)
            yaw = -1f;

        if (Keyboard.current.rightArrowKey.isPressed)
            yaw = 1f;

        if (Keyboard.current.qKey.isPressed)
            roll = 1f;

        if (Keyboard.current.eKey.isPressed)
            roll = -1f;

        transform.Rotate(Vector3.right * pitch * pitchSpeed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.up * yaw * yawSpeed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.forward * roll * rollSpeed * Time.deltaTime, Space.Self);

        if (Keyboard.current.spaceKey.isPressed)
            transform.position += transform.right * moveSpeed * Time.deltaTime;

        if (Keyboard.current.leftShiftKey.isPressed)
            transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime, Space.World);

        if (Keyboard.current.leftCtrlKey.isPressed)
            transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime, Space.World);
    }
}