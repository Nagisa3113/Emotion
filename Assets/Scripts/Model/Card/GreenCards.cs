using System;
using UnityEngine;

public class Heal : Card
{
    public Heal() : base(CardName.Heal, CardColor.Green, 5, 0)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        self.GetHP += 40 + 2 * self.GetCardManager.GetBonus(this.name);
        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            self.GetHP += 2 * self.GetCardManager.GetBonus(this.name);
        }
    }


}

public class Comfort : Card
{
    public Comfort() : base(CardName.AngerFire, CardColor.Green, 5, 0)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        self.GetBuffManager.AddBuff(CardName.Comfort);
    }


}
