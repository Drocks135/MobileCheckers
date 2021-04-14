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

    public void NextTurn()
    {
        if (currentPlayer.Equals("Black"))
        {
            currentPlayer = "White";
            hasJump = playerHasJump();
        }
        else
        {
            currentPlayer = "Black";
            hasJump = playerHasJump();
        }
    }

    public string GetPlayer()
    {
        return currentPlayer;
    }

    private bool playerHasJump()
    {
        if(currentPlayer == "Black")
        {
            for(int i = 0; i < playerBlack.Length; i++)
            {
                if (playerBlack[i].GetComponent<CheckerPiece>().hasJump())
                    return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
