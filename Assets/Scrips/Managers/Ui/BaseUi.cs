using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUi : MonoBehaviour
{
    public bool isOpen = false;
    public void Open()
    {
        if (isOpen) return;

        isOpen = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        if (!isOpen) return;

        isOpen = false;
        gameObject.SetActive(false);
    }

}
