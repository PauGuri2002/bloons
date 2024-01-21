using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundText : MonoBehaviour
{   
    Text roundText;

    void Start(){
        roundText = GetComponent<Text>();
    }

    public IEnumerator SetRoundText(int round){
        roundText.canvasRenderer.SetAlpha(1);
        roundText.text = "Wave " + round;
        yield return new WaitForSeconds(2);
        roundText.CrossFadeAlpha(0,2.0f,false);
    }

    void Update(){
        
    }
}
