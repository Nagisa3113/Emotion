using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Player player ;
    Enemy enemy ;

    void Start()
    {
        enemy =  GameObject.Find("Battle").GetComponent<BattleSystem>().GetEnemy();
        player = Player.GetInstance();
    }
    public void AI(Role self, Role target)
    {
        if (enemy.GetCardManager.GetCards().Count > 0)
        {
            int rankLeast = -10;
            Card temp = new Card();

            foreach (Card card in enemy.GetCardManager.GetCards())
            {
                if (card.GetRank > rankLeast && enemy.GetCardManager.ExpenseCurrent >= card.GetCost)
                {
                rankLeast=card.GetRank;
                temp = card; 
                }

            }

            if (temp.GetName != CardName.Empty )
            {

                EffectProcess.TakeEffect(temp, self, target);

                enemy.GetCardManager.GetCards().Remove(temp);
            }
        }
    
    }
}
