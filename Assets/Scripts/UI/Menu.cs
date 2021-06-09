using System.Collections;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    public static readonly string EnabledState = "Enabled";
    public static readonly string DisabledState = "Disabled";
    public static readonly string InitialState = "None";

    public MenuType Type { get => type; private set => type = value; }
    public string TargetState { get; protected set; }


    [SerializeField]
    private MenuType type;

    [SerializeField]
    private bool useAnimation;

    private Animator animator;

    public void Animate(bool enable)
    {
        if (useAnimation)
        {
            animator.SetBool("Enabled", enable);

            StartCoroutine(AwaitAnimation(enable));
        }
        else if (!enable)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator AwaitAnimation(bool enable)
    {
        TargetState = enable ? EnabledState : DisabledState;


        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(TargetState))
        {
            yield return null;
        }

        //If it hits 1 it means that animation finished
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        TargetState = InitialState;

        if (!enable)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnEnable()
    {
        if (useAnimation)
        {
            animator = GetComponent<Animator>();

            if (!animator)
            {
                Debug.LogWarning("Komponent animator nie jest umieszczony na obiekcie, który chcesz animowaæ");
            }
        }
    }
}