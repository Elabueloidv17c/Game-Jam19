using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader instance;
    Animator animator;
    bool isFading = false;
    bool isClear = true;


    public bool IsFading
    {
        get
        {
            return isFading;
        }

        private set
        {
            isFading = value;
        }
    }

    public bool IsClear
    {
        get
        {
            return isClear;
        }

        private set
        {
            isClear = value;
        }
    }

    public Animator Animator
    {
        get
        {
            if (null == animator)
                animator = GetComponent<Animator>();
            return animator;
        }
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsFading)
        {
            var state = Animator.GetCurrentAnimatorStateInfo(0);
            if (state.IsName("Black") || state.IsName("Clear"))
            {
                IsFading = false;
                if (state.IsName("Clear")) IsClear = true;
            }
        }
    }

    public void FadeToClear()
    {
        IsFading = true;
        Animator.SetTrigger("FadeIn");
    }

    public void FadeToBlack()
    {
        IsFading = true;
        Animator.SetTrigger("FadeOut");
    }
}
