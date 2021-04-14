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

            Destroy(cp);
        }

        //Set the original position to empty
        controller.GetComponent<Game>().SetPosistionEmpty(reference.GetComponent<CheckerPiece>().GetXboard(),
            reference.GetComponent<CheckerPiece>().GetYboard() );

        //Move the reference ot this position
        reference.GetComponent<CheckerPiece>().SetXboard(matrixX);
        reference.GetComponent<CheckerPiece>().SetYboard(matrixY);
        reference.GetComponent<CheckerPiece>().SetCoords();

        //Update the matrix
        controller.GetComponent<Game>().SetPosition(reference);

        reference.GetComponent<CheckerPiece>().DestroyMovePlates();
        bool check1 = reference.GetComponent<CheckerPiece>().hasJump();
        if (check1 && attack)
        {
            reference.GetComponent<CheckerPiece>().DestroyMovePlates();
            reference.GetComponent<CheckerPiece>().InitiateAttackPlates();
        }
        else
        {
            controller.GetComponent<Game>().NextTurn();
        }
        bool checkColor = reference.GetComponent<CheckerPiece>().isBlack();
        
        if (checkColor && matrixY == 0)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosistion(matrixX, matrixY);
            Destroy(cp);
            controller.GetComponent<Game>().Create("doubleBlack", matrixX, matrixY);
        }
        else if (checkColor == false && matrixY == 7)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosistion(matrixX, matrixY);
            Destroy(cp);
            controller.GetComponent<Game>().Create("doubleWhite", matrixX, matrixY);
        }
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
