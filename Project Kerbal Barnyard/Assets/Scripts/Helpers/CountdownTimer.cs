using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float _countdownTime = 3.0f;

    private bool _isCountingDown = false;
    private float _countdownProgress;
    private int _savedSecond;

    public static Action<int> OnSecondInterval = delegate { };
    public static Action OnCountdownOver = delegate { };

    private void Awake()
    {

    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if(_isCountingDown)
        {
            //increment down
            _countdownProgress -= Time.deltaTime;

            //trigger if on second interval
            int second = Mathf.FloorToInt(_countdownProgress) + 1;
            if (second != _savedSecond)
            {
                _savedSecond = second;
                OnSecondInterval?.Invoke(second);

                Debug.Log("Second: " + second);
            }

            //check if over
            if ( _countdownProgress <= 0)
            {
                OnCountdownOver?.Invoke();
                _isCountingDown = false;

                Debug.Log("Countdown Over");
            }
        }
    }
    public void StartCountdown()
    {
        _countdownProgress = _countdownTime;
        _savedSecond = 0;
        _isCountingDown = true;


    }
}
