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
        if (CoinManager.instance.SpendCoins(attackSpeedUpgradeCost))
        {
            // Implement upgrade logic here
            Debug.Log("Attack Speed Upgraded!");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    void UpgradeAttackPower()
    {
        if (CoinManager.instance.SpendCoins(attackPowerUpgradeCost))
        {
            // Implement upgrade logic here
            Debug.Log("Attack Power Upgraded!");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}
