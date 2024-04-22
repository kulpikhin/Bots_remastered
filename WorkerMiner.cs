using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WorkerMover))]
[RequireComponent(typeof(Worker))]
public class WorkerMiner : MonoBehaviour
{

    private WorkerMover _movement;
    private Base _curentBase;
    private Mineral _mineralTarget;
    private bool _isMining;
    private bool _isBringing;
    private Transform catchMineral;

    public event UnityAction MineralPuted;

    private void Start()
    {
        catchMineral = transform.GetChild(0);
        _movement = GetComponent<WorkerMover>();
    }

    public void SetCurrentBase(Base curentBase)
    {
        _curentBase = curentBase;
    }

    public void MineMineral(Mineral mineralTarget)
    {
        _isMining = true;
        _mineralTarget = mineralTarget;
        _movement.StartMove(mineralTarget.transform.position);
    }

    private void OnTriggerEnter(Collider collider)
    {
        SelectAction(collider);
    }

    private void SelectAction(Collider collider)
    {
        if (_isMining && collider.gameObject == _mineralTarget.gameObject)
        {
            BringMineral();
        }
        else if (_isBringing && collider.gameObject == _curentBase.gameObject)
        {
            PutMineral();
        }
    }

    private void BringMineral()
    {
        catchMineral.gameObject.SetActive(true);
        _isMining = false;
        _isBringing = true;
        Destroy(_mineralTarget.gameObject);
        _mineralTarget = null;
        _movement.StopMove();
        _movement.StartMove(_curentBase.transform.position);
    }

    private void PutMineral()
    {
        catchMineral.gameObject.SetActive(false);
        _isBringing = false;
        _movement.StopMove();
        MineralPuted?.Invoke();
    }
}
