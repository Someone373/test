using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float interval = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("ToggleVisibility", 0f, interval);
    }
    void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
