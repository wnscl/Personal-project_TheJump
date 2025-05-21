using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NpcUi : BaseUi
{
    [SerializeField] Image npcImage;
    [SerializeField] Text npcName;
    [SerializeField] Text[] textData;

    BasicNpc npc;

    public void SetNpcUiInfo(string name ,string[] npcData)
    {
        npcName.text = name;
        for (int i = 0; i < npcData.Length; i++)
        {
            //textData[i] = npcData[i];
        }
        
    }

}
