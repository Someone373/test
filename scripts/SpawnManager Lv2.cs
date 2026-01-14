using System.Numerics;
using UnityEngine;

public class SpawnManagerLv2 : MonoBehaviour
{
    public static SpawnManagerLv2 Instance; // 單例模式，方便 Checkpoint 調用

    public Transform startPoint;
    public Transform worldRoot;
    private Transform currentRespawn;
    private GameObject player;
    private CheckpointLv2 activeCheckpoint;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null && startPoint != null)
        {
            currentRespawn = startPoint;
            MovePlayerTo(currentRespawn);
        }
    }

    public void SetCheckpoint(CheckpointLv2 newCheckpoint)
    {
        if (activeCheckpoint == newCheckpoint) return; // 避免重複觸發

        if (activeCheckpoint != null) activeCheckpoint.Deactivate();

        activeCheckpoint = newCheckpoint;
        currentRespawn = newCheckpoint.transform;
        activeCheckpoint.Activate();
    }

    void Update()
    {
        if (player != null && currentRespawn != null)
        {
            // 建議將 -30 設為變數，方便調整
            if (player.transform.position.y <= -30 || Input.GetKeyDown(KeyCode.R))
            {
                MovePlayerTo(currentRespawn);
            }
        }
    }

    private void MovePlayerTo(Transform point)
    {
        // 1. 先處理場景旋轉
        if (activeCheckpoint != null && worldRoot != null)
        {
            worldRoot.rotation = activeCheckpoint.savedWorldRotation;
        }

        // 2. 處理物理狀態
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = UnityEngine.Vector3.zero;
            rb.angularVelocity = UnityEngine.Vector3.zero;
            // 暫時關閉物理模擬以確保座標強制對齊
            rb.isKinematic = true; 
        }

        // 3. 設定位置
        player.transform.position = point.position + UnityEngine.Vector3.up;
        player.transform.rotation = point.rotation;

        if (rb != null) rb.isKinematic = false;
    }
}