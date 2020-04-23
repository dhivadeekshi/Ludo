using System.Collections;
using System.Collections.Generic;
using System;

public interface Player
{
    void SetPlayersTurn();
    void EndPlayersTurn();
    bool IsPlayerWon();
    void SetListeners(Action<int> onDiceRolled);
    void ClearListeners();
    bool HasPossibleMove(int diceRoll);
    bool HasMoreThanOnePossibleMove();
}
