using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject turretPlacer;
    public GameObject bombTurretPlacer;
    public GameObject watergunTurretPlacer;

    void Start(){

    }

    void Update(){
        
    }

    public void UpgradeMPS(int amount){
        GetComponent<GameLogic>().money -= 100;
        GetComponent<GameLogic>().mps += amount;
    }

    public void SpawnTurret(string type){
        if(GameObject.FindGameObjectsWithTag("TurretPlacer").Length > 0){
            Destroy(GameObject.FindGameObjectsWithTag("TurretPlacer")[0]);
        }
        switch(type){
            case "turret":
                Instantiate(turretPlacer,new Vector3(20,0,0),Quaternion.identity);
                break;
            case "bomb":
                Instantiate(bombTurretPlacer,new Vector3(20,0,0),Quaternion.identity);
                break;
            case "watergun":
                Instantiate(watergunTurretPlacer,new Vector3(20,0,0),Quaternion.identity);
                break;
        }
        
    }
}
