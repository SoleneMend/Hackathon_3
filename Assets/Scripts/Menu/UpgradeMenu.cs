using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    private Player player;

    public GameObject upgradeUI;


    void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }


    public void OpenUpgradeMenu()
    {
        upgradeUI.SetActive(true);

        Time.timeScale = 0f;
    }


    public void MoreBulletSpeed()
    {
        player.projectileSpeed += 2f;

        CloseUpgradeMenu();
    }


    public void MoreDamage()
    {
        player.damage += 5f;

        CloseUpgradeMenu();
    }


    public void MoreFireRate()
    {
        player.fireRate += 0.5f;

        CloseUpgradeMenu();
    }


    void CloseUpgradeMenu()
    {
        upgradeUI.SetActive(false);

        Time.timeScale = 1f;

        // remet l'or à zéro
        GoldManager.Instance.ResetGold();
    }
}
