using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private UIController _ui;
    [SerializeField] private AudioController _audioController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private BuildController _buildController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private DurabilityEvents _durabilityEventController;
    public UIController UI => _ui;
    public AudioController audioController => _audioController;
    public PlayerController playerController => _playerController;
    public BuildController buildController => _buildController;
    public CameraController cameraController => _cameraController;
    public SceneController sceneController => _sceneController;
    public DurabilityEvents DurabilityEvents => _durabilityEventController;
}
