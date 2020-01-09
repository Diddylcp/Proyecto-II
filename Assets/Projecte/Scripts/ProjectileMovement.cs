using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private AudioSource projectileAudio;
    public float speed;

    void Start()
    {
        projectileAudio = GetComponent<AudioSource>();
        projectileAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision co)
    {
        speed = 0;
        Destroy(this.gameObject);
    }
}
