using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BuffManager
{
    Role self;

    [SerializeField]
    List<Buff> buffs;
    public List<Buff> Buffs
    {
        get { return buffs; }
        set { buffs = value; }
    }

    public BuffManager(Role role)
    {
        this.self = role;
        buffs = new List<Buff>();
    }

    public int CheckDebuffCount()
    {
        int num = 0;
        foreach (Buff buff in buffs)
        {
            if (IsDebuff(buff))
            {
                num++;
            }
        }
        return num;
    }

    public bool IsDebuff(Buff buff)
    {
        if (buff.ToString() == "CorrodeBuff" || buff.ToString() == "WearyBuff" || buff.ToString() == "FragileBuff" ||
              buff.ToString() == "DullAtmosphereBuff" || buff.ToString() == "DizzyBuff")
        {
            return true;
        }
        return false;
    }

    public bool CheckBuff(BuffType buffType)
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


    public int CheckLayer(string name)    //用于显示的，所以name为inciteBuff等
    {
        foreach (Buff buff in buffs)
        {
            if (buff.ToString() == name)
            {
                return buff.Layer;
            }
        }
        return 0;
    }

    public string CheckTip(string name)
    {
        foreach (Buff buff in buffs)
        {
            if (buff.ToString() == name)
            {
                return buff.tip;
            }
        }
        return "";
    }

    public bool IsBuff(string name)
    {
        foreach (Buff buff in buffs)
        {
            if (buff.name == name)
            {
                return true;
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
        int tmp = buffs.Count;
        for (int i = tmp - 1; i >= 0; i--)
        {
            buffs[i].Layer = 0;
            if (buffs[i].Active == false)
            {
                buffs.Remove(buffs[i]);
            }
        }
        View.Instance.ShowBuff(self);
    }

    public void ResetDeBuff()
    {
        int tmp = buffs.Count;
        for (int i = tmp - 1; i >= 0; i--)
        {
            if (IsDebuff(buffs[i]))
            {
                buffs[i].Layer = 0;
                if (buffs[i].Active == false)
                {
                    buffs.Remove(buffs[i]);
                }
            }
        }
        View.Instance.ShowBuff(self);
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
                View.Instance.ShowBuff(self);
            }
        }
    }

    public void BuffAddLayer(BuffName buffName, int layer = 1)
    {
        foreach (Buff buff in buffs)
        {
            if (buff.ToString() == buffName.ToString())
            {
                buff.Layer += layer;
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



    public void AddBuff(BuffName buffName, int layer)
    {
        Type type = Type.GetType(buffName.ToString());
        object obj = Activator.CreateInstance(type, true);
        if (obj == null)
        {
            Debug.Log("No Card for" + buffName.ToString());
            return;
        }
        AddBuff((Buff)obj);
        BuffAddLayer(buffName, layer);
        View.Instance.ShowBuff(self);

    }


}
