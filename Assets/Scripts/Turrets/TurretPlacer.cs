using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacer : MonoBehaviour
{
    public GameObject turretPrefab;
    GameLogic gameLogic;
    public int price = 500;
    public bool isWaterTurret = false;
    bool canPlace = false;
    SpriteRenderer sRenderer;
    Color normalColor;
    Color darkRed = new Color(0.2f,0,0);

    void Start(){
        sRenderer = GetComponent<SpriteRenderer>();
        gameLogic = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameLogic>();
        normalColor = sRenderer.color;
        sRenderer.color = darkRed;
    }

    void Update(){
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, -9);
    }

    void OnMouseDown(){
        if(canPlace){
            gameLogic.money -= price;
            gameLogic.mps -= (int) Mathf.Floor(gameLogic.wave/2);
            Instantiate(turretPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if((!isWaterTurret && other.gameObject.CompareTag("Buildable")) || (isWaterTurret && other.gameObject.CompareTag("Water"))){
            sRenderer.color = normalColor;
            canPlace = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if((!isWaterTurret && other.gameObject.CompareTag("Buildable")) || (isWaterTurret && other.gameObject.CompareTag("Water"))){
            sRenderer.color = darkRed;
            canPlace = false;
        }
    }
}
