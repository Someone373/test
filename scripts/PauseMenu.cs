using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public MonoBehaviour[] scriptsToDisable;
    public bool EscapeToDisable = true;
    private SettingMenu SettingMenuScript;

    void Start()
    {
        // 遊戲開始時隱藏滑鼠並鎖定
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&EscapeToDisable)
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // 隱藏滑鼠並鎖定
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        foreach (var script in scriptsToDisable)
        script.enabled = true;   // 重新啟用腳本
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        

        // 顯示滑鼠並解鎖
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        foreach (var script in scriptsToDisable)
        script.enabled = false;  // 停用腳本
    }

    public void Setting()
    {
        EscapeToDisable = false;
    }

    public bool IsEscapeToDisable()
    {
        return EscapeToDisable;
    }

    public void SettingToPause()
    {
        pauseMenuUI.SetActive(true);
        EscapeToDisable = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
