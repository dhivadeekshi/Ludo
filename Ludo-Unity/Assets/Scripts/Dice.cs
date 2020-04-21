using System.Collections;
using System.Collections.Generic;

public class Dice
{
    public int RollDice()
    {
        return new System.Random().Next(1, 7);
    }
}
