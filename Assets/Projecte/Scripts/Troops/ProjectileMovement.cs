using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private AudioSource projectileAudio;
    public float speed;
    Vector3 pos;
    public Vector3 posTarget;
   
    public float range;
    public GameObject target;
    float distance;
    float distance2;
    Vector3 finalA;


    void Start()
    {
        projectileAudio = GetComponent<AudioSource>();
        projectileAudio.Play();
        pos = this.transform.position;
        distance = Vector3.Distance(this.transform.position, posTarget);
        distance2 = distance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);

        finalA = this.transform.position;
        finalA.z -= 0.35f;
        distance = Vector3.Distance(finalA, posTarget);
        print(distance);

        if (Mathf.Abs(Vector3.Distance(pos, this.transform.position)) > range)
        {
            Destroy(this.gameObject);
        }
        else if (distance < 0.9f)
        {
            if (this.CompareTag("FireBall"))
            {
                StartCoroutine(FireWaitEnd());
            }
            else
            Destroy(this.gameObject);
        }
        

    }
    
   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target)
         Destroy(this.gameObject);


    } 

    IEnumerator FireWaitEnd()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);

    }
}
