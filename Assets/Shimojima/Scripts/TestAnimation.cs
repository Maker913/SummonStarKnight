using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("Zodiac");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("Damage");
        }
    }
}
