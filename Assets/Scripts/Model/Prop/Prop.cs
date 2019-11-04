using UnityEngine;
using System.Collections;

public enum PropType
{
    DumbBell,
    Book,
}


[System.Serializable]
public class Prop
{
    [SerializeField]
    PropType propType;

    public readonly int Cost;

    public Prop(PropType propType, int cost)
    {
        this.propType = propType;
        this.Cost = cost;

    }

    public virtual void Equip(Role role)
    {

    }

    public virtual void UnEquip(Role role)
    {

    }
}

public class Prop_DumbBell : Prop
{
    public Prop_DumbBell() : base(PropType.DumbBell, 30)
    {

    }

    public override void Equip(Role role)
    {
        role.HPMax += 100;
    }

    public override void UnEquip(Role role)
    {
        role.HPMax -= 100;
    }
}


public class Prop_Book : Prop
{
    public Prop_Book() : base(PropType.Book, 30)
    {

    }

    public override void Equip(Role role)
    {
        role.CardManager.numMax += 1;
    }

    public override void UnEquip(Role role)
    {
        role.CardManager.numMax -= 1;
    }
}

