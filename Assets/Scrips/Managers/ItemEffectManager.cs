using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectManager : MonoBehaviour
{
    public static ItemEffectManager Instance { get; private set; }

    public PlayerInventory inventory;
    public Player player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // 씬 전환 시 유지하고 싶다면 사용
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }

    public void InjectItemEffect(int num)
    {
        Item itemToUse = inventory.itemSlot[num].GetComponentInChildren<Item>();
        int code = itemToUse.itemCode;
        //player.StartCoroutine(HealPlayer());
        switch (code)
        {
            case 0:
                itemToUse.onUseEvent = player.OnHealPlayer;
                break;

            case 1:
                itemToUse.onUseEvent = player.OnSpeedUpPlayer;
                break;
        }
    }
}
