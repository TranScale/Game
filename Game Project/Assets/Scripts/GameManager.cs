using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Thư viện để chơi lại (Restart)

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Cần Kéo Vào")]
    public TextMeshProUGUI scoreText;
    //public GameObject winPanel;   // Kéo WinPanel vào đây
    //public GameObject losePanel;  // Kéo LosePanel vào đây

    [Header("Cài Đặt Game")]
    public int targetScore = 10; // Số điểm cần để thắng
    private int score = 0;
    private bool isGameEnded = false; // Cờ để tránh thắng/thua trùng nhau

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        // Cập nhật lại lần nữa khi game bắt đầu chơi
        UpdateUI();
        Time.timeScale = 1f;
    }
    private void OnValidate()
    {
        // Nó sẽ gọi cập nhật giao diện ngay cả khi game chưa chạy
        UpdateUI();
    }
    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Coin: " + score + " / " + targetScore;
        }
    }
    public void AddScore(int amount)
    {
        if (isGameEnded) return;
        score += amount;
        UpdateUI();

        if (score >= targetScore)
        {
            EndGame(true); // true = Thắng
        }
    }
    void EndGame(bool isWin)
    {
        isGameEnded = true;

        // 1. Lưu kết quả vào bộ nhớ tạm
        // "GameResult": 1 là thắng, 0 là thua
        PlayerPrefs.SetInt("GameResult", isWin ? 1 : 0);

        // 2. Có thể lưu thêm điểm số nếu muốn hiển thị bên kia
        PlayerPrefs.SetInt("FinalScore", score);

        // 3. Chuyển sang màn hình kết quả
        SceneManager.LoadScene("ResultScene");
    }
    public void GameOver() // Gọi hàm này khi nhân vật chết
    {
        if (isGameEnded) return;
        EndGame(false); // false = Thua
    }
    // Hàm để nút bấm gọi 
    public void RestartGame()
    {
        Time.timeScale = 1f; // Trả lại thời gian bình thường
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Load lại màn chơi hiện tại
    }
}