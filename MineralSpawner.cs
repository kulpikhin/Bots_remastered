using UnityEngine;
using UnityEngine.UIElements;

public class MineralSpawner : MonoBehaviour
{
    [SerializeField] private Mineral _mineral;
    [SerializeField] private float _colliderRadius;
    [SerializeField] private float _spawnCooldown;

    private float _xLeftScope = -45f;
    private float _xRightScope = -7f;
    private float _zUpScope = 45f;
    private float _zDownScope = 6f;
        
    private string _SpawnName = "Spawn";
    private string _maskName = "Floor";

    private MineralDistrebutor _mineralDistrebutor;
    private Vector3 _position;
    private int _startTime = 0;
    
    private void Awake()
    {
        _mineralDistrebutor = FindObjectOfType<MineralDistrebutor>();
    }

    private void Start()
    {
        InvokeRepeating(_SpawnName, _startTime, _spawnCooldown);
    }

    private void Spawn()
    {
        FindFreePlace();

        Mineral newMineral = Instantiate(_mineral, _position, Quaternion.identity);
        _mineralDistrebutor.DistributeMineral(newMineral);
    }

    private void FindFreePlace()
    {
        do
        {
            float positionX = Random.Range(_xLeftScope, _xRightScope + 1);
            float positionZ = Random.Range(_zUpScope, _zDownScope + 1);

            Vector3 newPosition = new Vector3(positionX, 0, positionZ);
            transform.position = newPosition;
            _position = newPosition;
        }
        while (CheckCollider());
    }

    private bool CheckCollider()
    {
        Collider[] colliders = Physics.OverlapSphere(_position, _colliderRadius, ~LayerMask.GetMask(_maskName));

        return colliders.Length > 0;
    }
}
