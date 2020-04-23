using System.Collections;
using System.Collections.Generic;

public class OnlinePlayer : CommonPlayer, Player
{
    public OnlinePlayer(LudoType playerType)
    {
        playerBoardType = playerType;
        // Player Original type is retrived from server connection
    }

    protected override PlayerType playerType { get { return PlayerType.OnlinePlayer; } }

    public void EndPlayersTurn()
    {
        throw new System.NotImplementedException();
    }

    public void SetPlayersTurn()
    {
        throw new System.NotImplementedException();
    }
}
