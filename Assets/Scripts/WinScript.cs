using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinScript : MonoBehaviour
{
    public Move[] movableObjects = new Move[3];
    public TextMeshProUGUI winMessage;
    // Start is called before the first frame update
    void Start()
    {
        if (winMessage == null)
        {
            Debug.LogError("WinMessage is not assigned!");
            return;
        }

        winMessage.text = "";
        
        foreach (Move obj in movableObjects)
        {
            obj.OnMatched += CheckWinCondition;
        }

        void CheckWinCondition()
        {

            bool allMatched = true;

            foreach (Move obj in movableObjects)
            {
                if (!obj.IsMatched) 
                {
                    allMatched = false;
                    break;
                }
            }

            if (allMatched)
            {
                winMessage.text = "Congratulations!!! Press Reset button to try again!";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
