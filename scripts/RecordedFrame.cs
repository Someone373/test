using UnityEngine;

[System.Serializable]
public class RecordedFrame
{
    public Vector3 position;
    public Quaternion rotation;

    public RecordedFrame(Vector3 pos, Quaternion rot)
    {
        position = pos;
        rotation = rot;
    }
}
