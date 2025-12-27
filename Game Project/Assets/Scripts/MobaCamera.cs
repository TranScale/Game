using UnityEngine;

public class MobaCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Fixed Offset (WORLD SPACE)")]
    public Vector3 offset = new Vector3(6f, 10f, -6f);

    [Header("Zoom")]
    public float zoomSpeed = 4f;
    public float minHeight = 8f;
    public float maxHeight = 14f;

    [Header("Smooth")]
    public float smoothSpeed = 5f;

    void Start()
    {
        // Góc nhìn 45° CỐ ĐỊNH
        transform.rotation = Quaternion.Euler(45f, 0f, 0f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Zoom chỉ thay đổi độ cao (KHÔNG xoay)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        offset.y -= scroll * zoomSpeed;
        offset.y = Mathf.Clamp(offset.y, minHeight, maxHeight);

        // Giữ góc 45° (height ≈ distance)
        offset.z = -offset.y;

        // Camera CHỈ theo vị trí nhân vật (WORLD SPACE)
        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // Nhìn cố định xuống nhân vật
        transform.LookAt(target.position);
    }
}
