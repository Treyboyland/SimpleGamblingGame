using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSymbol : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Animator animator;

    public bool IsAnimating => animator.GetCurrentAnimatorStateInfo(0).IsName("_Symbol-Animate");

    public bool IsFinished => animator.GetCurrentAnimatorStateInfo(0).IsName("_Symbol-Finished");

    public bool IsInitial => animator.GetCurrentAnimatorStateInfo(0).IsName("_Symbol-Initial");

    const string ANIMATE_TRIGGER = "Animate";
    const string RESET_TRIGGER = "Reset";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAnimation()
    {
        if (!IsAnimating)
        {
            animator.SetTrigger(ANIMATE_TRIGGER);
        }
    }

    public void ResetAnimation()
    {
        if (IsAnimating || IsFinished)
        {
            animator.SetTrigger(RESET_TRIGGER);
        }
    }

    public void SetSymbol(SymbolData symbolData)
    {
        animator.ResetTrigger(ANIMATE_TRIGGER);
        animator.ResetTrigger(RESET_TRIGGER);
        animator.runtimeAnimatorController = symbolData.SymbolAnimations;
        spriteRenderer.sprite = symbolData.SymbolSprite;
        spriteRenderer.color = symbolData.SymbolColor;
    }
}
