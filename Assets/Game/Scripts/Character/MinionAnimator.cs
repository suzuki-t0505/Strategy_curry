using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAnimator : MonoBehaviour
{
    private MinionController minionController;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        minionController = GetComponent<MinionController>();
        animator = GetComponent<Animator>();
    }

    public void MoveAnime(string animeName, float speed)
    {
        animator.SetFloat(animeName, /*meshAgent.desiredVelocity.magnitude*/speed);
    }

    public void AnimeTrigger(string animeName)
    {
        animator.SetTrigger(animeName);
    }

    public void AnimeRest(string animeName)
    {
        animator.ResetTrigger(animeName);
    }
}
