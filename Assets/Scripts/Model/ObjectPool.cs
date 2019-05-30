using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ObjectPool: MonoBehaviour
{
    
    public const string motherHandCard = "motherHandCard";
    public const string motherPutCard  = "motherPutCard";
    public GameObject putCard;
    public GameObject handCard;
    private Dictionary<string, List<GameObject>> pool;

    private static ObjectPool objectPool;

    private ObjectPool()
    {
        objectPool =this;
        pool = new Dictionary<string, List<GameObject>>();
    }
    public static ObjectPool GetInstance()
    {
        if (objectPool == null)
        {
            objectPool = new ObjectPool();
        }
        return objectPool;
    }
  
 

    public GameObject GetObj(string objName,Vector3 position,Quaternion quaternion)
    {
        //结果对象
        GameObject result = null;
        //判断是否有该名字的对象池
        if (pool.ContainsKey(objName) && pool[objName].Count > 0)
        {
    
            //获取结果
            result = pool[objName][0];
            //激活对象
            result.transform.position = position;
            result.transform.rotation = quaternion;
            result.SetActive(true);
            pool[objName].Remove(result);
            //返回结果
        }
        else
        {
        if(objName == "motherHandCard")
           result = Object.Instantiate(handCard);
        else if(objName == "motherPutCard")
           result = Object.Instantiate(putCard);
        result.transform.position = position;
        result.transform.rotation = quaternion;    
    
        }
        
        return result;
    }
 

    public void RecycleObj(string objName,GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.SetActive(false);
        //判断是否有该对象的对象池
        if (pool.ContainsKey(objName))
        {
            //放置到该对象池
            pool[objName].Add(obj);
        }
        else
        {
            //创建该类型的池子，并将对象放入
            pool.Add(objName, new List<GameObject>() { obj });
        }
    }
 
}
