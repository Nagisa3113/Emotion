using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RewardPanel : BasePanel
{
    CanvasGroup canvasGroup;
    Enemy enemy;

    public override void OnEnter(object args = null)
    {
        UIManager.Instance.UICanvas.gameObject.SetActive(true);

        base.OnEnter();

        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        transform.localScale = Vector3.zero;
        transform.DOScale(1, .5f);

        if (args != null)
        {
            GetReward(args as Enemy);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        canvasGroup.blocksRaycasts = false;
        transform.DOScale(0, .5f).OnComplete(() => canvasGroup.alpha = 0);
        Destroy(this.gameObject);
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();
    }

    void GetReward(Enemy enemy)
    {
        this.enemy = enemy;

        Debug.Log("玩家击败敌人获得奖励：");
        int gold = UnityEngine.Random.Range(10, 21);
        transform.Find("gold").Find("num").GetComponent<Text>().text = gold.ToString();

        switch (enemy.EnemyType)
        {
            case EnemyType.Normal:
                int start, end;
                start = (int)(CardColor.Red);
                end = (int)(CardColor.Yellow);
                for (int index = 0; index < 4; index++)
                {
                    int i = UnityEngine.Random.Range(start, end + 1);
                    transform.Find("CardSet").transform.GetChild(index).GetComponent<Good>().Content = Card.GetRandomCard((CardColor)i);
                }

                break;


            case EnemyType.Boss:
                for (int index = 0; index < 2; index++)
                {
                    transform.Find("CardSet").transform.GetChild(index).GetComponent<Good>().Content = CardSets.redCardSet[0];
                }
                break;
        }
    }

    public void OnCloseBullton()
    {
        this.gameObject.SetActive(false);
        UIManager.Instance.PopPanel();

        switch (enemy.EnemyType)
        {
            case EnemyType.Normal:
                GameManager.Instance.ReturnToMap();
                break;

            case EnemyType.Boss:
                GameManager.Instance.NextMap();
                break;
        }

    }

    public void OnMoneyButton()
    {
        int gold = Convert.ToInt32(transform.Find("gold").Find("num").gameObject.GetComponent<Text>().text);
        Player.Instance.Gold += gold;
        transform.Find("gold").gameObject.SetActive(false);
        Debug.Log("金钱+：" + gold);
    }

}
