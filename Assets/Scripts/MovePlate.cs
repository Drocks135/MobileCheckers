using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    //Reference for the object that instaniated the object
    GameObject reference = null;

    //Board Posistions
    int matrixX;
    int matrixY;

    // false: movement, true: attacking
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            //Change to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (attack)
        {
            int enemyX = (matrixX + reference.GetComponent<CheckerPiece>().GetXboard()) / 2;
            int enemyY = (matrixY + reference.GetComponent<CheckerPiece>().GetYboard()) / 2;
            GameObject cp = controller.GetComponent<Game>().GetPosistion(enemyX, enemyY);
            controller.GetComponent<Game>().SetPosistionEmpty(enemyX,
            enemyY);
            bool check = reference.GetComponent<CheckerPiece>().isBlack();
            if (check) {
                controller.GetComponent<Game>().setPieceNull(enemyX, enemyY, "White");
            } else {
                controller.GetComponent<Game>().setPieceNull(enemyX, enemyY, "Black");
            }
            Destroy(cp);
        }

        //Set the original position to empty
        controller.GetComponent<Game>().SetPosistionEmpty(reference.GetComponent<CheckerPiece>().GetXboard(),
            reference.GetComponent<CheckerPiece>().GetYboard() );

        //Move the reference to this position
        reference.GetComponent<CheckerPiece>().SetXboard(matrixX);
        reference.GetComponent<CheckerPiece>().SetYboard(matrixY);
        reference.GetComponent<CheckerPiece>().SetCoords();

        //king logic
        bool checkColor = reference.GetComponent<CheckerPiece>().isBlack();

        if (checkColor && matrixY == 0)
        {
            reference.GetComponent<CheckerPiece>().setName("doubleBlack");
            reference.GetComponent<CheckerPiece>().Activate();
        }
        else if (checkColor == false && matrixY == 7)
        {
            reference.GetComponent<CheckerPiece>().setName("doubleWhite");
            reference.GetComponent<CheckerPiece>().Activate();
        }

        //Update the matrix
        controller.GetComponent<Game>().SetPosition(reference);

        reference.GetComponent<CheckerPiece>().DestroyMovePlates();
        
        controller.GetComponent<Game>().NextTurn(reference, attack);
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetRefrence(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
