using System.Collections.Generic;
using UnityEngine;

public class ManagmentWorkers : MonoBehaviour
{
    [SerializeField] private Worker _worker;

    private Queue<Worker> _freeWorkers;
    private Base _currentBase;
    private int _startWorkersCount;
    private Vector3 _spawnPosition;
    private Vector3 _lengthToSpawn;
    private bool _isFirstBase;

    public int WorkersCount { get; private set; }

    private void Awake()
    {
        _freeWorkers = new Queue<Worker>();
        _isFirstBase = true;
        _startWorkersCount = 3;
        _lengthToSpawn = new Vector3(3, 0, 0);
    }

    public void SendConstuct(Flag flag)
    {
        Worker worker = _freeWorkers.Dequeue();
        worker.StartConstruct(flag);
    }

    public void SendMining(Mineral mineral)
    {
        Worker worker = _freeWorkers.Dequeue();
        worker.StartMining(mineral);
    }

    public bool CheckFreeWorker()
    {
        return _freeWorkers.Count > 0;
    }

    public void InitializateBase(Base currentBase)
    {
        _currentBase = currentBase;
        SetSpawnPosition();
        FillStartWorkers();
    }

    public void AddWorker(Worker worker)
    {
        _freeWorkers.Enqueue(worker);
    }

    public void CreatWorker()
    {
        Worker worker = Instantiate(_worker, _spawnPosition, Quaternion.identity);
        worker.SetNewBase(_currentBase);
        AddWorker(worker);
        WorkersCount++;
    }

    private void SetSpawnPosition()
    {
        _spawnPosition = _currentBase.transform.position + _lengthToSpawn;
        _spawnPosition.y = 0;
    }

    private void FillStartWorkers()
    {
        if (_isFirstBase)
        {
            for (int i = 0; i < _startWorkersCount; i++)
            {
                CreatWorker();
            }
        }
    }

    public void IdentifySecondBase()
    {
        _isFirstBase = false;
    }
}

