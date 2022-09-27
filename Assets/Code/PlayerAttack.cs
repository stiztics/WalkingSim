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

    void Update()
    {
        // left mouse button
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit; 

            if(Physics.Raycast(camTrans.position, camTrans.forward, out hit, raycastDist, enemyLayer)){
                GameObject enemy = hit.collider.gameObject; 

                if(enemy.CompareTag("Target")){
                    // destroy enemy
                    //Destroy(enemy);

                    // push back
                    Rigidbody enemyRB = enemy.GetComponent<Rigidbody>(); 
                    enemyRB.AddForce(transform.forward * 800 + Vector3.up * 200);
                    enemyRB.AddTorque(new Vector3(Random.Range(-50,50), Random.Range(-50,50), Random.Range(-50,50)));
                }
            }
        }
    }

    private void FixedUpdate(){
        RaycastHit hit; 

        if(Physics.Raycast(camTrans.position, camTrans.forward, out hit, raycastDist) && hit.collider.CompareTag("Target")){
            if(!recticleTarget){
                recticle.color = Color.red; 
                recticleTarget = true; 
            }
        } else if(recticleTarget){
            recticle.color = Color.white; 
            recticleTarget = false; 
        }
    }
}
