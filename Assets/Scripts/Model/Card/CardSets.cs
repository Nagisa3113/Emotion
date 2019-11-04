using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CardSet
{
    public readonly CardColor Color;
    public readonly int Cost;
    public readonly List<Card> Cards;

    public CardSet(CardColor color, int cost, List<Card> cards)
    {
        this.Cost = cost;
        this.Cards = cards;
        this.Color = color;
    }
}

public class CardGood
{
    int cost;
    Card card;

    public CardGood(int cost, Card card)
    {
        this.cost = cost;
        this.card = card;

    }
}

public class CardSets
{
    public static CardSet[] redCardSet = new CardSet[]
    {
        new CardSet
        (
            CardColor.Red,50,
            new List<Card>()
            {
            Card.NewCard(CardName.NoNameFire),
            Card.NewCard(CardName.NoNameFire),
            Card.NewCard(CardName.NoNameFire),
            Card.NewCard(CardName.NoNameFire),
            Card.NewCard(CardName.ReasonVanish),
            Card.NewCard(CardName.Vent),
            }
        ),
        new CardSet
        (
            CardColor.Red,50,
            new List<Card>()
            {
            Card.NewCard(CardName.ReasonVanish),
            Card.NewCard(CardName.ReasonVanish),
            Card.NewCard(CardName.Revenge),
            Card.NewCard(CardName.Revenge),
            Card.NewCard(CardName.Incite),
            Card.NewCard(CardName.Incite),
            }
        ),
        new CardSet
        (
            CardColor.Red,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Enrange),
            Card.NewCard(CardName.Enrange),
            Card.NewCard(CardName.Enrange),
            Card.NewCard(CardName.Execute),
            Card.NewCard(CardName.Provoke),
            Card.NewCard(CardName.Provoke),
            }
        ),
        new CardSet
        (
            CardColor.Red,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Furious),
            Card.NewCard(CardName.Furious),
            Card.NewCard(CardName.Furious),
            Card.NewCard(CardName.RadicalAction),
            Card.NewCard(CardName.RadicalAction),
            Card.NewCard(CardName.RadicalAction),
            }
        )
    };

    public static CardSet[] grenCardSet = new CardSet[]
    {
        new CardSet
        (
            CardColor.Green,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Heal),
            Card.NewCard(CardName.Heal),
            Card.NewCard(CardName.Heal),
            Card.NewCard(CardName.Comfort),
            Card.NewCard(CardName.Comfort),
            Card.NewCard(CardName.Comfort),
            }
        ),
        new CardSet
        (
            CardColor.Green,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Plot),
            Card.NewCard(CardName.Plot),
            Card.NewCard(CardName.Encumber),
            Card.NewCard(CardName.Encumber),
            Card.NewCard(CardName.Sneer),
            Card.NewCard(CardName.Sneer),
            }
        ),
        new CardSet
        (
            CardColor.Green,50,
            new List<Card>()
            {
            Card.NewCard(CardName.OverHeated),
            Card.NewCard(CardName.OverHeated),
            Card.NewCard(CardName.RePastEvent),
            Card.NewCard(CardName.RePastEvent),
            Card.NewCard(CardName.Obstruct),
            Card.NewCard(CardName.Obstruct),
            }
        ),
        new CardSet
        (
            CardColor.Green,50,
            new List<Card>()
            {
            Card.NewCard(CardName.OverHeated),
            Card.NewCard(CardName.OverHeated),
            Card.NewCard(CardName.SelfControl),
            Card.NewCard(CardName.SelfControl),
            Card.NewCard(CardName.XingZaiLeHuo),
            Card.NewCard(CardName.XingZaiLeHuo),
            }
        )
    };

    public static CardSet[] grayCardSet = new CardSet[]
    {
        new CardSet
        (
            CardColor.Gray,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Iron),
            Card.NewCard(CardName.Iron),
            Card.NewCard(CardName.HoldOn),
            Card.NewCard(CardName.HoldOn),
            Card.NewCard(CardName.Increase),
            Card.NewCard(CardName.Increase),
            }
        ),
        new CardSet
        (
            CardColor.Gray,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Lesson),
            Card.NewCard(CardName.Lesson),
            Card.NewCard(CardName.Iron),
            Card.NewCard(CardName.Iron),
            Card.NewCard(CardName.FightBack),
            Card.NewCard(CardName.FightBack),
            }
        ),
        new CardSet
        (
            CardColor.Gray,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Accelerate),
            Card.NewCard(CardName.Accelerate),
            Card.NewCard(CardName.Accelerate),
            Card.NewCard(CardName.Reinforce),
            Card.NewCard(CardName.Reinforce),
            Card.NewCard(CardName.Reinforce),
            }
        ),
        new CardSet
        (
            CardColor.Gray,50,
            new List<Card>()
            {
            Card.NewCard(CardName.HoldOn),
            Card.NewCard(CardName.HoldOn),
            Card.NewCard(CardName.Accelerate),
            Card.NewCard(CardName.Accelerate),
            Card.NewCard(CardName.JianRenBuBa),
            Card.NewCard(CardName.JianRenBuBa),
            }
        )
    };

    public static CardSet[] purpleCardSet = new CardSet[]
    {
        new CardSet
        (
            CardColor.Purple,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Depress),
            Card.NewCard(CardName.Depress),
            Card.NewCard(CardName.Depress),
            Card.NewCard(CardName.Pacify),
            Card.NewCard(CardName.Pacify),
            Card.NewCard(CardName.Compare),
            }
        ),
        new CardSet
        (
            CardColor.Purple,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Complain),
            Card.NewCard(CardName.Complain),
            Card.NewCard(CardName.Complain),
            Card.NewCard(CardName.DullAtmosphere),
            Card.NewCard(CardName.DullAtmosphere),
            Card.NewCard(CardName.Confess),
            }
        ),
        new CardSet
        (
            CardColor.Purple,50,
            new List<Card>()
            {
            Card.NewCard(CardName.WeiYuChouMou),
            Card.NewCard(CardName.WeiYuChouMou),
            Card.NewCard(CardName.OuDuanSiLian),
            Card.NewCard(CardName.OuDuanSiLian),
            Card.NewCard(CardName.Confess),
            Card.NewCard(CardName.Compare),
            }
        ),
        new CardSet
        (
            CardColor.Purple,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Blues),
            Card.NewCard(CardName.Blues),
            Card.NewCard(CardName.Blues),
            Card.NewCard(CardName.Complain),
            Card.NewCard(CardName.Complain),
            Card.NewCard(CardName.Complain),
            }
        )
    };

    public static CardSet[] yellowCardSet = new CardSet[]
    {
        new CardSet
        (
            CardColor.Yellow,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Reconcile),
            Card.NewCard(CardName.Reconcile),
            }
        ),
        new CardSet
        (
            CardColor.Yellow,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Suppress),
            Card.NewCard(CardName.Suppress),

            }
        ),
        new CardSet
        (
            CardColor.Yellow,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Transfer),
            Card.NewCard(CardName.Transfer),
            }
        ),
        new CardSet
        (
            CardColor.Yellow,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Feed),
            Card.NewCard(CardName.Feed),
            }
        ),
        new CardSet
        (
            CardColor.Yellow,50,
            new List<Card>()
            {
            Card.NewCard(CardName.Trick),
            Card.NewCard(CardName.Trick),
            }
        )
    };


    public static CardSet[][] sets =
    { redCardSet,grenCardSet,grayCardSet,purpleCardSet,yellowCardSet };

    public void AddCardSet(Player player)
    {

    }

    public static CardSet GetRandomCardSet()
    {
        CardSet[] set = sets[Random.Range(0, sets.Length)];
        CardSet cardSet = set[Random.Range(0, set.Length)];
        return cardSet;
    }


}
