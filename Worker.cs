using UnityEngine;

[RequireComponent (typeof(WorkerMiner))]
[RequireComponent (typeof(BaseConstruction))]
public class Worker : MonoBehaviour
{
    private WorkerMiner _workerMiner;
    private Mineral _targetMineral;
    private BaseConstruction _baseConstruction;
    private Base _currentBase;

    private void Awake()
    {
        _workerMiner = GetComponent<WorkerMiner>();
        _baseConstruction = GetComponent<BaseConstruction>();
    }

    private void OnEnable()
    {
        _baseConstruction.ConstructionEnd += EndConstruct;
        _workerMiner.MineralPuted += PutMineral;
    }

    private void OnDisable()
    {
        _baseConstruction.ConstructionEnd -= EndConstruct;
        _workerMiner.MineralPuted -= PutMineral;
    }

    public void StartMining(Mineral targetMineral)
    {
        _targetMineral = targetMineral;
        _workerMiner.MineMineral(_targetMineral);
    }

    public void StartConstruct(Flag flag)
    {        
        _baseConstruction.SendConstruct(flag);
    }

    public void SetNewBase(Base newBase)
    {
        _currentBase = newBase;        
        _workerMiner.SetCurrentBase(_currentBase);
    }

    private void EndConstruct(Base newBase)
    {
        SetNewBase(newBase);
        _currentBase.IdentifyBuilder(this);
    }

    private void PutMineral()
    {
        _currentBase.GetInQueue(this);
        _currentBase.GetMineral(this);
    }
}
