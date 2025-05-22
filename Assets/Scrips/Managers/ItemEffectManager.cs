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
            //DontDestroyOnLoad(gameObject); // �� ��ȯ �� �����ϰ� �ʹٸ� ���
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
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
