using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatergunBullet : MonoBehaviour
{
    public Vector3 targetPosition;
    Vector3 direction;

    void Start(){
        direction = targetPosition - transform.position;
        direction.Normalize();
        StartCoroutine(delayedDestroy());
    }

    void Update(){

    }

    IEnumerator delayedDestroy(){
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Bloon")){
            other.gameObject.GetComponent<Bloon>().GetHit(1);
        }
    }
}
