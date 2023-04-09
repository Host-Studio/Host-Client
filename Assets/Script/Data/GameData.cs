using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]  // 직렬화된 Data
public class GameData
{
    public int date = 0;
    public int gold = 0;

    public void UpdateDate()
    {
        date++;
    }

    public void UpdateGold(int _gold)
    {
        gold += _gold;
    }
}
