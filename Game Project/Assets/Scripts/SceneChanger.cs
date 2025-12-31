using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Chuyển bằng tên của Scene
    public void ChangeSceneByName(string SampleSence)
    {
        SceneManager.LoadScene(SampleSence);
    }

    // Chuyển bằng chỉ số Index trong Build Settings
    public void ChangeSceneByIndex(int Begin)
    {
        SceneManager.LoadScene(Begin);
    }

    // Load lại Scene hiện tại (thường dùng khi Game Over)
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}