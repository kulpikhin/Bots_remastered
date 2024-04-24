using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private InputSystem _inputSystem;

    private void OnEnable()
    {
        _inputSystem.InputGeted += Move;
    }

    private void OnDisable()
    {
        _inputSystem.InputGeted -= Move;
    }

    public void Move(Vector3 _direction)
    {
        _direction *= Time.deltaTime * _speed;
        transform.position += _direction;
    }
}
