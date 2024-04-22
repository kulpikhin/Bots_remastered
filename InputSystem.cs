using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputSystem : MonoBehaviour
{
    private float _xLeftScope = -37f;
    private float _xRightScope = -14f;
    private float _zUpScope = 35f;
    private float _zDownScope = 0f;

    private KeyCode _up = KeyCode.W;
    private KeyCode _down = KeyCode.S;
    private KeyCode _left = KeyCode.A;
    private KeyCode _right = KeyCode.D;

    private Vector3 _direction;
    private CameraMover _cameraMover;
    private bool _isAllowableKey;

    public event UnityAction<Vector3> InputGeted;

    private void Awake()
    {
        _cameraMover = FindAnyObjectByType<CameraMover>();
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {       
        if (Input.GetKey(_up) && _cameraMover.transform.position.z <= _zUpScope)
        {
            _direction = new Vector3(0, 0, 1);
            _isAllowableKey = true;
        }

        if (Input.GetKey(_down) && _cameraMover.transform.position.z >= _zDownScope)
        {
            _direction = new Vector3(0, 0, -1);
            _isAllowableKey = true;
        }

        if (Input.GetKey(_left) && _cameraMover.transform.position.x >= _xLeftScope)
        {
            _direction = new Vector3(-1, 0, 0);
            _isAllowableKey = true;
        }

        if (Input.GetKey(_right) && _cameraMover.transform.position.x <= _xRightScope)
        {
            _direction = new Vector3(1, 0, 0);
            _isAllowableKey = true;
        }

        if (_isAllowableKey)
        {
            InputGeted?.Invoke(_direction);
            _isAllowableKey = false;
        }
    }
}
