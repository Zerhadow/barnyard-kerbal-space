using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private BuildController _buildController;

    [Header("Properties")]
    public int puchasesCost = 10;

    [Header("Stats")]
    public int weight;
    public int thrust;
    public int durability;

    private void Awake()
    {
        _buildController = FindObjectOfType<BuildController>();
    }
}
