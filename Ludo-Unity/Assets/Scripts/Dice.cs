using System.Collections;
using System.Collections.Generic;

public class Dice
{
    public static int RollDice()
    {
        return new System.Random().Next(1, 7);
    }
}
