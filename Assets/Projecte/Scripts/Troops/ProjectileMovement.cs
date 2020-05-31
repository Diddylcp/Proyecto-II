using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private AudioSource projectileAudio;
    public float speed;
    Vector3 pos;
    public float range;
    public GameObject target;
    

    void Start()
    {
        projectileAudio = GetComponent<AudioSource>();
        projectileAudio.Play();
        pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
        
        if (Mathf.Abs(Vector3.Distance(pos, this.transform.position)) > range)
        {
            Destroy(this.gameObject);
        }
    }
    
   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target)
         Destroy(this.gameObject);


    } 
}
