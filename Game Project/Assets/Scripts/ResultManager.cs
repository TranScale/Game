using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [Header("UI Cần Kéo Vào")]
    public GameObject winUI;  // Kéo cái UI Thắng vào đây
    public GameObject loseUI; // Kéo cái UI Thua vào đây

    void Start()
    {
        // Lấy dữ liệu đã lưu từ GameManager
        int result = PlayerPrefs.GetInt("GameResult", 0); // Mặc định là 0 nếu không tìm thấy
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);

        // Kiểm tra kết quả
        if (result == 1)
        {
            Debug.Log("Đang hiện màn hình Thắng");
            if (winUI != null) winUI.SetActive(true);
            if (loseUI != null) loseUI.SetActive(false);

            // Phát nhạc thắng (nếu có)
            if (AudioManager.instance != null) AudioManager.instance.PlayWinSound();
        }
        else
        {
            Debug.Log("Đang hiện màn hình Thua");
            if (winUI != null) winUI.SetActive(false);
            if (loseUI != null) loseUI.SetActive(true);

            // Phát nhạc thua (nếu có)
            if (AudioManager.instance != null) AudioManager.instance.PlayLoseSound();
        }
    }

    // Hàm cho nút "Chơi Lại"
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene"); // Load lại scene chơi game
    }

    // Hàm cho nút "Về Menu"
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}