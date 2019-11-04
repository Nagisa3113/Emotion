using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy1 : Enemy
{
    public GreenEnemy1() : base(EnemyType.Normal, EnemyColor.Green)
    {

    }

    public override void InitLibrary()
    {
        Card.AddCard(cardLibrary, CardName.Heal, 2);
        Card.AddCard(cardLibrary, CardName.Blow, 8);
    }
}

public class GreenEnemy2 : Enemy
{
    public GreenEnemy2() : base(EnemyType.Normal, EnemyColor.Green)
    {

    }

    public override void InitLibrary()
    {
        Card.AddCard(cardLibrary, CardName.Weaken, 3);
        Card.AddCard(cardLibrary, CardName.Blow, 7);
    }
}

public class Weaken : Card
{
    public Weaken() : base(CardName.Weaken, CardColor.Green, 1)
    {
        this.cName = "弱化";
        this.tip = "使敌人获得一层脆弱一层腐蚀";

    }

    public override void TakeEffect(Role self, Role target)
    {
        //使敌人获得3回合脆弱效果
        target.GetBuffManager.AddBuff(BuffName.FragileBuff, 1);
        target.GetBuffManager.AddBuff(BuffName.CorrodeBuff, 1);

    }
}

public class GreenEnemy3 : Enemy
{
    public GreenEnemy3() : base(EnemyType.Normal, EnemyColor.Green)
    {

    }

    public override void InitLibrary()
    {
        Card.AddCard(cardLibrary, CardName.Stuns, 2);
        Card.AddCard(cardLibrary, CardName.Thump, 8);

    }
}

public class Stuns : Card
{
    public Stuns() : base(CardName.Stuns, CardColor.Green, 1)
    {
        this.cName = "击晕";
        this.tip = "使敌人获得一层眩晕。";

    }

    public override void TakeEffect(Role self, Role target)
    {
        //使敌人获得3回合脆弱效果
        target.GetBuffManager.AddBuff(BuffName.DizzyBuff, 1);


    }
}
