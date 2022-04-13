using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void OnEnable() => _animator = GetComponent<Animator>();

    private const string _moveName = "isMove";

    public void GetMoveAnimation(bool active) => _animator.SetBool(_moveName, active);
}
