using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatergunTurret : MonoBehaviour
{
    public float cooldown = 1.5f;
    public GameObject bullet;
    float timer = 0;
    List<GameObject> targets;

    void Start(){
        targets = new List<GameObject>();
    }

    void Update(){
        if(targets.Count > 0){
            if(timer <= 0){
                Vector3 dir = targets[0].transform.position - transform.position;
                dir.Normalize();
                float zRotation = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f,0f,zRotation);

                var bulletInstance = Instantiate(bullet, transform.position, transform.rotation);
                bulletInstance.GetComponent<WatergunBullet>().targetPosition = targets[0].transform.position;
                timer = cooldown;
            }
        }
        timer -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Bloon")){
            targets.Add(other.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("Bloon")){
            targets.Remove(other.gameObject);
        }
    }
}