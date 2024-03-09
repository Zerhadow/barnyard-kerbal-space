using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class Utility
{
    public static TextMeshPro CreateWorldText3D(string value, float fontSize, Vector3 position, Quaternion rotation)
    {
        //create gameobject
        GameObject textObject = new GameObject("DebugDisplay");
        TextMeshPro textMesh = textObject.AddComponent<TextMeshPro>();
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();

        //modify text settings
        textObject.transform.position = position;

        rectTransform.sizeDelta = new Vector2(1f, 0.5f);
        rectTransform.rotation = rotation;

        textMesh.fontSize = fontSize;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.alignment = TextAlignmentOptions.Midline;

        //update string
        textMesh.text = value;

        return textMesh;
    }
    public static TextMeshPro CreateWorldText2D(string value, float fontSize, Vector3 position)
    {
        //create gameobject
        GameObject textObject = new GameObject("DebugDisplay");
        TextMeshPro textMesh = textObject.AddComponent<TextMeshPro>();
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();

        //modify text settings
        textObject.transform.position = position;

        rectTransform.sizeDelta = new Vector2(1f, 0.5f);
        rectTransform.rotation = Quaternion.identity;

        textMesh.fontSize = fontSize;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.alignment = TextAlignmentOptions.Midline;

        //update string
        textMesh.text = value;

        return textMesh;
    }
    public static RaycastHit GetMouseHit()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = 10f;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out hit))
        {
            return hit;
        }
        else
            return default;

        
    }
    public static RaycastHit2D GetMouseHit2D()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        return hit;
    }
    public static Vector3 GetWorldMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
