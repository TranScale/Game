using UnityEngine;
using UnityEngine.UI; // Bắt buộc có dòng này để chỉnh UI Slider

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Slider healthSlider; // Kéo thanh Slider vào đây

    void Start()
    {
        // Lúc đầu game, máu đầy
        currentHealth = maxHealth;

        // Cập nhật thanh máu hiển thị đúng số máu
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    // Hàm này sẽ được gọi khi bị quái đánh
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Trừ máu

        // Đảm bảo máu không âm (nhỏ nhất là 0)
        if (currentHealth < 0) currentHealth = 0;

        // Cập nhật thanh máu trên màn hình
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        // Kiểm tra xem chết chưa
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Gọi hàm GameOver bên GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}