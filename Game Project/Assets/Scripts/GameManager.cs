using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Thư viện để chơi lại (Restart)

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Cần Kéo Vào")]
    public TextMeshProUGUI scoreText;
    public GameObject winPanel;   // Kéo WinPanel vào đây
    public GameObject losePanel;  // Kéo LosePanel vào đây

    [Header("Cài Đặt Game")]
    public int targetScore = 10; // Số điểm cần để thắng
    private int score = 0;
    private bool isGameEnded = false; // Cờ để tránh thắng/thua trùng nhau

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        if (isGameEnded) return; // Nếu game đã kết thúc thì không tính nữa

        score += amount;
        if (scoreText != null) scoreText.text = "Score: " + score;

        // KIỂM TRA ĐIỀU KIỆN THẮNG
        if (score >= targetScore)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        isGameEnded = true;
        Debug.Log("Chiến thắng!");

        // Hiện màn hình thắng
        if (winPanel != null) winPanel.SetActive(true);

        // Dừng thời gian lại (Mọi thứ đứng yên)
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        if (isGameEnded) return;

        isGameEnded = true;
        Debug.Log("Thất bại!");

        // Hiện màn hình thua
        if (losePanel != null) losePanel.SetActive(true);

        // Dừng thời gian
        Time.timeScale = 0f;
    }

    // Hàm để nút bấm gọi (sẽ làm ở Bước 4)
    public void RestartGame()
    {
        Time.timeScale = 1f; // Trả lại thời gian bình thường
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Load lại màn chơi hiện tại
    }
}