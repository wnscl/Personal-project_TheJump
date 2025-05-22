using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }

    public Player player;

    [SerializeField] BaseUi basicUi;
    [SerializeField] NpcUi npcUi;
    public int uiIdx = 0;
    //public bool interctionMode = false;


    Dictionary<string, TestUi> _uiList = new();
    //key-value 키-값을 쌍으로 저장하는 컬렉션의 한 종류
    //키값 중복되면 안됨 (고유의 값이여야함)

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

    private void Update()
    {
        if (Input.GetKeyDown("7"))
        {
            TestDamage();
        }
    }

    public void PlayerUiInterctionOrder(string order, BasicNpc npc)
    {
        basicUi.Close();
        npcUi.Close(); 

        switch (order)
        {
            case "NpcOpen":
                Debug.Log("npc대화창 오픈");
                npc.NowInterction(true);  
                npcUi.SetNpcUiInfo(npc);
                npcUi.Open();
                break;
            case "NpcClose":
                Debug.Log("npc대화창 클로스");
                npc.NowInterction(false);
                npcUi.InitInfo();
                npcUi.Close();
                player.interctionMode = false;
                break;
        }

    }

    void TestDamage()
    {
        player.TakeDamage(10);    
    }

    /*private string GetUiName<T>() where T : TestUi
    {
        return typeof(T).Name;
    }
    public T Open<T>() where T : TestUi //타입제한자
    {
        string uiName = GetUiName<T>();

        if (!_uiList.ContainsKey(uiName))
        {
            T prefab = Resources.Load<T>($"Ui/{uiName}"); 
            // Resources/Ui/uiName 프리팹 불러오기
            if (prefab == null)
            {
                throw new System.Exception("프리팹 없음");
                
            }

            T ui = Instantiate(prefab);
            //프리팹을 하이어라키에 복사해서 추가

            _uiList.Add(uiName, ui);
            return ui;
        }

        TestUi spawnedUi = _uiList[uiName];
        spawnedUi.Open();
        return spawnedUi as T;  
    }

    public void Close<T>(bool kill = false) where T : TestUi
    {
        string uiName = GetUiName<T>();
        if (!_uiList.TryGetValue(uiName, out TestUi savedUi))
        {
            return;
        }

        savedUi.Close(kill);
        if (kill)
        {
            _uiList.Remove(uiName);
        }
    }
    public bool TryGet<T>(out T ui) where T : TestUi
    {
        ui = null;

        string uiName = GetUiName<T>();

        if (!_uiList.TryGetValue(uiName, out TestUi savedUi))
        {
            return false;   
        }
        ui = savedUi as T;

        return true;
    }*/












}
