using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSymbolRevealerAnimation : GameSymbolRevealer
{
    [SerializeField]
    protected Animator animator;

    protected const string REVEALED_STATE = "SymbolReveal-Revealed";
    protected const string HIDDEN_STATE = "SymbolReveal-Hidden";

    protected const string ANIMATING_STATE = "SymbolReveal-Animation";

    protected const string REVEAL_TRIGGER = "Reveal";

    protected const string HIDE_TRIGGER = "Hide";

    public override bool IsRevealed { get => animator.GetCurrentAnimatorStateInfo(0).IsName(REVEALED_STATE); set {/*Nothing*/} }

    public override bool IsHidden { get => animator.GetCurrentAnimatorStateInfo(0).IsName(HIDDEN_STATE); set {/*Nothing*/} }

    public override void RevealSymbol()
    {
        if (!IsRevealed && !animator.GetCurrentAnimatorStateInfo(0).IsName(ANIMATING_STATE))
        {
            animator.SetTrigger(REVEAL_TRIGGER);
        }
    }

    public override void HideSymbol()
    {
        if (!IsHidden)
        {
            animator.SetTrigger(HIDE_TRIGGER);
        }
    }
}
