using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    public float speed = 100f; // Tốc độ xoay

    void Update()
    {
        // Xoay quanh trục Y của thế giới (World Up) để nó quay tại chỗ giống con vụ
        // Space.World đảm bảo dù đồng xu đang nghiêng thế nào, nó vẫn xoay theo trục thẳng đứng của đất
        transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
    }
}