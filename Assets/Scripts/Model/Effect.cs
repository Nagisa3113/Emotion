using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Damage,
    Heal,
}


public class Effect
{
    public EffectType effectType;

    public int value;

    public Effect()
    {

    }

    public Effect(EffectType effectType, int value)
    {
        this.effectType = effectType;
        this.value = value;
    }

    public virtual void Process(Role self, Role target)
    {

    }

}

public class DamageEffect:Effect
{
    public DamageEffect(EffectType effectType, int value): base(effectType, value)
    {

    }

    public override void Process(Role self, Role target)
    {
        target.GetHP -= base.value;
    }
}


public class HealEffect:Effect
{
    public HealEffect(EffectType effectType, int value) : base(effectType, value)
    {

    }

    public override void Process(Role self, Role target)
    {
        self.GetHP += value;
    }
}

