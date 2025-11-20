using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TimeRewindable : MonoBehaviour
{
    private List<RecordedFrame> frames = new List<RecordedFrame>();
    private Rigidbody rb;

    [Header("倒帶參數")]
    public float recordTime = 5f; // 記錄幾秒
    private float fixedDelta;

    private bool isRewinding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fixedDelta = Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void Record()
    {
        // 每幀記錄位置與旋轉
        if (frames.Count > Mathf.Round(recordTime / fixedDelta))
            frames.RemoveAt(frames.Count - 1);

        frames.Insert(0, new RecordedFrame(transform.position, transform.rotation));
    }

    void Rewind()
    {
        if (frames.Count > 0)
        {
            RecordedFrame frame = frames[0];
            transform.position = frame.position;
            transform.rotation = frame.rotation;
            frames.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }
}
