using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject Checker;

    
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[12];
    private GameObject[] playerWhite = new GameObject[12];

    private string currentPlayer = "Black";
    private bool hasJump = false;

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //Load in white pieces
        for (int i = 0; i < 8; i++)
        {
            if(i % 2 == 0)
            {
                playerWhite[i] = Create("singleWhite", i, 0);
                playerWhite[11 - i / 2] = Create("singleWhite", i, 2);
            } 
            else
            {
               playerWhite[i] = Create("singleWhite", i, 1);
            }
        }

        //Load in Black pieces
        for (int i = 0; i < 8; i++)
        {
            if (i % 2 == 0)
            {
                playerBlack[i] = Create("singleBlack", i, 6);
                
            }
            else
            {
                playerBlack[i] = Create("singleBlack", i, 7);
                playerBlack[11 - i / 2] = Create("singleBlack", i, 5);
            }
        }

        for (int i = 0; i < playerWhite.Length; i++)
        {
            SetPosition(playerWhite[i]);
            SetPosition(playerBlack[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        //Create a checker piece object
        GameObject obj = Instantiate(Checker, new Vector3(0, 0, -1), Quaternion.identity);
        CheckerPiece cm = obj.GetComponent<CheckerPiece>();

        //Set the attributes of the piece
        cm.name = name;
        cm.SetXboard(x);
        cm.SetYboard(y);
        cm.Activate();

        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        CheckerPiece cm = obj.GetComponent<CheckerPiece>();
        positions[cm.GetXboard(), cm.GetYboard()] = obj;
    }

    public void SetPosistionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosistion(int x, int y)
    {
        return positions[x, y];
    }

    public bool PosistionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public void NextTurn(GameObject obj, bool wasLastJump)
    {
        if (checkIfBlackWins() && currentPlayer.Equals("Black")){
            print("Congratulations, black team wins!");
        } else if (checkIfWhiteWins() && currentPlayer.Equals("White")) {
            print("Congratulations, white team wins!");
        } else {
            CheckerPiece cm = obj.GetComponent<CheckerPiece>();
            if (currentPlayer.Equals("Black"))
            {
                hasJump = playerHasJump(obj);
                if (hasJump == true && wasLastJump == true) {
                    currentPlayer = "Black";
                    cm.InitiateAttackPlates();
                    for (int i = 0; i < 12; i++){
                        if (playerBlack[i] != null) {
                            CheckerPiece temp = playerBlack[i].GetComponent<CheckerPiece>();
                            temp.setForced(true);
                        }
                    }
                } else {
                    currentPlayer = "White";
                    bool canJump = false;
                    for (int i = 0; i < 12; i++){
                        if (playerWhite[i] != null) {
                            CheckerPiece temp = playerWhite[i].GetComponent<CheckerPiece>();
                            if(temp.hasJump()){
                                canJump = true;
                                for (int j = 0; j < 12; j++){
                                    if (playerWhite[j] != null) {
                                        CheckerPiece temp2 = playerWhite[j].GetComponent<CheckerPiece>();
                                        temp2.setForced(true);
                                    }
                                }
                            break;
                            }
                        }
                    }
                    if (canJump == false){
                        for (int i = 0; i < 12; i++){
                            if (playerWhite[i] != null) {
                                CheckerPiece temp = playerWhite[i].GetComponent<CheckerPiece>();
                                temp.setForced(false);
                            }
                        }
                    }
                }
            }
            else
            {
                hasJump = playerHasJump(obj);
                if (hasJump == true && wasLastJump == true) {
                    currentPlayer = "White";
                    cm.InitiateAttackPlates();
                    for (int i = 0; i < 12; i++){
                        if (playerWhite[i] != null) {
                            CheckerPiece temp = playerWhite[i].GetComponent<CheckerPiece>();
                            temp.setForced(true);
                        }
                    }
                } else {
                    currentPlayer = "Black";
                    bool canJump = false;
                    for (int i = 0; i < 12; i++){
                        if (playerBlack[i] != null) {
                            CheckerPiece temp = playerBlack[i].GetComponent<CheckerPiece>();
                            if(temp.hasJump()){
                                canJump = true;
                                for (int j = 0; j < 12; j++){
                                    if (playerBlack[j] != null) {
                                        CheckerPiece temp2 = playerBlack[j].GetComponent<CheckerPiece>();
                                        temp2.setForced(true);
                                    }
                                }
                            break;
                            }
                        }
                    }
                    if (canJump == false){
                        for (int i = 0; i < 12; i++){
                            if (playerBlack[i] != null) {
                                CheckerPiece temp = playerBlack[i].GetComponent<CheckerPiece>();
                                temp.setForced(false);
                            }
                        }
                    }
                }
            }
        }
    }

    public string GetPlayer()
    {
        return currentPlayer;
    }

    private bool playerHasJump(GameObject obj)
    {
        CheckerPiece cm = obj.GetComponent<CheckerPiece>();
        return cm.hasJump();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool checkIfBlackWins(){
        for (int i = 0; i < 12; i++){
            if (playerWhite[i] != null) {
                CheckerPiece temp = playerWhite[i].GetComponent<CheckerPiece>();
                if (temp.CanMove()){
                    
                    return false;
                }
            }
        }
        return true;
    }

    public void setPieceNull(int x, int y, string color) {
        if (color.Equals("Black")) {
            for (int i = 0; i < 12; i++){
                if (playerBlack[i] != null) {
                    CheckerPiece temp = playerBlack[i].GetComponent<CheckerPiece>();
                    if (temp.GetXboard() == x && temp.GetYboard() == y){
                        playerBlack[i] = null;
                    }
                }
            }
        } else {
            for (int i = 0; i < 12; i++){
                if (playerWhite[i] != null) {
                    CheckerPiece temp = playerWhite[i].GetComponent<CheckerPiece>();
                    if (temp.GetXboard() == x && temp.GetYboard() == y){
                        playerWhite[i] = null;
                    }
                }
            }
        }
    }

    public bool checkIfWhiteWins(){
        for (int i = 0; i < 12; i++){
            if (playerBlack[i] != null) {
                CheckerPiece temp = playerBlack[i].GetComponent<CheckerPiece>();
                if (temp.CanMove()){
                    
                    return false;
                }
            }
        }
        return true;
    }
}
