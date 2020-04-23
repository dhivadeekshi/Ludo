
using System.Collections.Generic;

public class Board
{
    #region USER INTERFACE
    public Board(LudoType player1Type, LudoType player2Type)
    {
        CreatePlayer(player1Type);
        CreatePlayer(player2Type);
    }

    public Board(LudoType player1Type, LudoType player2Type, LudoType player3Type)
    {
        CreatePlayer(player1Type);
        CreatePlayer(player2Type);
        CreatePlayer(player3Type);
    }

    public Board()
    {
        for(LudoType i = 0; i < LudoType.END; i++)
            CreatePlayer(i);
    }

    public void NextPlayersTurn()
    {
        currentPlayer++;
        if (currentPlayer == TotalPlayers)
            currentPlayer = 0;
    }
    #endregion

    #region INTERNALS
    private List<BoardPlayerInfo> boardPlayers = new List<BoardPlayerInfo>();
    private TileManager tileManager = null;
    private int currentPlayer = 0;

    private int TotalPlayers { get { return boardPlayers.Count; } }
   
    private struct BoardPlayerInfo
    {
        public BoardPlayer player;
        public LudoType playerType;
        public int startingTile;

        public BoardPlayerInfo(BoardPlayer player, LudoType playerType, int startingTile)
        {
            this.player = player;
            this.playerType = playerType;
            this.startingTile = startingTile;
        }
    }

    private void CreatePlayer(LudoType playerType)
    {
        BoardPlayer player = new BoardPlayer(playerType, tileManager);
        boardPlayers.Add(new BoardPlayerInfo(player, playerType, 0)); // TODO update starting tile use tile manager
    }

    private bool CanPlayerMove(BoardPlayer player, int dice)
    {
        return player.HasPossibleMove(dice);
    }

    private bool CanCurrentPlayerMove(int dice)
    {
        return CanPlayerMove(boardPlayers[currentPlayer].player, dice);
    }

    private void MakePlayerMove(BoardPlayer player, int dice)
    {

    }
    #endregion
}
