using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Kéo cái PausePanel")]
    public GameObject pausePanel;

    // Biến để kiểm tra xem game đang dừng hay chạy
    public static bool isPaused = false;

    void Start()
    {
        // Đảm bảo lúc đầu panel tắt
        if (pausePanel != null) pausePanel.SetActive(false);
    }

    void Update()
    {
        // Bấm nút ESC để bật/tắt menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pausePanel.SetActive(false); // Ẩn menu
        Time.timeScale = 1f;         // Thời gian chạy lại bình thường
        isPaused = false;
    }

    void Pause()
    {
        pausePanel.SetActive(true);  // Hiện menu
        Time.timeScale = 0f;         // Đóng băng thời gian
        isPaused = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Nhớ trả lại thời gian trước khi load
        SceneManager.LoadScene("GameScene"); // Tên Scene chơi game của bạn
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Tên Scene Menu của bạn
    }

    public void QuitGame()
    {
        Debug.Log("Đang thoát game...");
        Application.Quit(); // Chỉ hoạt động khi đã Build ra file .exe
    }
}