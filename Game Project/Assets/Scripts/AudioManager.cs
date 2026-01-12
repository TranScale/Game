using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Tạo Singleton để dễ gọi từ bất kỳ đâu
    public static AudioManager instance;

    [Header("Nguồn phát")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Danh sách âm thanh")]
    public AudioClip winSound;
    public AudioClip loseSound;

    [Header("Cài đặt Âm lượng")]
    [Range(0f, 1f)]
    public float sfxVolume = 1f;

    void Awake()
    {
        instance = this;
    }

    // Hàm gọi khi Thắng
    public void PlayWinSound()
    {
        musicSource.Stop(); // Tắt nhạc nền đi cho kịch tính
        sfxSource.PlayOneShot(winSound, sfxVolume); // Phát tiếng thắng 1 lần
    }

    // Hàm gọi khi Thua
    public void PlayLoseSound()
    {
        musicSource.Stop(); // Tắt nhạc nền
        sfxSource.PlayOneShot(loseSound, sfxVolume); // Phát tiếng thua
    }
}