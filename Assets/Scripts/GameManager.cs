using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Move[] movableObjects; 
    public Button resetButton;

    // Start is called before the first frame update
    void Start()
    {
        resetButton.onClick.AddListener(ResetObjects);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetObjects()
    {
        foreach (Move obj in movableObjects)
        {
            obj.ResetObject();
        }
    }
}
