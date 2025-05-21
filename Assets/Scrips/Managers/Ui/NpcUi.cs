using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class NpcUi : BaseUi
{
    [SerializeField] Image npcImage;
    [SerializeField] Text npcNameText;
    [SerializeField] Text talkText;
    [SerializeField] BasicNpc npc;
    public string[] dialogData;
    public int talkIdx = 0;

    public void OnNext()
    {
        if (talkIdx +1 >= dialogData.Length)
        {
            UiManager.Instance.PlayerUiInterctionOrder("NpcClose", npc);
            return;
        }
        talkIdx++;
        talkText.text = dialogData[talkIdx];
    }
    public void SetNpcUiInfo(BasicNpc npc)
    {
        if (npc.dialog == null || npc.dialog.Length == 0)
        //방어로직
        {
            return;
        }

        if (dialogData.Length < npc.dialog.Length)
        //크기 재설정
        {
            Array.Resize(ref dialogData, npc.dialog.Length);
        }

        npcImage.sprite = npc.photo;
        npcNameText.text = npc.name;

        for (int i = 0; i < npc.dialog.Length; i++)
        {
            dialogData[i] = npc.dialog[i];
        }
        talkText.text = dialogData[talkIdx];
    }
    public void InitInfo()
    {
        talkIdx = 0;
    }
}
