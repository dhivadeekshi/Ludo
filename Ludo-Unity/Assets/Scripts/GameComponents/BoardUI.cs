using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardUI : MonoBehaviour
{
    #region USER INTERFACE
    public void InitBoard(LudoType playerType)
    {
        RotateBoard(playerType);
        UpdatePlayers(playerType);
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

    private void RotateBoard(LudoType boardType)
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
    }

    private void UpdatePlayers(LudoType startPlayerType)
    {
        LudoType playerType = startPlayerType;
        boardPlayers[0].Init(playerType, "You");
        for (int i = 1; i < boardPlayers.Length;i++)
        {
            playerType++;
            if (playerType == LudoType.END)
                playerType = 0;
            boardPlayers[i].Init(playerType, "Player " + (i + 1));
        }
    }
    #endregion
}
