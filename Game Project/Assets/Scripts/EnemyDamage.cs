using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 20; // Sát thương mỗi lần chạm (mất 20 máu)

    // Dùng OnCollisionEnter nếu quái vật là vật rắn (có Collider không tích Trigger)
    private void OnCollisionEnter(Collision collision)
    {
        // Kiểm tra xem cái va chạm có phải là Player không
        if (collision.gameObject.CompareTag("Player"))
        {
            // Tìm script máu trên người Player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // Nếu tìm thấy thì trừ máu
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);

                // (Tuỳ chọn) Đẩy lùi nhân vật ra xa để không bị trừ máu liên tục
                // Hoặc làm quái vật biến mất sau khi nổ
            }
        }
    }
}