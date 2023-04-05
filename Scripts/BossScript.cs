using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{

    public GameObject skelly;
    public SwordInfo axe;
    private NavMeshAgent bossSkel;
    public Transform MainCharacter;
    public GameObject MainCharPosition;
    public float distance;
    public int skeletonHealth = 12;
    
    void Start()
    {
        MainCharPosition = GameObject.FindGameObjectWithTag("Player");
        MainCharacter = GameObject.FindGameObjectWithTag("Player").transform;
        bossSkel = GetComponent<NavMeshAgent>();

    }
    public void isAttacking()
    {
        axe.isAttacking();
    }
    
    public void isNotAttacking()
    {
        axe.isNotAttacking();
    }

    [Obsolete("Obsolete")]
    void Update()
    {
        
        GameObject BossAxe = GameObject.Find("Axe");
        
        distance = Vector3.Distance (MainCharPosition.transform.position, bossSkel.transform.position);

        if (skeletonHealth > 0 && distance < 1)
        {
            skelly.GetComponent<Animator>().Play("Attack02");
            
            StartCoroutine (Timer());

            

            IEnumerator Timer()
            {
                yield return new WaitForSecondsRealtime((float)0.1);
                BossAxe.GetComponent<BoxCollider>().isTrigger = true;
                yield return new WaitForSecondsRealtime((float)0.4);
                BossAxe.GetComponent<BoxCollider>().isTrigger = false;

            }

        }

        if (skeletonHealth <= 0)
        {
            Destroy(gameObject);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

       else
        {
            bossSkel.SetDestination(MainCharacter.position);
            TurnToTarget(); 
        }
        
    }
    void TurnToTarget()
    {
        Vector3 direction = (MainCharacter.position - transform.position).normalized;
        Quaternion turnRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = turnRotation;
    }

    
    Boolean isHit = false;

    private void setIsHitBack()
    {
        isHit = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.name == "Sword_Curved" && isHit == false)
        {
            skeletonHealth = skeletonHealth - 3;
            Debug.Log("Skeleton is hit, its health is:  "+ skeletonHealth);
            isHit = true;
            Invoke("setIsHitBack", (float)0.6);

        }
        
    }
}
