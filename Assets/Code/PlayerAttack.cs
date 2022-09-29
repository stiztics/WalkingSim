using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    // the ray distance of the player bieng able to shoot an object
    private float raycastDist = 50; 

    public LayerMask enemyLayer; 

    // Camera Transform
    public Transform camTrans; 

    // User aim dot
    public Image recticle;

    private bool recticleTarget = false;

    AudioSource _audioSource;
    public AudioClip coinSound;
    public AudioClip gunSound;
    public float volume = 0.5f;

    private void Start(){
        _audioSource = GetComponent<AudioSource>();
        PublicVars.AddScore(0);
    }

    void Update()
    {
        // left mouse button
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit; 

            if(Physics.Raycast(camTrans.position, camTrans.forward, out hit, raycastDist, enemyLayer)){
                GameObject enemy = hit.collider.gameObject; 
                //_audioSource.PlayOneShot(gunSound, volume);                         having problem with this code. Cannot play the audio clip.
                if(enemy.CompareTag("Target")){
                    // push back
                    Rigidbody enemyRB = enemy.GetComponent<Rigidbody>(); 
                    enemyRB.AddForce(transform.forward * 800 + Vector3.up * 200);
                    enemyRB.AddTorque(new Vector3(Random.Range(-50,50), Random.Range(-50,50), Random.Range(-50,50)));
                }
                if(enemy.CompareTag("Monster")){
                    PublicVars.AddKill(1);
                    displayenemy.enemyValue += 1;
                    
                    Destroy(enemy.transform.parent.gameObject);
                }
            }
        }
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
        if(other.CompareTag("Coin")){
            //_audioSource.PlayOneShot(coinSound, volume);                   Same Problems here. Cannot play the audio clip
            PublicVars.AddScore(1);
            displayScore.scoreValue += 1;
            Destroy(other.gameObject);
        }
    }
}

