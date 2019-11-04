using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewBuff : MonoBehaviour
{
    public TextMesh layer;
    public TextMesh tip;
    public SpriteRenderer back;

    void Update()
    {
        int temp;
        if (transform.parent.name == "PlayerBuffs")
        {
            temp = View.Instance.player.GetBuffManager.CheckLayer(transform.name);
        }
        else
        {
            temp = View.Instance.enemy.GetBuffManager.CheckLayer(transform.name);
        }
        layer.text = temp.ToString();
    }

    void OnMouseEnter()
    {
        string temp;
        if (transform.parent.name == "PlayerBuffs")
        {
            temp = View.Instance.player.GetBuffManager.CheckTip(transform.name);
        }
        else
        {
            temp = View.Instance.enemy.GetBuffManager.CheckTip(transform.name);
        }
        tip.gameObject.SetActive(true);
        tip.GetComponent<TextMesh>().text = temp.ToString();

        back.transform.localScale = new Vector3(0.1F, 0, 0) * temp.ToString().Length + new Vector3(0.1F, 0.4f, 0);
    }

    void OnMouseExit()
    {
        tip.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
    }

}
