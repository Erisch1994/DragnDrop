using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShapeMatching : MonoBehaviour
{
    public GameObject correctShape;
    public TextMeshProUGUI resultText;
    private bool isDragging;
    private bool isFinished;
    private bool isMatched;

    private Vector3 startPosition;
    private float displayTimer = 0f;

    public delegate void MatchedAction();
    public event MatchedAction OnMatched;

    void Update()
    {

        if (displayTimer > 0)
        {
            displayTimer -= Time.deltaTime;

            if (displayTimer <= 0)
            {
                resultText.gameObject.SetActive(false);
            }
        }

        if (!isFinished)
        {
            if (isDragging)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                transform.localPosition = new Vector3(mousePos.x - startPosition.x, mousePos.y - startPosition.y, transform.localPosition.z);
            }

        }
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosition = new Vector3(mousePos.x - transform.localPosition.x, mousePos.y - transform.localPosition.y, transform.localPosition.z);

            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (Mathf.Abs(transform.localPosition.x - correctShape.transform.localPosition.x) < 0.5f &&
            Mathf.Abs(transform.localPosition.y - correctShape.transform.localPosition.y) < 0.5f)
        {
            transform.position = new Vector3(correctShape.transform.position.x, correctShape.transform.position.y, correctShape.transform.position.z);
            isFinished = true;
            isMatched = true;
            if (OnMatched != null)
            {
                OnMatched();
            }
        }
        else
        {
            transform.localPosition = startPosition;
            isMatched = false;

            resultText.text = "FAIL!";
            resultText.gameObject.SetActive(true);

            displayTimer = 1f;
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
        isFinished = false;
        transform.localPosition = startPosition;
    }
}
