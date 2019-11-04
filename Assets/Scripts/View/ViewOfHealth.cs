using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ViewOfHealth : MonoBehaviour
{
    Text healhText;

    void Awake()
    {
        healhText = GetComponentInChildren<Text>();
    }

    public void MouseEnter()
    {
        healhText.enabled = true;

        if (transform.name == "player")
        {
            healhText.text = Player.Instance.HP.ToString() + '/' + Player.Instance.HPMax.ToString();
        }
        else if (transform.name == "enemy")
        {
            healhText.text = View.Instance.enemy.HP.ToString() + '/' + View.Instance.enemy.HPMax.ToString();
        }
    }

    public void OnPonitExit()
    {
        healhText.enabled = false;
    }

}
