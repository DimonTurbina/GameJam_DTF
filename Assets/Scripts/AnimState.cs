using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimState : MonoBehaviour
{
    private Animator _anim;
    public enum enAnimation
    {
        idle,
        move,
        attact
    }
    public enAnimation AnimInstruction;
    // Use this for initialization
    void Start()
    {
        AnimInstruction = enAnimation.idle;
        _anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AnimInstruction == enAnimation.idle)
        {
            _anim.SetBool("idle", true);
            _anim.SetBool("walk", false);
            _anim.SetBool("attack", false);
        }
        if (AnimInstruction == enAnimation.move)
        {
            _anim.SetBool("idle", false);
            _anim.SetBool("walk", true);
            _anim.SetBool("attack", false);
        }
        if (AnimInstruction == enAnimation.attact)
        {
            _anim.SetBool("idle", false);
            _anim.SetBool("walk", false);
            _anim.SetBool("attack", true);
        }
    }
}
