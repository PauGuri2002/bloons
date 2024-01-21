using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{
    public Vector3 targetPosition;
    Vector3 direction;
    public float speed = 20;
    public BombExplosionDetect explosionDetect;
    public GameObject explodeParticle;

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
            HashSet<GameObject> colliders = new HashSet<GameObject>(explosionDetect.GetColliders());
            foreach(GameObject collider in colliders){
                collider.GetComponent<Bloon>().GetHit(1);
            }
            Instantiate(explodeParticle,transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
