using UnityEngine;
using UnityEngine.SceneManagement; // Thư viện chuyển cảnh

public class MainMenuController : MonoBehaviour
{
    [Header("Kéo TutorialPanel vào đây")]
    public GameObject tutorialPanel; // Biến để chứa cái bảng luật chơi
    // Hàm này sẽ gán vào nút Play
    public void PlayGame()
    {
        // Load màn chơi game (Hãy chắc chắn tên trùng khớp với tên bạn đã lưu)
        SceneManager.LoadScene("GameScene");
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // Chắc chắn là game không còn bị dừng (Pause)

        // Load lại chính màn chơi hiện tại (GameScene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Hàm này để thoát game (Dùng cho nút Quit nếu bạn muốn làm thêm)
    public void QuitGame()
    {
        // Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Đã thoát game!");
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // QUAN TRỌNG: Phải trả thời gian về bình thường trước khi chuyển cảnh
        SceneManager.LoadScene("MainMenu");
    }
    // Hàm gọi khi bấm nút "Cách Chơi"
    public void OpenTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true); // Hiện bảng lên
        }
    }

    // Hàm gọi khi bấm nút "Đóng" (X)
    public void CloseTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false); // Ẩn bảng đi
        }
    }
}