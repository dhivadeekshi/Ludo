using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardUI : MonoBehaviour
{
    #region USER INTERFACE
    public void InitTwoPlayerBoard(LudoType bottomPlayerType, string player1Name, string player2Name)
    {
        RotateBoard(bottomPlayerType);
        UpdatePlayerName(0, player1Name);
        DisablePlayer(1);
        UpdatePlayerName(2, player2Name);
        DisablePlayer(3);
    }

    public void RotateBoard(LudoType boardType)
    {
        InternalRotateBoard(boardType);
    }

    public BoardPlayerUI GetPlayer(LudoType playerType)
    {
        foreach (var player in boardPlayers)
        {
            if (player.playerType == playerType)
                return player;
        }
        return null;
    }

    #endregion

    #region INTERNALS
    [SerializeField]
    private GameObject board = null;
    [SerializeField]
    private BoardPlayerUI[] boardPlayers = null;

    private LudoType boardType = LudoType.Blue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InternalRotateBoard(LudoType boardType)
    {
        this.boardType = boardType;
        Debug.Log("Rotate Board : " + boardType);
        switch (boardType)
        {
            case LudoType.Blue:
                board.transform.SetPositionAndRotation(board.transform.position, Quaternion.AngleAxis(0, new Vector3(0, 0, 1)));
                break;
            case LudoType.Yellow:
                board.transform.SetPositionAndRotation(board.transform.position, Quaternion.AngleAxis(90, new Vector3(0, 0, 1)));
                break;
            case LudoType.Red:
                board.transform.SetPositionAndRotation(board.transform.position, Quaternion.AngleAxis(180, new Vector3(0, 0, 1)));
                break;
            case LudoType.Green:
                board.transform.SetPositionAndRotation(board.transform.position, Quaternion.AngleAxis(270, new Vector3(0, 0, 1)));
                break;
        }
        UpdatePlayers(boardType);
    }

    private void UpdatePlayers(LudoType bottomLeftPlayer)
    {
        LudoType playerType = bottomLeftPlayer;
        for (int i = 0; i < boardPlayers.Length;i++)
        {
            boardPlayers[i].Init(playerType);
            playerType++;
            if (playerType == LudoType.END)
                playerType = 0;
        }
    }

    private void UpdatePlayerName(int index, string playerName)
    {
        boardPlayers[index].SetPlayerName(playerName);
    }

    private void DisablePlayer(int index)
    {
        boardPlayers[index].gameObject.SetActive(false);
    }

    private void SetTurn(BoardPlayerUI player)
    {
        if(player != null && player.enabled)
        {
            player.DisplayDice();
        }
    }

    private void TurnEnded(BoardPlayerUI player)
    {
        if (player != null && player.enabled)
        {
            player.HideDice();
        }
    }
    #endregion
}
