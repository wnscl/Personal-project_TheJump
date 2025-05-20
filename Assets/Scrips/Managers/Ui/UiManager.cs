using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UiManager : MonoBehaviour
{
    public Player player;
    [SerializeField] UnityEngine.UI.Slider[] statSlider;
    [SerializeField] Text[] statText;


    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            TestDamage();
        }
        UpdatePlayerHp();
    }

    private void UpdatePlayerHp()
    {
        if (player.Hp >= 0)
        {
            statSlider[0].value = (float)player.Hp / (float)player.MaxHp;
            statText[0].text = $"{player.Hp} / {player.MaxHp}";
        }
        else
        {
            return;
        }

    }
    private void UpdatePlayerSp()
    {

    }


    void TestDamage()
    {
        player.TakeDamage(10);    
    }
}
