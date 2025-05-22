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
    //key-value Ű-���� ������ �����ϴ� �÷����� �� ����
    //Ű�� �ߺ��Ǹ� �ȵ� (������ ���̿�����)

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
                Debug.Log("npc��ȭâ ����");
                npc.NowInterction(true);  
                npcUi.SetNpcUiInfo(npc);
                npcUi.Open();
                break;
            case "NpcClose":
                Debug.Log("npc��ȭâ Ŭ�ν�");
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
    public T Open<T>() where T : TestUi //Ÿ��������
    {
        string uiName = GetUiName<T>();

        if (!_uiList.ContainsKey(uiName))
        {
            T prefab = Resources.Load<T>($"Ui/{uiName}"); 
            // Resources/Ui/uiName ������ �ҷ�����
            if (prefab == null)
            {
                throw new System.Exception("������ ����");
                
            }

            T ui = Instantiate(prefab);
            //�������� ���̾��Ű�� �����ؼ� �߰�

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
