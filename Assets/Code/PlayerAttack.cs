using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using TMPro; 

public class PlayerAttack : MonoBehaviour
{

    // the ray distance of the player bieng able to shoot an object
    private float raycastDist = 50; 

    public LayerMask enemyLayer; 

    // Camera Transform
    public Transform camTrans; 

    // User aim dot
    public Image recticle;

    public TextMeshProUGUI potionScoreText;
    public TextMeshProUGUI bulletCount; 

    private bool recticleTarget = false;

    AudioSource _audioSource;
    public AudioClip potionSound;
    public AudioClip gunSound;
    public AudioClip gunEmpty;
    public AudioClip gunReload;
    public float volume = 0.5f;

    //Game Values
    private int bulletsLeft;
    private bool reloadingState;

    private void Start(){
        //Game Values
        bulletsLeft = 6;
        reloadingState = false;
        _audioSource = GetComponent<AudioSource>();
        AddScorePotion(0);
        SubBullet(); 
    }

    void Update()
    {
        // left mouse button
        if(Input.GetMouseButtonDown(0) && reloadingState == false) {
            if(bulletsLeft > 0){                
                RaycastHit hit; 
                _audioSource.PlayOneShot(gunSound, volume);
                bulletsLeft = bulletsLeft - 1;
                SubBullet(); 
                if(Physics.Raycast(camTrans.position, camTrans.forward, out hit, raycastDist, enemyLayer)){
                    GameObject enemy = hit.collider.gameObject; 
                    if(enemy.CompareTag("Target")){
                        // push back
                        Rigidbody enemyRB = enemy.GetComponent<Rigidbody>(); 
                        enemyRB.AddForce(transform.forward * 800 + Vector3.up * 200);
                        enemyRB.AddTorque(new Vector3(Random.Range(-50,50), Random.Range(-50,50), Random.Range(-50,50)));
                    }
                    if(enemy.CompareTag("Monster")){
                        print(enemy);
                        // PublicVars.AddKill(1);
                        displayenemy.enemyValue += 1;
                        
                        Destroy(enemy);
                    }
                }
            }
            else{
                _audioSource.PlayOneShot(gunEmpty, volume);
            }
        }

        //Pressed R
        if(Input.GetKeyDown("r") && reloadingState == false){
            _audioSource.PlayOneShot(gunReload, volume);
            reloadingState = true;
            StartCoroutine(ReloadDelay());
            bulletsLeft = 6;
            SubBullet(); 
        }
    }


    private IEnumerator ReloadDelay(){
        yield return new  WaitForSecondsRealtime(3f);
        reloadingState = false;
    }

    private void FixedUpdate(){
        RaycastHit hit; 

        if(Physics.Raycast(camTrans.position, camTrans.forward, out hit, raycastDist) && (hit.collider.CompareTag("Target") || hit.collider.CompareTag("Monster"))){
            if(!recticleTarget){
                recticle.color = Color.red; 
                recticleTarget = true; 
            }
        } else if(recticleTarget){
            recticle.color = Color.white; 
            recticleTarget = false; 
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Potion")){
            _audioSource.PlayOneShot(potionSound, volume);
            AddScorePotion(1); 
            if(PublicVars.potion_score >= 5){
                SceneManager.LoadScene("WinScreen"); 
            }
            //displayScore.scoreValue += 1;
            Destroy(other.gameObject);
        }
        if(other.CompareTag("Monster")){
            //You Die. TODO
            //Play death music and load retry screens
            SceneManager.LoadScene("End");
        }
    }

    // Display Potion Count
    void AddScorePotion(int points){
        PublicVars.potion_score += points; 
        potionScoreText.text = "Potion Points: " + PublicVars.potion_score + "/5"; 
    }

    // Display Bullet Count
    void SubBullet(){
        bulletCount.text = "Bullet Count: " + bulletsLeft + "/6"; 
    }
}
