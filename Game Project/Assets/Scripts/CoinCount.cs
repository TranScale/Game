using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    // Hàm này tự động chạy khi có vật thể chui vào vùng Trigger của đồng xu
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem vật chạm vào có đúng là Nhân vật (Tag Player) không?
        if (other.CompareTag("Player"))
        {
            // 1. Cộng điểm
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(coinValue);
            }

            // 2. Biến mất (Hủy object này)
            Destroy(gameObject);
        }
    }
}