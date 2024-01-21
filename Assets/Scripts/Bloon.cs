using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloon : MonoBehaviour
{
    static Color[] colors = new Color[]{
        new Color(1,0,0),
        new Color(0,0,1),
        new Color(0,1,0),
        new Color(1,1,0),
        new Color(1,0,1),
        new Color(0,0,0),
        new Color(1,1,1),
        new Color(0.5f, 0.5f, 0.5f)
    };
    static float[] MoveSpeed = new float[]{1.0f,1.4f,1.8f,3.2f,3.5f,1.8f,2.0f,1.0f};

    Transform[] PathNodes;
    GameLogic gameLogic;
    public GameObject destroyParticle;
    public int health = 1;
    float Timer;
    float distance;
    int NodeIndex = 1;
    Vector3 CurrentPosition;
    Vector3 StartPosition;
    SpriteRenderer sRenderer;

    [HideInInspector] public int modifier = 0;

    // regen bloon (modifier = 1)
    bool healing = false;
    int baseHealth;
    public Sprite regenSprite;
    // camo bloon (modifier = 2)
    // zebra bloon (modifier = 3)
    
    void Start(){
        PathNodes = GameObject.FindGameObjectsWithTag("Path")[0].GetComponentsInChildren<Transform>();
        gameLogic = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameLogic>();
        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.color = colors[health-1];

        // regen only
        if(modifier == 1){
            sRenderer.sprite = regenSprite;
            baseHealth = health;
        } // camo only 
        else if(modifier == 2){
            Color temp = sRenderer.color;
            temp.a = 0.4f;
            sRenderer.color = temp;
        }

        transform.position = PathNodes[1].position;
        CheckNode();
    }

    void CheckNode(){
        Timer = 0;
        StartPosition = transform.position;
        CurrentPosition = PathNodes[NodeIndex].position;
        distance = Vector3.Distance(CurrentPosition, StartPosition);
    }

    void Update(){
        Timer += Time.deltaTime * (MoveSpeed[health-1]*3) / distance;
        if(transform.position != CurrentPosition){
            transform.position = Vector3.Lerp(StartPosition, CurrentPosition, Timer);
        } else {
            if(NodeIndex < PathNodes.Length - 1){
                NodeIndex++;
                CheckNode();
            } else {
                gameLogic.GlobalDamage(health);
                Destroy(this.gameObject);
            }
        }
    }

    // regen only
    IEnumerator Heal(){
        healing = true;
        while(health < baseHealth){
            yield return new WaitForSeconds(1);
                health++;
                sRenderer.color = colors[health-1];
        }
        healing = false;
    }

    public void GetHit(int damage){
        health -= damage;
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
        if(health <= 0){
            Destroy(this.gameObject);
        } else {
            sRenderer.color = colors[health-1];

            //zebra only
            if(modifier == 3){
                Debug.Log("Secondary bloon spawn");
            }

            // regen only
            if(!healing && modifier == 1){
                StartCoroutine(Heal());
            }
        }
    }
}
