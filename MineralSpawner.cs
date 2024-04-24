using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class MineralSpawner : MonoBehaviour
{
    [SerializeField] private Mineral _mineral;
    [SerializeField] private float _colliderRadius;
    [SerializeField] private float _spawnCooldown;
    [SerializeField] private MineralDistrebutor _mineralDistrebutor;

    private float _xLeftScope = -45f;
    private float _xRightScope = -7f;
    private float _zUpScope = 45f;
    private float _zDownScope = 6f;
      
    private WaitForSeconds _waitSeconds;
    private Coroutine _coroutineSpawn;
    private string _maskName = "Floor";

    private void Start()
    {
        _waitSeconds = new WaitForSeconds(_spawnCooldown);
        StartSpawn();
    }

    private void StartSpawn()
    {
        if(_coroutineSpawn != null)
            StopCoroutine(_coroutineSpawn);

        bool isWork = true;
        _coroutineSpawn = StartCoroutine(Spawn(isWork));
    }

    private Vector3 FindFreePlace()
    {
        Vector3 newPosition;

        do
        {
            float positionX = Random.Range(_xLeftScope, _xRightScope + 1);
            float positionZ = Random.Range(_zUpScope, _zDownScope + 1);
            newPosition = new Vector3(positionX, 0, positionZ);
            transform.position = newPosition;
        }
        while (DetectCollision());

        return newPosition;
    }

    private bool DetectCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _colliderRadius, ~LayerMask.GetMask(_maskName));

        return colliders.Length > 0;
    }

    private IEnumerator Spawn(bool isWork)
    {
        while(isWork)
        {
            Mineral newMineral = Instantiate(_mineral, FindFreePlace(), Quaternion.identity);
            _mineralDistrebutor.DistributeMineral(newMineral);

            yield return _waitSeconds;
        }
    }
}
