using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private bool isRewinding = false;

    void Update()
    {
        // 按下 F 鍵啟動/停止倒帶
        if (Input.GetKeyDown(KeyCode.F))
            StartRewindAll();
        if (Input.GetKeyUp(KeyCode.F))
            StopRewindAll();
    }

    void StartRewindAll()
    {
        isRewinding = true;
        foreach (TimeRewindable obj in FindObjectsOfType<TimeRewindable>())
        {
            obj.StartRewind();
        }
    }

    void StopRewindAll()
    {
        isRewinding = false;
        foreach (TimeRewindable obj in FindObjectsOfType<TimeRewindable>())
        {
            obj.StopRewind();
        }
    }
}
