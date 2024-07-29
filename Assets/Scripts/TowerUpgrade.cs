using UnityEngine;
using UnityEngine.UI;

public class TowerUpgrade : MonoBehaviour
{
    public Button upgradeAttackSpeedButton;
    public Button upgradeAttackPowerButton;
    public int attackSpeedUpgradeCost = 10;
    public int attackPowerUpgradeCost = 20;

    void Start()
    {
        upgradeAttackSpeedButton.onClick.AddListener(UpgradeAttackSpeed);
        upgradeAttackPowerButton.onClick.AddListener(UpgradeAttackPower);
    }

    void UpgradeAttackSpeed()
    {
        if (SoulManager.instance.SpendSouls(attackSpeedUpgradeCost))
        {
            // Implement upgrade logic here
            Debug.Log("Attack Speed Upgraded!");
        }
        else
        {
            Debug.Log("Not enough souls!");
        }
    }

    void UpgradeAttackPower()
    {
        if (SoulManager.instance.SpendSouls(attackPowerUpgradeCost))
        {
            // Implement upgrade logic here
            Debug.Log("Attack Power Upgraded!");
        }
        else
        {
            Debug.Log("Not enough souls!");
        }
    }
}
