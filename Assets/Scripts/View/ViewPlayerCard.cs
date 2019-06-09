using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPlayerCard : MonoBehaviour
{
    public float mTime;
    private Vector3 endPos;
     
    void Start () 
    {
        mTime = 1f;
        endPos =new Vector3(2f,1,0);
       
    }

     
        //开始前转
    public void StartShow()
    {
        StartCoroutine(ToShow());  
    }
     

     

    IEnumerator ToShow()
    {
        Vector3 interval = new Vector3(0.3f, 0, -0.01f);
        Vector3 startPosition = new Vector3(-4f, 0f, 0);
        GameObject cardTombs = GameObject.Find("CardTombs");
        int x = cardTombs.transform.childCount; 
        var dur = 0.0f; 
        float time =1f;
        Vector3 beginPos = transform.position;
        while (dur <= time)
        { 
            dur += Time.deltaTime; 
            transform.position = Vector3.Lerp(beginPos, endPos, dur / time); 
            yield return 0; 
        }
        dur = 0;
        while (dur <= time)
        { 
            dur += Time.deltaTime; 
            yield return 0; 
        }
        dur = 0.0f;
        while (dur <= time)
        { 
            dur += Time.deltaTime; 
            transform.position = Vector3.Lerp(endPos, startPosition + interval*(x-1), dur / time); 
            yield return null; 
        }
        
        //Destroy(transform.gameObject);
    }
}
