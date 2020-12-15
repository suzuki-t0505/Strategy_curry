using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void WalkAnima(float moveInput)
    {
        animator.SetFloat("Speed", moveInput);
    }

    public void StopWalkAnima()
    {
        animator.SetFloat("Speed", 0f);
    }

    public void DamageAnima()
    {
        animator.SetTrigger("Damage");
    }

    public void DethAnima()
    {
        animator.SetTrigger("Deth");
    }

    public void RevivalAnima()
    {
        animator.SetTrigger("Revival");
    }

    public void Attak1_Skill()
    {
        animator.SetTrigger("Attack");
    }

    public void Attack2_Skill()
    {
        animator.SetTrigger("Attack2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
