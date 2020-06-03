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
    private Vector3 initForward;

    void Start()
    {
        initForward = transform.forward;
        projectileAudio = GetComponent<AudioSource>();
        projectileAudio.Play();
        pos = this.transform.position;
        distance = Vector3.Distance(this.transform.position, posTarget);
        distance2 = distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position += initForward * (speed * Time.deltaTime);

            finalA = this.transform.position;
            finalA.z -= 0.35f;
            distance = Vector3.Distance(finalA, target.transform.position);
            if (Mathf.Abs(Vector3.Distance(pos, this.transform.position)) > range || target == null)
            {
                if (this.CompareTag("FireBall"))
                {
                    HideFireBall();
                    StartCoroutine(FireWaitEnd());
                }
                else
                    Destroy(this.gameObject);
            }
            else if (distance < 0.9f || target == null)
            {
                if (this.CompareTag("FireBall"))
                {
                    HideFireBall();
                    StartCoroutine(FireWaitEnd());
                }
                else
                    Destroy(this.gameObject);
            } 
        }
        else Destroy(this.gameObject);
        Debug.Log(target);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target)
        {
            if (this.CompareTag("FireBall"))
            {
                HideFireBall();
                StartCoroutine(FireWaitEnd());
            }
            else
                Destroy(this.gameObject);
        }
    } 

    IEnumerator FireWaitEnd()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);

    }
    private void HideFireBall()
    {
        this.gameObject.GetComponentInChildren<Light>().enabled = false;
        ParticleSystem.EmissionModule em = this.GetComponent<ParticleSystem>().emission;
        em.enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
    }
}
