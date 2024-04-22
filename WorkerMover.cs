using System.Collections;
using UnityEngine;

[RequireComponent(typeof(WorkerAnimator))]
public class WorkerMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _target;
    private WorkerAnimator _animatorWorker;
    private bool _isMoving;
    private Coroutine _moveCoroutine;

    private void Awake()
    {
        _animatorWorker = GetComponent<WorkerAnimator>();
    }

    public void StartMove(Vector3 target)
    {
        if(_moveCoroutine != null)        
            StopCoroutine(_moveCoroutine);        

        _target = target;
        _isMoving = true;
        
        _animatorWorker.Move(_isMoving);     
        _moveCoroutine = StartCoroutine(Move());
    }

    public void StopMove()
    {
        _isMoving = false;
        _animatorWorker.Move(_isMoving);

        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);
    }

    private IEnumerator Move()
    {
        while (_isMoving)
        {
            transform.LookAt(_target);
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
            yield return null;
        }
    }
}
