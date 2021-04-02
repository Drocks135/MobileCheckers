using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerPiece : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;

    //positions
    private int xBoard = -1;
    private int yBoard = -1;

    //Black or White player
    private string player;

    //Refrences for all sprite
    public Sprite singleBlack, singleWhite, doubleBlack, doubleWhite;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch (this.name)
        {
            case "singleBlack": this.GetComponent<SpriteRenderer>().sprite = singleBlack; break;
            case "singleWhite": this.GetComponent<SpriteRenderer>().sprite = singleWhite; break;
            case "doubleBlack": this.GetComponent<SpriteRenderer>().sprite = doubleBlack; break;
            case "doubleWhite": this.GetComponent<SpriteRenderer>().sprite = doubleWhite; break;
        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXboard()
    {
        return xBoard;
    }

    public int GetYboard()
    {
        return yBoard;
    }

    public void setXboard(int x)
    {
        xBoard = x;
    }

    public void setYboard(int y)
    {
        yBoard = y;
    }
}
