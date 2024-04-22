using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (ManagmentWorkers))]
public class Base : MonoBehaviour
{
    [SerializeField] private Worker _worker;
    [SerializeField] private Flag _flagPrefab;

    private int _mineralsCount = 0;
    private int _costWorker = 3;
    private int _costConstruction = 5;
    private int _maximumWorkers = 6;

    private bool _isFlagPlanted;
    private Flag _flag;
    private Worker _builder;
    private MineralDistrebutor _mineralDistrebutor;

    public ManagmentWorkers WorkerManagment { get; private set; }

    private void Awake()
    {
        _mineralDistrebutor = FindObjectOfType<MineralDistrebutor>();
        WorkerManagment = GetComponent<ManagmentWorkers>();
    }

    private void Start()
    {
        _mineralsCount = 0;        
        Initializate();              
    }

    public void SetFlagPosition(Vector3 flagPosition)
    {
        if (_isFlagPlanted)
        {
            _flag.transform.position = flagPosition;
        }
        else
        {
            _flag = Instantiate(_flagPrefab, flagPosition, Quaternion.identity);
            _isFlagPlanted = true;
        }
    }

    public void IdentifyBuilder(Worker builder)
    {
        _builder = builder;
        WorkerManagment.AddWorker(_builder);
    }

    public void AddMineral(Worker worker)
    {
        _mineralsCount++;
        WorkerManagment.AddWorker(worker);
        SpendMineral();
    }

    private void Initializate()
    {
        _mineralDistrebutor.AddBase(this);
        WorkerManagment.InitializateBase(this);
    }

    private void SpendMineral()
    {
        if (_isFlagPlanted)
        {
            BuyConstruction();
        }
        else
        {
            BuyWorker();
        }
    }

    private void BuyWorker()
    {
        if (_costWorker <= _mineralsCount && _maximumWorkers > WorkerManagment.WorkersCount)
        {
            WorkerManagment.CreatWorker();
            _mineralsCount -= _costWorker;
        }
    }

    private void BuyConstruction()
    {
        if (_costConstruction <= _mineralsCount)
        {
            _isFlagPlanted = false;
            WorkerManagment.SendConstuct(_flag);
            _mineralsCount -= _costConstruction;
        }
    }
}
