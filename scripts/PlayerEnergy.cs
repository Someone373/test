using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy: MonoBehaviour
{
    public Image EnergyBar;  // 指向 UI 的血條
    public float maxEnergy = 100f;
    private float currentEnergy;

    void Start()
    {
        currentEnergy = maxEnergy;
        UpdateEnergyUI();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            ConsumeEnergy(20);
            Debug.Log("消耗能量，當前能量：" + currentEnergy);
        }

        if(Input.GetKeyDown(KeyCode.N)){
            GainEnergy(5);
            Debug.Log("恢復能量，當前能量：" + currentEnergy);
        }
    }

    public void ConsumeEnergy(float consume)
    {
        currentEnergy -= consume;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        UpdateEnergyUI();
    }

    public void GainEnergy(float gain)
    {
        currentEnergy += gain;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        UpdateEnergyUI();
    }

    void UpdateEnergyUI()
    {
        EnergyBar.fillAmount = currentEnergy / maxEnergy;
    }
}
