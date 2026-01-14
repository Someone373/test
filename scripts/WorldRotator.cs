using UnityEngine;
using System.Collections;

public class WorldRotator : MonoBehaviour
{
    public float rotateDuration = 2f;
    public float rotateAngle = 90f;
    bool rotating = false;

    public IEnumerator Rotate90(Vector3 axis)
    {
        if (rotating) yield break;
        rotating = true;

        Quaternion start = transform.rotation;
        Quaternion target = Quaternion.AngleAxis(rotateAngle, axis) * start;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / rotateDuration;
            transform.rotation = Quaternion.Slerp(start, target, t);
            yield return null;
        }

        transform.rotation = target;
        rotating = false;
    }
}
