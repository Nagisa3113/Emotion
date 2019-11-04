using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ViewCardBack : MonoBehaviour
{
    GameObject frontCard;

    public void StartFront(int index)
    {
        frontCard = Instantiate(GameResources.cardFront, transform.position, Quaternion.identity);
        frontCard.GetComponent<ViewCardFront>().ShowCard(View.Instance.enemy, View.Instance.enemy.CardManager.Cards[index]);

        transform.eulerAngles = Vector3.zero;
        frontCard.transform.eulerAngles = new Vector3(0, 90, 0);

        StartCoroutine(ToFront());
        frontCard.transform.SetParent(View.Instance.cardTombs.transform);
    }


    IEnumerator ToFront()
    {
        float dur = 0.0f;
        float mTime = 0.3f;
        Vector3 beginPos = transform.position;
        Vector3 endPos = new Vector3(0f, 0, 0);
        Vector3 bigSize = new Vector3(1.2f, 1.2f, 1);
        Vector3 normalSize = new Vector3(1, 1, 1);
        Vector3 interval = new Vector3(0.3f, 0, -0.4f);
        Vector3 startPosition = new Vector3(-4f, 0f, 0);

        int x = View.Instance.cardTombs.transform.childCount;

        transform.DORotate(new Vector3(0, 90, 0), mTime);
        for (float i = mTime; i >= 0; i -= Time.deltaTime)
        {
            dur += Time.deltaTime;
            transform.position = Vector3.Lerp(beginPos, endPos, dur / mTime);
            frontCard.transform.position = transform.position;
            yield return 0;
        }

        frontCard.transform.DORotate(new Vector3(0, 0, 0), mTime);
        yield return new WaitForSeconds(mTime);

        dur = 0;
        while (dur <= mTime)
        {
            frontCard.transform.localScale = Vector3.Lerp(normalSize, bigSize, dur / mTime);
            dur += Time.deltaTime;
            yield return 0;
        }

        dur = 0;
        while (dur <= mTime)
        {
            frontCard.transform.localScale = Vector3.Lerp(bigSize, normalSize, dur / mTime);
            dur += Time.deltaTime;
            yield return 0;
        }

        dur = 0.0f;
        while (dur <= mTime)
        {
            dur += Time.deltaTime;
            frontCard.transform.position = new Vector3(
                Vector3.Lerp(endPos, startPosition + interval * (x - 1), dur / mTime).x,
                Vector3.Lerp(endPos, startPosition + interval * (x - 1), dur / mTime).y,
                (startPosition + interval * (x - 1)).z);
            yield return null;
        }

        Destroy(transform.gameObject);
    }

}
