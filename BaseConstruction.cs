using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Worker))]
[RequireComponent (typeof(WorkerMover))]
public class BaseConstruction : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;

    private WorkerMover _movement;
    private bool _isBuilding;
    private Flag _flag;

    public event UnityAction<Base> ConstructionStarted;

    private void Start()
    {
        _movement = GetComponent<WorkerMover>();
    }

    public void SendConstruct(Flag flag)
    {
        _flag = flag;
        _isBuilding = true;
        _movement.StartMove(_flag.transform.position);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (_flag)
        {
            if (_isBuilding && collider.gameObject == _flag.gameObject)
            {
                StartConstruct();
                _flag = null;
            }
        }
    }

    private void StartConstruct()
    {
        _isBuilding = false;
        _movement.StopMove();        
        Base newBase = Instantiate(_basePrefab, _flag.transform.position, Quaternion.identity);
        Destroy(_flag.gameObject);
        ConstructionStarted?.Invoke(newBase);
        newBase.WorkerManagment.IdentifySecondBase();
    }
}
