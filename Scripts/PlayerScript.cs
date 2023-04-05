using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    public GameObject mainCharacter;
    public SwordInfo curvy;
    public CharacterController controller;
    public int PlayerHealth = 1;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public Transform cameraTransform;


    void Update()
    {
        GameObject charSword = GameObject.Find("Sword_Curved");
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 direction = new Vector3(-vertical, 0f, horizontal).normalized;

        if (direction.magnitude >= 0.1f)
        {
            
            mainCharacter.GetComponent<Animator>().Play("Female Sword Walk");
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            controller.Move(( direction * speed * Time.deltaTime ));
            
        }
        
        else if (direction.magnitude < 0.1f && Input.GetKeyDown(KeyCode.Space))
        {
            
            
            mainCharacter.GetComponent<Animator>().Play("sword attack");
            /*StartCoroutine (Timer());

            

            IEnumerator Timer()
            {
                yield return new WaitForSecondsRealtime((float)0.15);
                charSword.GetComponent<BoxCollider>().isTrigger = true;
                yield return new WaitForSecondsRealtime((float)0.55);
                charSword.GetComponent<BoxCollider>().isTrigger = false;

            }*/

            
            

            Debug.Log("space was pressed");
        }

        
        cameraTransform.rotation= Quaternion.identity;
        
        if (PlayerHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        


    }
    
    Boolean isHit = false;

    private void setIsHitBack()
    {
        isHit = false;
    }
    
    public void isAttacking()
    {
        curvy.isAttacking();
    }
    
    public void isNotAttacking()
    {
        curvy.isNotAttacking();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.name == "SkeletonSword"&& isHit == false)
        {
            PlayerHealth = PlayerHealth - 2;
            Debug.Log("The player has been damaged. Health: "+ PlayerHealth);
            isHit = true;
            Invoke("setIsHitBack", (float)0.6);

        }
        
        else if (other.name == "Axe" && isHit == false)
        {
            PlayerHealth = PlayerHealth - 4;
            Debug.Log("The player has been damaged. Health: "+ PlayerHealth);
            isHit = true;
            Invoke("setIsHitBack", (float)0.6);


        }
        
    }
}
