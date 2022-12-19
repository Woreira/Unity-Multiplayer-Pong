using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Ball : NetworkBehaviour{
    
    public float speed;
    Vector3 dir;
    Rigidbody2D rb;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        dir = Random.insideUnitCircle;
    }
    
    void Update(){
    }

    void FixedUpdate(){
        Constrain();
        Move();
    }

    void Move(){
       rb.velocity = dir * speed;
    }

    void Flip(){
        dir.x *= -1f;
    }

    void Constrain(){
        if(transform.position.y > 5f || transform.position.y < -5f) dir.y *= -1f;
        if(transform.position.x > 9f || transform.position.x < -9f) dir.x *= -1f; 
    }

    void OnCollisionEnter2D(Collision2D c){
        Flip();
    }
}