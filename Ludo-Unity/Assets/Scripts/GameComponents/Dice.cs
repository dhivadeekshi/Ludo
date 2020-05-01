using System;

public class Dice
{
    public static int RollDice()
    {
        const int tempMultiplier = 100;
        int tempMax = Constants.DiceRoll.max * tempMultiplier;
        int random = new Random().Next(Constants.DiceRoll.min, tempMax + 1);
        random = (int)Math.Ceiling((double)random / tempMultiplier);
        return random;
    }
}
