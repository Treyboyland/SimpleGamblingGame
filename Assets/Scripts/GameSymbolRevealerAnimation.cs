using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSymbolRevealerAnimation : GameSymbolRevealer
{
    [SerializeField]
    Animator animator;

    const string REVEALED_STATE = "SymbolReveal-Revealed";
    const string HIDDEN_STATE = "SymbolReveal-Hidden";

    const string REVEAL_TRIGGER = "Reveal";

    const string HIDE_TRIGGER = "Hide";

    public override bool IsRevealed { get => animator.GetCurrentAnimatorStateInfo(0).IsName(REVEALED_STATE); set {/*Nothing*/} }

    public override bool IsHidden { get => animator.GetCurrentAnimatorStateInfo(0).IsName(HIDDEN_STATE); set {/*Nothing*/} }

    public override void RevealSymbol()
    {
        if (!IsRevealed)
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
