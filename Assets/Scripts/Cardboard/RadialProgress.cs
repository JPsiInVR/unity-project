using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]
public class RadialProgress : MonoBehaviour
{
    public static RadialProgress Instance { get; private set; }

    public float Progress { 
        get => progress; 
        set {

            if(value >= 1f)
            {
                PlayFadeOut();
            }

            progress = value; 
            Image.fillAmount = progress;
        }
    }

    [SerializeField]
    private List<Sprite> sprites;
    [SerializeField]
    private float animationDelay;
    [SerializeField]
    private string fadeAnimationName;
    [SerializeField]
    private string defaultAnimationName;

    private Animator animator;
    private WaitForSeconds waitTime;
    private float progress;

    public Image Image { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        waitTime = new WaitForSeconds(animationDelay);
    }

    private void Start()
    {
        Image = GetComponent<Image>();
        animator = GetComponent<Animator>();
        StartCoroutine(AnimateSprite());
    }

    private IEnumerator AnimateSprite()
    {
        while (true)
        {
            yield return waitTime;
            Image.sprite = sprites[Random.Range(0, sprites.Count)];
        }
    }

    public void PlayFadeOut()
    {
        animator.Play(fadeAnimationName);
    }

    public void PlayDefault()
    {
        animator.Play(defaultAnimationName);
    }


    //Reciever of animation event
    public void ResetProgress()
    {
        Progress = 0;
    }
}
