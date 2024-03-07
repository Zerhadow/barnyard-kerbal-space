using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    [Header("Movement Stuff")]
    public float forceMagnitude = 10f;
    public float gravityScale = 10f;
    private int totalWeight;
    private int totalThrust;
    private Rigidbody2D rigidBody2D;
    [Header("Physics Calculations")]
    private float startingY;


    private void Awake() {
        rigidBody2D = player.GetComponent<Rigidbody2D>();
        startingY = player.transform.position.y;
    }
    
    public void Launch() { // initial launch speed
        rigidBody2D.gravityScale = gravityScale;

        rigidBody2D.AddForce(Vector2.up * forceMagnitude);
    }

    public bool Falling() {
        if(rigidBody2D.velocity.y < 0f) { return true; }
        else { return false; }
    }

    public void SetGravityScale(int num) {
        rigidBody2D.gravityScale = num;
    }

    public float GetCurrentHeight(float currPoss) {
        return currPoss - startingY;
    }
}