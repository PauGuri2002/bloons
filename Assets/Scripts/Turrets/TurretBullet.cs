using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public Vector3 targetPosition;
    Vector3 direction;
    public float speed = 20;

    void Start(){
        direction = targetPosition - transform.position;
        direction.Normalize();
    }

    void Update(){
        transform.position += direction * Time.deltaTime * speed;

        if(Mathf.Abs(transform.position.x) > 15 || Mathf.Abs(transform.position.y) > 15){
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Bloon")){
            other.gameObject.GetComponent<Bloon>().GetHit(1);
            Destroy(this.gameObject);
        }
    }
}
