using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class ViewEnemyCard : MonoBehaviour
{
    private GameObject front;
    public float mTime;

    private Vector3 endPos;

    public void Init() 
    {
        transform.eulerAngles = Vector3.zero;
        front.transform.eulerAngles = new Vector3(0, 90, 0);
    }
     
    void Start () 
    {
        mTime = 1f;
        endPos =new Vector3(4.9f,1,0);
       
    }

     
        //开始前转
    public void StartFront()
    {
        front = ObjectPool.GetInstance().GetObj("motherPutCard", transform.position, Quaternion.identity);
        front.transform.name = transform.name;
        front.transform.SetParent(View.GetInstance().cardTombs.transform);
        foreach (var prefab in View.GetInstance().handCards)
        {

            if (transform.name == prefab.name)
            {
                front.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                break;
            }
        }
        Init();
        StartCoroutine(ToFront());
        
    }
     

     

    IEnumerator ToFront()
    {
        Vector3 interval = new Vector3(0.3f, 0, -0.01f);
        Vector3 startPosition = new Vector3(-4f, 0f, 0);
        int x = View.GetInstance().cardTombs.transform.childCount; 
        var dur = 0.0f; 
        float time =1f;
        Vector3 beginPos = transform.position;
        while (dur <= time)
        { 
            dur += Time.deltaTime; 
            transform.position = Vector3.Lerp(beginPos, endPos, dur / time); 
            front.transform.position = Vector3.Lerp(beginPos, endPos, dur / time); 
            yield return 0; 
        }

        transform.DORotate(new Vector3(0, 90, 0), mTime);
        for (float i = mTime ; i >= 0; i-= Time.deltaTime)
        {
            yield return 0;
        }
        front.transform.DORotate(new Vector3(0, 0, 0), mTime);
        for (float i = mTime ; i >= 0; i-= Time.deltaTime)
        {
            yield return 0;
        }
        dur = 0.0f;
        while (dur <= time)
        { 
            dur += Time.deltaTime; 
            front.transform.position = Vector3.Lerp(endPos, startPosition + interval*(x-1), dur / time); 
            yield return null; 
        }

        Destroy(transform.gameObject);
    }



}
