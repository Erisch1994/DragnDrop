using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject correctShape;
    private bool movement;
    private bool finish;
    private bool isMatched;

    public delegate void MatchedAction();
    public event MatchedAction OnMatched;
    private float startPosX;
    private float startPosY;

    private Vector3 resetPosition;


    void Start()
    {
        resetPosition = this.transform.localPosition;
    }

 
    void Update()
    {
        if (finish == false)
        {
            if (movement)
            {

                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
            }

        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            movement = true;
        }
    }

    private void OnMouseUp()
    {
        movement = false;

        if (Mathf.Abs(this.transform.localPosition.x - correctShape.transform.localPosition.x) < 0.5f &&
            Mathf.Abs(this.transform.localPosition.y - correctShape.transform.localPosition.y) < 0.5f)
        {
            this.transform.position = new Vector3(correctShape.transform.position.x, correctShape.transform.position.y, correctShape.transform.position.z);
            finish = true;
            isMatched = true;
            if (OnMatched != null)
            {
                OnMatched();
            }
        }
        else
        {
            this.transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
            isMatched = false;
        }
    }

    public void ResetMatchedState()
    {
        isMatched = false;
    }

    public bool IsMatched
    {
        get { return isMatched; }
    }

    public void ResetObject()
    {
        finish = false;
        this.transform.localPosition = resetPosition;
    }
}