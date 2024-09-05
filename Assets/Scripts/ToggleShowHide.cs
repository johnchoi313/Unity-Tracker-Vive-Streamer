using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleShowHide : MonoBehaviour
{
    public GameObject obj;


    public void ShowHideToggle() {
        obj.SetActive(!obj.activeSelf);
    }
}
