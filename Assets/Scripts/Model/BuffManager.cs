using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BuffManager
{
    [SerializeField]
    List<Buff> buffs;
    public List<Buff> Buffs
    {
        get
        {
            return buffs;
        }
        set
        {
            buffs = value;
        }
    }

    Role self;

    public View view;

    public BuffManager(Role role)
    {
        this.self = role;
        buffs = new List<Buff>();
        view = View.GetInstance();

    }

    public int CheckCount()
    {
        return buffs.Count;
    }
    public bool CheckBuff(BuffType buffType)    //严
    {
        foreach (Buff buff in buffs)
        {
            if (buff.buffType == buffType)
            {
                return true;
            }
        }
        return false;
    }


    
    public int CheckLayer (string name)    //用于显示的，所以name为inciteBuff等
    {
        foreach (Buff buff in buffs)
        {
            if (buff.ToString() == name)
            {
                return  buff.Layer;
            }
        }
        return 0;
    }

    public string CheckTip (string name)
    {
        foreach (Buff buff in buffs)
        {
            if (buff.ToString() == name)
            {
                return  buff.tip;
            }
        }
        return "";
    }

     public bool IsBuff (string name)    
    {
        foreach (Buff buff in buffs)
        {
            if (buff.name == name)
            {
                return  true;
            }
        }
        return false;
    }



    public void BuffProcess(BuffType buffType, Role self)
    {
        foreach (Buff buff in buffs)
        {
            if (buff.buffType == buffType)
            {
                buff.Process(self);
            }
        }
    }

    public void ResetBuff()
    {
        foreach (Buff buff in buffs)
        {
            buff.Layer = 0;
            buffs.Remove(buff);
        }
        view.ShowBuff(self);
    }


    public void BuffReduceLayer()
    {
        int tmp = buffs.Count;
        for (int i = tmp - 1; i >= 0; i--)
        {
            buffs[i].Layer--;
            if (buffs[i].Active == false)
            {
                buffs.Remove(buffs[i]);
                view.ShowBuff(self);

            }
        }
    }

    public void BuffAddLayer(BuffName buffName,int layer =1)
    {
        foreach (Buff buff in buffs)
        {
            if (buff.ToString() == buffName.ToString())
            {
                buff.Layer += layer ;
                Debug.Log(layer);
                return;
            }
        } 

    }


    //判断是否已经有重复Buff
    void AddBuff(Buff buff)
    {
        foreach (Buff b in buffs)
        {
            if (b.name.Equals(buff.name))
            {
                return;
            }
        }
        buffs.Add(buff);
    }

 

    public void AddBuff(BuffName buffName,int layer)   
    {
        Type type = Type.GetType(buffName.ToString());   
        object obj = Activator.CreateInstance(type, true) ;   
        if (obj == null)
        {            
            Debug.Log("No Card for" + buffName.ToString());
            return;
         }
        AddBuff((Buff)obj);
        BuffAddLayer(buffName,layer);
        view.ShowBuff(self);

    }


}
