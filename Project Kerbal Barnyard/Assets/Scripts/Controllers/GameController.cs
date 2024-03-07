using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private UIController _ui;
    [SerializeField] private AudioController _audioController;
    [Header("Play Dependencies")]
    public GameObject player;
    public float forceMagnitude = 10f;
    public float gravityScale = 10f;
    public UIController UI => _ui;
    public AudioController audioController => _audioController;
}
