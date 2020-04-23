

public class Dice
{
    public static int RollDice()
    {
        return new System.Random().Next(Constants.DiceRoll.min, Constants.DiceRoll.max + 1);
    }
}
