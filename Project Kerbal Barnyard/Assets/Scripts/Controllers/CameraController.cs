using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameController _gameController;

    [SerializeField] private CameraFollow _cameraFollow;

    private void Awake()
    {
        _gameController = GetComponentInParent<GameController>();
    }

    public void EnableDisableCameraFollow(bool enable)
    {
        if(_cameraFollow != null) _cameraFollow.isFollowing = enable;
    }
}
