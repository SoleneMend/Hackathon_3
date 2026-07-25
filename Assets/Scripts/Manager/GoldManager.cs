using UnityEngine;
using System;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance { get; private set; }    
    public int CurrentGold { get; private set; }
    public int GoldToLevelUp = 100;
    public event Action<int> OnGoldChanged;
    public event Action OnLevelUp;

    private Player player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        player = FindAnyObjectByType<Player>();
    }

     public void AddGold(int amount)
    {
        CurrentGold += amount;

        OnGoldChanged?.Invoke(CurrentGold);


        if(CurrentGold >= GoldToLevelUp)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        CurrentGold = 0;

        OnGoldChanged?.Invoke(CurrentGold);

        OnLevelUp?.Invoke();


        UpgradeMenu upgradeMenu =
            FindAnyObjectByType<UpgradeMenu>();

        if(upgradeMenu != null)
        {
            upgradeMenu.OpenUpgradeMenu();
        }

        player.Heals(50);
    }

    public void ResetGold()
    {
        CurrentGold = 0;
        OnGoldChanged?.Invoke(CurrentGold);
    }
}