using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject Checker;

    
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    private string currentPlayer = "White";

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
            for(int j = 0; j < 2; j++)
        {
            playerWhite[i] = Create("singleWhite", i, j);
        }

        for (int i = 0; i < 8; i++)
            for (int j = 7; j > 5; j--)
            {
                playerWhite[i] = Create("singleBlack", i, j);
            }

        for (int i = 0; i < playerWhite.Length; i++)
        {
            SetPosition(playerWhite[i]);
            SetPosition(playerBlack[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(Checker, new Vector3(0, 0, -1), Quaternion.identity);
        CheckerPiece cm = obj.GetComponent<CheckerPiece>();
        cm.name = name;
        cm.setXboard(x);
        cm.setYboard(y);
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

    void SetupBoard()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
