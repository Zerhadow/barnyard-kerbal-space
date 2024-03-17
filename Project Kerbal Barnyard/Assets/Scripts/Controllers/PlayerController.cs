using System;
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

    //save starting gravity scale to reset it at start of each play state
    public float startingGravity {  get; private set; }
    
    private float startingY;

    [Header("Win Conditions")]
    public float winHeight;

    public static Action OnRocketLaunched = delegate { };
    public static Action OnFlightOver = delegate { };

    private void Awake() {
        rigidBody2D = parentObj.GetComponent<Rigidbody2D>();
        startingY = parentObj.transform.position.y;
        startingGravity = gravityScale;
    }
    
    public void Launch() { // initial launch speed
        rigidBody2D.gravityScale = gravityScale; // starting gravity

        forceMagnitude = shipInfo.CalculateVelocity();

        rigidBody2D.AddForce(Vector2.up * forceMagnitude);

        OnRocketLaunched?.Invoke();
    }

    public bool Falling() {
        if(rigidBody2D.velocity.y < 0f) { return true; }
        else { return false; }
    }

    public void SetGravityScale(int num) {
        rigidBody2D.gravityScale = num;
    }
    public void SetGravityScale(float num)
    {
        rigidBody2D.gravityScale = num;
    }
    public float GetGravityScale()
    {
        return rigidBody2D.gravityScale;
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

    public string GetCharacterName() {
        Debug.Log("Char Name " + shipInfo.charType);

        if(shipInfo.charType.Contains("RP_1x1 Cat")) {
            return "Cat";
        } else if(shipInfo.charType.Contains("RP_1x1 Chicken")) {
            return "Chicken";
        } else if(shipInfo.charType.Contains("RP_1x1 Cow")) {
            return "Cow";
        } else {
            Debug.LogError("character name unknown");
            return null;
        }
    }
}