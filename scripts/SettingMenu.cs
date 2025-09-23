using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    private PauseMenu PauseMenuScript;
    private FollowPlayer FollowPlayerScript;
    public GameObject SettingMenuUI;    

    void Start()
    {
        PauseMenuScript = FindObjectOfType<PauseMenu>();
        FollowPlayerScript = FindObjectOfType<FollowPlayer>();
    }

    void Update()
    {
        if(PauseMenuScript != null && Input.GetKeyDown(KeyCode.Escape) && !PauseMenuScript.IsEscapeToDisable())
        {
            SettingMenuUI.SetActive(false);
            PauseMenuScript.SettingToPause();
        }
    }
}
