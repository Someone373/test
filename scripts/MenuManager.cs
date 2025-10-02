using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject levelPanel;
    public GameObject settingsPanel;

    [Header("Level Settings")]
    public string levelNamePrefix = "Level"; // 例如 Level1, Level2...

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ShowMain();
    }

    public void ShowMain()
    {
        mainPanel.SetActive(true);
        levelPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ShowLevelPanel()
    {
        levelPanel.SetActive(true);
        mainPanel.SetActive(false);     
        settingsPanel.SetActive(false);
        Debug.Log("Level Panel Shown");
    }

    public void ShowSettings()
    {
        mainPanel.SetActive(false);
        levelPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // Inspector 綁定按鈕時直接傳數字進來
    public void LoadLevelByIndex(int index)
    {
        string sceneName = levelNamePrefix + index;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
