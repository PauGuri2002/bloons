using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    static float[][] waves = new float[][] {
        new float[] { // 1
            0.75f, 20, 1, 0},
        new float[] { // 2
            0.5f, 35, 1, 0},
        new float[] { // 3
            0.3f, 25, 1, 0, 5, 2, 0},
        new float[] { // 4
            0.3f, 35, 1, 0, 18, 2, 0},
        new float[] { // 5
            0.3f, 5, 1, 0, 27, 2, 0},
        new float[] { // 6
            0.3f, 15, 1, 0, 15, 2, 0, 4, 3, 0},
        new float[] { // 7
            0.25f, 20, 1, 0, 20, 2, 0, 5, 3, 0},
        new float[] { // 8
            0.3f, 10, 1, 0, 20, 2, 0, 14, 3, 0},
        new float[] { //9
            0.3f, 30, 3, 0},
        new float[] { // 10
            0.15f, 102, 2, 0},
        new float[] { // 11
            0.3f, 10, 1, 0, 10, 2, 0, 12, 3, 0, 3, 4, 0},
        new float[] { // 12
            0.3f, 15, 2, 0, 10, 3, 0, 5, 4, 0},
        new float[] { // 13
            0.2f, 50, 2, 0, 23, 3, 0},
        new float[] { // 14
            0.3f, 49, 1, 0, 15, 2, 0, 10, 3, 0, 9, 4, 0},
        new float[] { // 15
            0.3f, 20, 1, 0, 15, 2, 0, 12, 3, 0, 10, 4, 0, 5, 5, 0},
        new float[] { // 16
            0.25f, 40, 3, 0, 8, 4, 0},
        new float[] { // 17
            0.25f, 12, 4, 1},
        new float[] { // 18
            0.3f, 80, 3, 0},
        new float[] { // 19
            0.3f, 10, 3, 0, 4, 4, 0, 5, 4, 1, 15, 5, 0},
        new float[] { // 20
            0.25f, 6, 6, 0},
        new float[] { // 21
            0.3f, 40, 4, 0, 14, 5, 0},
        new float[] { // 22
            0.35f, 16, 7, 0},
        new float[] { // 23
            0.3f, 7, 6, 0, 7, 7, 0},
        new float[] { // 24
            0.2f, 20, 2, 0, 1, 3, 2},
        new float[] { // 25
            0.3f, 25, 4, 1, 15, 6, 0},
        new float[] { // 26
            0.3f, 23, 5, 0, 4, 7, 3},
        new float[] { // 27
            0.15f, 100, 1, 0, 60, 2, 0, 45, 3, 0, 45, 4, 0},
        new float[] { // 28
            0.3f, 6, 8, 0},
        new float[] { // 29
            0.3f, 50, 4, 0, 15, 4, 1},
        new float[] { // 30
            0.3f, 9, 8, 0}
    };

    public RoundText roundText;
    public Text moneyText;
    public Text MPSText;
    public Text healthText;
    public RectTransform innerHealthBar;
    public GameObject Bloon;
    [HideInInspector] public int wave = 0;
    int globalHealth = 100;
    public int money = 500;
    public int mps = 30;

    void Start(){
        StartCoroutine(SendWave());
        StartCoroutine(MoneyPerSecond());
    }

    IEnumerator SendWave(){
        while(wave <= waves.Length - 1){
            StartCoroutine(roundText.SetRoundText(wave+1));
            float[] waveItems = waves[wave];
            float frequency = waveItems[0];
            var bloons = new List<int[]>();
            for(int i = 1; i < waveItems.Length; i+=3){
                for(int j = 0; j < waveItems[i]; j++){
                    bloons.Add(new int[]{(int) waveItems[i+1], (int) waveItems[i+2]});
                }
            }
            int len = bloons.Count;
            for(int i = 0; i < len; i++){
                yield return new WaitForSeconds(frequency);
                var randomIndex = (int) Mathf.Floor(Random.Range(0, bloons.Count));
                GameObject bloonInstance = Instantiate(Bloon,new Vector3(20,0,0),Quaternion.identity);
                Bloon bScript = bloonInstance.GetComponent<Bloon>();
                bScript.health = bloons[randomIndex][0];
                bScript.modifier = bloons[randomIndex][1];
                bloonInstance.name = "Bloon" + i;
                bloons.RemoveAt(randomIndex);
            }

            wave++;
            yield return new WaitUntil(() => {
                return GameObject.FindGameObjectsWithTag("Bloon").Length == 0;
            });
            yield return new WaitForSeconds(2);
        }
        SceneManager.LoadScene("Menu");
    }

    IEnumerator MoneyPerSecond(){
        while(true){
            money += mps;
            yield return new WaitForSeconds(1);
        }
    }

    public void GlobalDamage(int damage){
        globalHealth -= damage;
        healthText.text = globalHealth.ToString();
        innerHealthBar.localScale = new Vector3(((float)globalHealth/100.0f),innerHealthBar.localScale.y,innerHealthBar.localScale.z);
    }

    void Update(){
        moneyText.text = "$ " + money;
        MPSText.text = mps + "$/s";
    }
    
}
