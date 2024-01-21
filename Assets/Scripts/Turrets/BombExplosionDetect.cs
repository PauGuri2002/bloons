using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosionDetect : MonoBehaviour
{
    private HashSet<GameObject> colliders = new HashSet<GameObject>();
    public HashSet<GameObject> GetColliders() { return colliders; }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Bloon")){
            colliders.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("Bloon")){
            colliders.Remove(other.gameObject);
        }
    }
}
