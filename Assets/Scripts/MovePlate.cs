using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

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
            GameObject cp = controller.GetComponent<Game>().GetPosistion(matrixX, matrixY);

            Destroy(cp);
        }

        controller.GetComponent<Game>().SetPosistionEmpty(reference.GetComponent<CheckerPiece>().GetXboard(),
            reference.GetComponent<CheckerPiece>().GetYboard() );


        reference.GetComponent<CheckerPiece>().setXboard(matrixX);
        reference.GetComponent<CheckerPiece>().setXboard(matrixY);
        reference.GetComponent<CheckerPiece>().SetCoords();

        controller.GetComponent<Game>().SetPosition(reference);

        //reference.GetComponent<Checkerman>.DestroyMovePlates();

    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void setRefrence(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
