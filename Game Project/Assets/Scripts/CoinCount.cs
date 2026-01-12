using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Cài đặt chung")]
    public int coinValue = 1;

    [Header("Cài đặt âm thanh")]
    public AudioClip pickupSound; // Kéo file âm thanh "ting" vào đây
    [Range(0f, 1f)]
    public float volume = 0.5f;   // Chỉnh độ to nhỏ

    // Hàm này tự động chạy khi có vật thể chui vào vùng Trigger của đồng xu
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem vật chạm vào có đúng là Nhân vật (Tag Player) không?
        if (other.CompareTag("Player"))
        {
            // Phát âm thanh tại vị trí đồng xu trước khi nó bị phá hủy
            if (pickupSound != null)
            {
                // PlayClipAtPoint tạo ra một cái loa tạm thời, hát xong tự hủy
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
            }

            // Cộng điểm
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(coinValue);
            }

            // Biến mất (Hủy object này)
            Destroy(gameObject);
        }
    }
}