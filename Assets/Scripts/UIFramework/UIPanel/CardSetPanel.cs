using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CardSetPanel : BasePanel
{
    CanvasGroup canvasGroup;
    CardSet cardSet;

    public override void OnEnter(object args = null)
    {
        base.OnEnter();
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 1;
        int count = transform.parent.childCount;

        transform.SetSiblingIndex(count - 1);
        //transform.localScale = Vector3.zero;
        //transform.DOScale(1, .5f);

        if (args != null)
        {
            UpdateContent(args as CardSet);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        canvasGroup.alpha = 0;
        //transform.DOScale(0, .5f).OnComplete(() => canvasGroup.alpha = 0);
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

    void UpdateContent(CardSet cardSet)
    {

    }
}
