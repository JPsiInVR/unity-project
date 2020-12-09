using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomAnimationStart : MonoBehaviour
{
    private Animator Animator { get; set; }
    private AnimatorStateInfo AnimatorStateInfo { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        AnimatorStateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        Animator.speed = Random.Range(.7f, 1.3f);
        Animator.Play(AnimatorStateInfo.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
