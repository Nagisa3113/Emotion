﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardManager : CardManager
{
    public override void GetCardsFromLibrary(int num)
    {

        for (int i = 0; i < num; i++)
        {

            if (CardLibrary.GetInstance().GetNum == 0 || CardsNum == numMax)
            {
                break;
            }
            else
            {
                cards.Add(CardLibrary.GetInstance().GetRandomCard());
            }

        }
    }
}