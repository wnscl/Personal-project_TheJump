using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class BasicUi : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Slider[] statSlider;
    [SerializeField] Text[] statText;

    void Start()
    {
        
    }
    private void Update()
    {
        UpdatePlayerHp();
    }

    private void UpdatePlayerHp()
    {
        Player player = UiManager.Instance.player;
        if (player == null || player.MaxHp <= 0 || player.Hp < 0)
            return;

        statSlider[0].value = (float)player.Hp / (float)player.MaxHp;
        statText[0].text = $"{player.Hp} / {player.MaxHp}";
        //UiManager.Instance.
    }
    private void UpdatePlayerSp()
    {

    }
}
