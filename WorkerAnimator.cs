using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WorkerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move(bool isMoving)
    {
        _animator.SetBool(BotsAnimatorData.Params.IsMove, isMoving);
    }
}
