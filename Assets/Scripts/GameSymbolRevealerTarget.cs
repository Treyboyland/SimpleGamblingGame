using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSymbolRevealerTarget : GameSymbolRevealerAnimation
{
    [SerializeField]
    AnimationCurve moveCurve;

    [SerializeField]
    protected float secondsToReveal;

    float elapsed = 0;

    public SymbolTargetPoint TargetPoint;

    public Transform OriginPosition;

    bool shouldMove = false;

    public override bool IsRevealed
    {
        get
        {
            if (animator)
            {
                return animator.GetCurrentAnimatorStateInfo(0).IsName(REVEALED_STATE) && elapsed >= secondsToReveal;
            }
            else
            {
                return elapsed >= secondsToReveal;
            }
        }
        set {/*Nope*/}
    }
    public override bool IsHidden
    {
        get
        {
            if (animator)
            {
                return animator.GetCurrentAnimatorStateInfo(0).IsName(HIDDEN_STATE) && elapsed <= 0;
            }
            else
            {
                return elapsed <= 0;
            }
        }
        set {/*Nope*/}
    }

    public override void HideSymbol()
    {
        if (!IsHidden)
        {
            if (animator)
            {
                animator.SetTrigger(HIDE_TRIGGER);
            }

            shouldMove = false;
            elapsed = 0;
        }
    }

    public override void RevealSymbol()
    {
        if (!IsRevealed && !animator.GetCurrentAnimatorStateInfo(0).IsName(ANIMATING_STATE))
        {
            shouldMove = true;

            if (animator)
            {
                animator.SetTrigger(REVEAL_TRIGGER);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            elapsed += Time.deltaTime;
            UpdatePosition(elapsed);
        }
    }

    void UpdatePosition(float elapsed)
    {
        float progress = moveCurve.Evaluate(elapsed / secondsToReveal);
        transform.position = Vector3.Lerp(OriginPosition.position, TargetPoint.transform.position, progress);
    }
}
