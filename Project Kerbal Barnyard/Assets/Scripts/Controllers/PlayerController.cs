using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public RocketParent shipInfo;
    public GameObject parentObj;
    
    [Header("Movement Stuff")]
    private float forceMagnitude;
    public float gravityScale = 10f;
    private Rigidbody2D rigidBody2D;
    
    private float startingY;

    [Header("Win Conditions")]
    public float winHeight;


    private void Awake() {
        rigidBody2D = parentObj.GetComponent<Rigidbody2D>();
        startingY = parentObj.transform.position.y;
    }
    
    public void Launch() { // initial launch speed
        rigidBody2D.gravityScale = gravityScale; // starting gravity

        forceMagnitude = shipInfo.CalculateVelocity();

        rigidBody2D.AddForce(Vector2.up * forceMagnitude);
    }

    public bool Falling() {
        if(rigidBody2D.velocity.y < 0f) { return true; }
        else { return false; }
    }

    public void SetGravityScale(int num) {
        rigidBody2D.gravityScale = num;
    }

    public float GetCurrentHeight() {
        float currPoss = parentObj.transform.position.y;
        return currPoss - startingY;
    }

    public float GetCurrentSpeed() {
        return rigidBody2D.velocity.y;
    }

    public void ResetVeloctity() {
        rigidBody2D.velocity = Vector2.zero;
    }

    public void MoveParts(float currHeight) {
        // move red sq to where the char part is

        // move transform of Rocket Parts with red sq
        Vector3 newPos = transform.position;
        newPos.y = currHeight;
        parentObj.transform.position = newPos;
    }
}