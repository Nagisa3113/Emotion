using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ShopPanel : BasePanel
{
    CanvasGroup canvasGroup;
    public Good[] goods;

    public override void OnEnter(object args = null)
    {
        base.OnEnter();
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        transform.localScale = Vector3.zero;
        transform.DOScale(1, .5f);

        this.goods = GetComponentsInChildren<Good>();
    }

    public override void OnExit()
    {
        base.OnExit();
        canvasGroup.blocksRaycasts = false;
        transform.DOScale(0, .5f).OnComplete(() => canvasGroup.alpha = 0);
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();

    }

    public void OnCloseBullton()
    {

        UIManager.Instance.PopPanel();
    }

}
