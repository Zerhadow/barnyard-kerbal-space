using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public RocketParent shipInfo;
    [Header("Movement Stuff")]
    private float forceMagnitude;
    public float gravityScale = 10f;
    private Rigidbody2D rigidBody2D;
    [Header("Physics Calculations")]
    private float startingY;
    public int totalLoadMass;
    private int totalMass;
    public int totalThrust;
    [Header("Win Conditions")]
    public float winHeight;


    private void Awake() {
        rigidBody2D = player.GetComponent<Rigidbody2D>();
        startingY = player.transform.position.y;
    }
    
    public void Launch() { // initial launch speed
        rigidBody2D.gravityScale = gravityScale; // starting gravity

        forceMagnitude = shipInfo.CalculateVelocity();

        rigidBody2D.AddForce(Vector2.up * forceMagnitude);

        // have ship transform follow player
    }

    public bool Falling() {
        if(rigidBody2D.velocity.y < 0f) { return true; }
        else { return false; }
    }

    public void SetGravityScale(int num) {
        rigidBody2D.gravityScale = num;
    }

    public float GetCurrentHeight() {
        float currPoss = player.transform.position.y;
        return currPoss - startingY;
    }

    public float GetCurrentSpeed() {
        return rigidBody2D.velocity.y;
    }
}