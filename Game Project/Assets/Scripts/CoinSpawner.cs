using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Cài đặt")]
    public GameObject coinPrefab;
    public int numberOfCoins = 20;
    public float coinOffset = 0.5f;

    [Header("Quan trọng: Chọn Layer Đất")]
    public LayerMask whatIsGround;

    private BoxCollider spawnArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();
        if (spawnArea == null)
        {
            Debug.LogError("LỖI: CoinSpawner chưa có BoxCollider!");
            return;
        }
        SpawnCoins();
    }

    void SpawnCoins()
    {
        int successCount = 0;
        int maxAttempts = numberOfCoins * 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            if (successCount >= numberOfCoins) break;

            if (SpawnSingleCoin())
            {
                successCount++;
            }
        }

        Debug.Log($"Kết quả: Đã sinh được {successCount} / {numberOfCoins} đồng xu.");
    }

    bool SpawnSingleCoin()
    {
        Bounds bounds = spawnArea.bounds;

        // Random vị trí
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        // Điểm bắt đầu bắn tia
        Vector3 rayOrigin = new Vector3(x, bounds.max.y, z);

        // Vẽ tia laser màu đỏ trong Scene để bạn nhìn thấy (tồn tại trong 10 giây)
        Debug.DrawRay(rayOrigin, Vector3.down * bounds.size.y, Color.red, 10f);

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hitInfo, bounds.size.y, whatIsGround, QueryTriggerInteraction.Ignore))
        {
            Vector3 spawnPos = hitInfo.point + Vector3.up * coinOffset;
            Instantiate(coinPrefab, spawnPos, coinPrefab.transform.rotation);

            // Vẽ tia màu xanh lá nếu bắn TRÚNG đất
            Debug.DrawLine(rayOrigin, hitInfo.point, Color.green, 10f);
            return true;
        }

        return false;
    }
}