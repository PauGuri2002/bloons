using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceButton : MonoBehaviour
{
    Button button;
    public int price = 0;
    public GameLogic gameLogic;

    void Start(){
        button = GetComponent<Button>();
    }

    void Update(){
        if(gameLogic.money >= price){
            button.interactable = true;
        } else {
            button.interactable = false;
        }
    }
}
