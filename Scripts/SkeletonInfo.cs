using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonInfo : MonoBehaviour
{

    public GameObject skelly;
    public SwordInfo badsword;
    private NavMeshAgent skeleton;
    public Transform MainCharacter;

    public GameObject MainCharPosition;

    public float distance;

    public static int enemyCount = 19;

    public int skeletonHealth = 5;

    void Start()
    {
        MainCharPosition = GameObject.FindGameObjectWithTag("Player");
        MainCharacter = GameObject.FindGameObjectWithTag("Player").transform;


        skeleton = GetComponent<NavMeshAgent>();

    }
    
    public void isAttacking()
    {
        badsword.isAttacking();
    }
    
    public void isNotAttacking()
    {
        badsword.isNotAttacking();
    }
    
    [Obsolete("Obsolete")]
    void Update()
    {
        
    //    GameObject skellySword = GameObject.Find("SkeletonSword");

        distance = Vector3.Distance (MainCharPosition.transform.position, skeleton.transform.position);

        if (skeletonHealth > 0 && distance < 1)
        {
            skelly.GetComponent<Animator>().Play("DS_onehand_attack_A");
            
          /*  StartCoroutine (Timer());
            IEnumerator Timer()
            {
                yield return new WaitForSecondsRealtime((float)0.33);
                skellySword.GetComponent<BoxCollider>().isTrigger = true;
                yield return new WaitForSecondsRealtime((float)0.7);
                skellySword.GetComponent<BoxCollider>().isTrigger = false;

            }*/

        }

        if (skeletonHealth <= 0)
        {
            Destroy(gameObject);
            enemyCount--;
            Debug.Log("Number of enemies left: "+enemyCount);

        }

        else
        {
            skeleton.SetDestination(MainCharacter.position);
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
