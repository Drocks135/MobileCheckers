using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerPiece : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;

    //setting up fade functionality
    private bool fadeOut = true, fade = false;
    private float fadeSpeed = 0.8f;
    
    private bool isForced = false;

    //positions
    private int xBoard = -1;
    private int yBoard = -1;

    //Black or White player
    private string player;

    private bool hasJumped = false;

    //Refrences for all sprite
    public Sprite singleBlack, singleWhite, doubleBlack, doubleWhite;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch (this.name)
        {
            case "singleBlack": this.GetComponent<SpriteRenderer>().sprite = singleBlack; player = "Black"; break;
            case "singleWhite": this.GetComponent<SpriteRenderer>().sprite = singleWhite; player = "White"; break;
            case "doubleBlack": this.GetComponent<SpriteRenderer>().sprite = doubleBlack; player = "Black"; break;
            case "doubleWhite": this.GetComponent<SpriteRenderer>().sprite = doubleWhite; player = "White"; break;
        }
    }

    void Update()
    {
        if(fade){
            if(fadeOut){
                Color objectColor = this.GetComponent<Renderer>().material.color;
                float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                this.GetComponent<Renderer>().material.color = objectColor;
                if(objectColor.a <= 0){
                    fadeOut = false;
                }
            }else{
                Color objectColor = this.GetComponent<Renderer>().material.color;
                float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                this.GetComponent<Renderer>().material.color = objectColor;
                if(objectColor.a >= 1){
                    fadeOut = true;
                }
            }
        }else{
            Color objectColor = this.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
            if(objectColor.a < 1){
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                this.GetComponent<Renderer>().material.color = objectColor;
            }
        }
    }

    public void setName(string newName) {
        this.name = newName;
    }

    public void setForced(bool boolean, bool turn){
        isForced = boolean;
        if(this.hasJump() && boolean && turn)
            fade = true;
        else
            fade = false;
    }
    public bool isBlack()
    {
        if (player == "Black")
        {
            return true;
        }
        return false;
    }

    public void makeDouble()
    {

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

    public void SetXboard(int x)
    {
        xBoard = x;
    }

    public void SetYboard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        if (isForced == true){
            DestroyMovePlates();
            Game sc = controller.GetComponent<Game>();
            if (sc.GetPlayer() == player)
            {
                InitiateAttackPlates();
            }
        } else {
            DestroyMovePlates();
            Game sc = controller.GetComponent<Game>();
            if (sc.GetPlayer() == player)
            {
                InitiateMovePlates();
            }
        }
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        for(int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        if (CanMoveDownLeft())
        {
            MovePlateSpawn(xBoard - 1, yBoard + 1);
        }

        if (CanMoveDownRight())
        {
            MovePlateSpawn(xBoard + 1, yBoard + 1);

        }

        if (CanMoveUpLeft())
        {
            MovePlateSpawn(xBoard - 1, yBoard - 1);
        }

        if (CanMoveUpRight())
        {
            MovePlateSpawn(xBoard + 1, yBoard - 1);
        }

        if (CanJumpDownLeft())
        {
            AttackPlateSpawn(xBoard - 2, yBoard + 2);
        }

        if (CanJumpDownRight())
        {
            AttackPlateSpawn(xBoard + 2, yBoard + 2);
        }

        if (CanJumpUpLeft())
        {
            AttackPlateSpawn(xBoard - 2, yBoard - 2);
        }

        if (CanJumpUpRight())
        {
            AttackPlateSpawn(xBoard + 2, yBoard - 2);
        }
    }

    public void InitiateAttackPlates()
    {
        if (CanJumpDownLeft())
        {
            AttackPlateSpawn(xBoard - 2, yBoard + 2);
        }

        if (CanJumpDownRight())
        {
            AttackPlateSpawn(xBoard + 2, yBoard + 2);
        }

        if (CanJumpUpLeft())
        {
            AttackPlateSpawn(xBoard - 2, yBoard - 2);
        }

        if (CanJumpUpRight())
        {
            AttackPlateSpawn(xBoard + 2, yBoard - 2);
        }
    }


    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetRefrence(this.gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void AttackPlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetRefrence(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
    
    public bool hasJump()
    {
        if (CanJumpDownLeft()|| CanJumpDownRight() || CanJumpUpLeft() || CanJumpUpRight()){
            return true;
        }
        return false;
    }

    private bool CanMoveDownLeft()
    {
        Game sc = controller.GetComponent<Game>();

        if (this.name == "singleWhite" || this.name == "doubleBlack" || this.name == "doubleWhite")
        {
            if (sc.PosistionOnBoard(xBoard - 1, yBoard + 1) && sc.GetPosistion(xBoard - 1, yBoard + 1) == null)
            {
                return true;
            }
        }

        return false;
    }

    private bool CanMoveDownRight()
    {
        Game sc = controller.GetComponent<Game>();
        if (this.name == "singleWhite" || this.name == "doubleBlack" || this.name == "doubleWhite")
        {
            if (sc.PosistionOnBoard(xBoard + 1, yBoard + 1) && sc.GetPosistion(xBoard + 1, yBoard + 1) == null)
            {
                return true;
            }
        }
        return false;
    }

    private bool CanMoveUpLeft()
    {
        if (this.name == "singleBlack" || this.name == "doubleWhite" || this.name == "doubleBlack")
        {
            Game sc = controller.GetComponent<Game>();

            if (sc.PosistionOnBoard(xBoard - 1, yBoard - 1) && sc.GetPosistion(xBoard - 1, yBoard - 1) == null)
            {
                return true;
            }

        }

        return false;
    }

    private bool CanMoveUpRight()
    {
        if (this.name == "singleBlack" || this.name == "doubleWhite" || this.name == "doubleBlack")
        {
            Game sc = controller.GetComponent<Game>();

            if (sc.PosistionOnBoard(xBoard + 1, yBoard - 1) && sc.GetPosistion(xBoard + 1, yBoard - 1) == null)
            {
                return true;
            }
        }

        return false;
    }



    private bool CanJumpDownLeft()
    {
        Game sc = controller.GetComponent<Game>();

        if (this.name == "singleWhite" || this.name == "doubleBlack" || this.name == "doubleWhite")
        {
            if (yBoard == 7)
                return false;
            if (sc.PosistionOnBoard(xBoard - 1, yBoard + 1)
                        && sc.GetPosistion(xBoard - 1, yBoard + 1) != null
                        && sc.GetPosistion(xBoard - 1, yBoard + 1).GetComponent<CheckerPiece>().player != player)
            {
                if (sc.PosistionOnBoard(xBoard - 2, yBoard + 2) && sc.GetPosistion(xBoard - 2, yBoard + 2) == null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool CanJumpDownRight()
    {
        Game sc = controller.GetComponent<Game>();
        if (this.name == "singleWhite" || this.name == "doubleBlack" || this.name == "doubleWhite")
        {
            if (yBoard == 7)
                return false;
            if (sc.PosistionOnBoard(xBoard + 1, yBoard + 1)
            && sc.GetPosistion(xBoard + 1, yBoard + 1) != null
            && sc.GetPosistion(xBoard + 1, yBoard + 1).GetComponent<CheckerPiece>().player != player)
            {
                if (sc.PosistionOnBoard(xBoard + 2, yBoard + 2) && sc.GetPosistion(xBoard + 2, yBoard + 2) == null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool CanJumpUpLeft()
    {
        if (this.name == "singleBlack" || this.name == "doubleWhite" || this.name == "doubleBlack")
        {
            Game sc = controller.GetComponent<Game>();
            if (yBoard == 0)
                return false;
            if (sc.PosistionOnBoard(xBoard - 1, yBoard - 1)
                        && sc.GetPosistion(xBoard - 1, yBoard - 1) != null
                        && sc.GetPosistion(xBoard - 1, yBoard - 1).GetComponent<CheckerPiece>().player != player)
            {
                if (sc.PosistionOnBoard(xBoard - 2, yBoard - 2) && sc.GetPosistion(xBoard - 2, yBoard - 2) == null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool CanJumpUpRight()
    {
        if (this.name == "singleBlack" || this.name == "doubleWhite" || this.name == "doubleBlack")
        {
            Game sc = controller.GetComponent<Game>();
            if (yBoard == 0)
                return false;
            if (sc.PosistionOnBoard(xBoard + 1, yBoard - 1)
                && sc.GetPosistion(xBoard + 1, yBoard - 1) != null
                && sc.GetPosistion(xBoard + 1, yBoard - 1).GetComponent<CheckerPiece>().player != player)
            {
                if (sc.PosistionOnBoard(xBoard + 2, yBoard - 2) && sc.GetPosistion(xBoard + 2, yBoard - 2) == null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool CanMove(){
        if(CanJumpDownLeft()){
            return true;
        }
        if(CanJumpDownRight()){
            return true;
        }
        if(CanJumpUpLeft()){
            return true;
        }
        if(CanJumpUpRight()){
            return true;
        }
        if(CanMoveDownLeft()){
            return true;
        }
        if(CanMoveDownRight()){
            return true;
        }
        if(CanMoveUpLeft()){
            return true;
        }
        if(CanMoveUpRight()){
            return true;
        }
        return false;
    }
}
