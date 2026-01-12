using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // Biến này giúp các script khác biết tổng số xu để tính Thắng/Thua
    public static int MaxCoinsInLevel;

    [Header("Cài đặt chung")]
    public GameObject coinPrefab;
    public int numberOfCoins = 20;
    public float coinOffset = 1.0f;
    public float minHeight = -6.5f; // Độ cao tối thiểu để sinh xu

    [Header("Bộ lọc Layer")]
    public LayerMask whatIsGround;   // Layer của Đất (Ground)
    public LayerMask whatIsObstacle; // Layer của Vật cản (Cây, Đá, Nhà...)

    [Header("Tinh chỉnh vùng trống")]
    [Tooltip("Bán kính vùng an toàn. Nếu trong vòng tròn này có cây/đá, sẽ không sinh xu.")]
    public float checkRadius = 1.0f;

    private BoxCollider spawnArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();
        SpawnCoins();
    }

    void SpawnCoins()
    {
        int successCount = 0;
        int attempts = 0;
        int maxAttempts = numberOfCoins * 20; // Tăng số lần thử lên

        while (successCount < numberOfCoins && attempts < maxAttempts)
        {
            if (SpawnSingleCoin())
            {
                successCount++;
            }
            attempts++;
        }

        // Cập nhật tổng số xu thực tế sinh được vào biến tĩnh
        MaxCoinsInLevel = successCount;

    }

    bool SpawnSingleCoin()
    {
        Bounds bounds = spawnArea.bounds;

        // 1. Random vị trí
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        Vector3 rayOrigin = new Vector3(x, bounds.max.y, z);

        // 2. Bắn tia tìm Đất
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hitInfo, bounds.size.y, whatIsGround, QueryTriggerInteraction.Ignore))
        {
            Vector3 spawnPos = hitInfo.point + Vector3.up * coinOffset;

            // Nếu điểm chạm đất (hitInfo.point.y)
            if (hitInfo.point.y < minHeight)
            {
                return false;
            }

            // --- KIỂM TRA VẬT CẢN ---

            // Kiểm tra xem nó có chạm vào "whatIsObstacle" không
            if (Physics.CheckSphere(spawnPos, checkRadius, whatIsObstacle))
            {
                return false; // Bị vướng cây/đá -> Hủy bỏ, không sinh
            }

            // --- NẾU KHÔNG VƯỚNG GÌ THÌ MỚI SINH ---
            Instantiate(coinPrefab, spawnPos, coinPrefab.transform.rotation);
            return true;
        }

        return false;
    }

    // Vẽ hình cầu trong Scene để bạn dễ hình dung vùng kiểm tra (Chỉ hiện khi không Play)
    void OnDrawGizmosSelected()
    {
        if (spawnArea == null) return;
        Gizmos.color = Color.red;
        // Vẽ minh họa một quả cầu check tại tâm của hộp
        Gizmos.DrawWireSphere(spawnArea.bounds.center, checkRadius);
    }
}