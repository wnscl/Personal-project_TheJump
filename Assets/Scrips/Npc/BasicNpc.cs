using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum NpcState
{
    None,
    Idle,
    Talk
}

public class BasicNpc : MonoBehaviour
{
    NpcState currentState;
    NpcState nextState;

    [SerializeField] string name;
    [SerializeField] string[] dialog;
    [SerializeField] Animator anim;

    private void Awake()
    {
        
    }
    private void Update()
    {
        
    }

    private void DecideState()
    {
        
    }

}
