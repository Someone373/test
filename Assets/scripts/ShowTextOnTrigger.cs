using UnityEngine;
using UnityEngine.UI;

public class ShowTextOnTrigger : MonoBehaviour
{
    public UnityEngine.UI.Text messageText; // 指定使用 Unity 的 UI Text 類別
    public string messageToShow;
    public float displayDuration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.text = messageToShow;
            CancelInvoke(nameof(HideText));
            Invoke(nameof(HideText), displayDuration);
        }
    }

    void HideText()
    {
        messageText.text = "";
    }
}
