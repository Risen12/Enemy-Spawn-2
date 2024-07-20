using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Func<Enemy> TimeChanged;

    private float _delay;
    private Coroutine _timer;

    private void OnEnable()
    {
        _delay = 2f;
        _timer = StartCoroutine(SetTimer(_delay));
    }

    private void OnDisable()
    {
        StopCoroutine(_timer);
    }

    private IEnumerator SetTimer(float seconds)
    { 
        WaitForSeconds wait = new WaitForSeconds(seconds);

        while (true)
        {
            yield return new WaitForSeconds(seconds);
            TimeChanged?.Invoke();
        }
    }
}
