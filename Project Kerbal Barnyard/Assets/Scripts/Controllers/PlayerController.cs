using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    [Header("Movement Stuff")]
    private float forceMagnitude;
    public float gravityScale = 10f;
    private Rigidbody2D rigidBody2D;
    [Header("Physics Calculations")]
    private float startingY;
    public int charMass;
    public int totalLoadMass;
    private int totalMass;
    public int totalThrust;


    private void Awake() {
        rigidBody2D = player.GetComponent<Rigidbody2D>();
        startingY = player.transform.position.y;
    }
    
    public void Launch() { // initial launch speed
        rigidBody2D.gravityScale = gravityScale;

        forceMagnitude = CalculateVelocity();

        rigidBody2D.AddForce(Vector2.up * forceMagnitude);
    }

    private float CalculateVelocity() { // F = ma - mg
        float fGrav = charMass * 9.81f; // force of char
        float fBoosters = totalLoadMass * totalThrust;

        float fNet = fBoosters - fGrav;
        Debug.Log("fNet: " + fNet);

        return fNet;
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

    public float GetCurrentSpeed() {
        return rigidBody2D.velocity.y;
    }
}