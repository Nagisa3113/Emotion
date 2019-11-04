using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewCardFront : MonoBehaviour
{
    public TextMesh name;
    public SpriteRenderer front;
    public TextMesh tip;
    public TextMesh bonus;
    public SpriteRenderer expenseSprite;
    public TextMesh expenseText;
    public SpriteRenderer upgradeSprite;
    public TextMesh upgradeText;

    bool tag;
    bool hasPut;

    public void ShowCard(Role role, Card card)
    {
        transform.name = card.Name.ToString();

        name.text = card.CName;
        tip.text = card.Tip();

        if ((int)card.Color >= 5)
            return;

        front.sprite = GameResources.s_cardFront[(int)card.Color];
        expenseSprite.sprite = GameResources.s_expenseColor[(int)card.Color];
        expenseText.text = card.Cost.ToString();

        upgradeSprite.sprite = GameResources.s_upgradeColor[(int)card.Color];
        upgradeText.text = card.Upgrade.ToString();

        if (role.CardManager.GetBonus(card.Color) >= card.UpgradeTwice)
        {
            tip.text = card.TipUpgradeTwice(role);
            expenseSprite.sprite = GameResources.s_upgradeColor[(int)card.Color];
            upgradeText.text = "MAX";
        }
        else if (role.CardManager.GetBonus(card.Color) >= card.Upgrade)
        {
            tip.text = card.TipUpgrade(role);
            expenseSprite.sprite = GameResources.s_upgradeColor[(int)card.Color];
            if (card.UpgradeTwice < 100)
                upgradeText.text = card.UpgradeTwice.ToString();
            else
                upgradeText.text = "MAX";
        }
        else
        {
            tip.text = card.Tip(role);
            expenseSprite.sprite = GameResources.s_upgradeColor[(int)card.Color];
            if (card.UpgradeTwice < 100)
                upgradeText.text = card.Upgrade.ToString();
            else
                upgradeText.text = "MAX";
        }
    }

    public void ShowLibraryCard(Role role, Card card)
    {
        Role self = role;
        transform.name = card.Name.ToString();
        front.sprite = GameResources.s_cardFront[(int)card.Color];
        name.text = card.CName;
        tip.text = card.Tip(self);
        expenseSprite.sprite = GameResources.s_expenseColor[(int)card.Color];
        expenseText.text = card.Cost.ToString();
        upgradeSprite.sprite = GameResources.s_upgradeColor[(int)card.Color];
        upgradeText.text = card.Upgrade < 100 ? card.Upgrade.ToString() : "MAX";
    }

    void OnMouseEnter()
    {

        View.Instance.showCard.SetActive(true);
        View.Instance.showCard.GetComponent<ViewCardFront>().CopyContent(transform.gameObject.GetComponent<ViewCardFront>());

    }

    void OnMouseExit()
    {
        View.Instance.showCard.SetActive(false);
    }

    void OnMouseUp()
    {
        if (View.Instance.canPutCard && !hasPut)
        {
            int index = transform.GetSiblingIndex();
            if (index < Player.Instance.CardManager.Cards.Count)
                Player.Instance.PutSelectCard(View.Instance.enemy, index);
        }
        View.Instance.showCard.SetActive(false);

    }

    public IEnumerator IEPut()
    {
        float dur = 0.0f;
        float mTime = 0.3f;

        Vector3 bigSize = new Vector3(1.2f, 1.2f, 1);
        Vector3 normalSize = new Vector3(1, 1, 1);

        Vector3 beginPos = transform.position;
        Vector3 endPos = new Vector3(0, 0, -0.4f) * transform.GetSiblingIndex();

        Vector3 interval = new Vector3(0.3f, 0, -0.4f);
        Vector3 startPosition = new Vector3(-4f, 0f, 0);

        hasPut = true;

        while (dur <= mTime + 0.2f)
        {
            endPos = new Vector3(0, 0, -0.4f) * transform.GetSiblingIndex();
            dur += Time.deltaTime;
            transform.position = Vector3.Lerp(beginPos, endPos, dur / mTime);
            yield return 0;
        }
        dur = 0;

        while (dur <= mTime)
        {
            transform.localScale = Vector3.Lerp(normalSize, bigSize, dur / mTime);
            dur += Time.deltaTime;
            yield return 0;
        }
        dur = 0;
        while (dur <= mTime)
        {
            transform.localScale = Vector3.Lerp(bigSize, normalSize, dur / mTime);
            dur += Time.deltaTime;
            yield return 0;
        }
        dur = 0;
        while (dur <= mTime + 0.2f)
        {
            dur += Time.deltaTime;
            transform.position = Vector3.Lerp(endPos, startPosition + interval * (transform.GetSiblingIndex() - 1), dur / mTime);
            yield return null;
        }
    }

    public void CopyContent(ViewCardFront copy)
    {
        front.sprite = copy.front.sprite;
        transform.name = copy.transform.name;
        name.text = copy.name.text;
        tip.text = copy.tip.text;
        expenseSprite.sprite = copy.expenseSprite.sprite;
        expenseText.text = copy.expenseText.text;
        upgradeSprite.sprite = copy.upgradeSprite.sprite;
    }

}