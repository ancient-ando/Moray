using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Bumper {

    Animator _anim;

    protected override void Awake() {
        base.Awake();
        _anim = GetComponent<Animator>();
    }

    protected override void OnCollisionEnter2D(Collision2D other) {
        base.OnCollisionEnter2D(other);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        Destroy(gameObject);
    }

    protected override void PostPause() {
        base.PostPause();
        _anim.enabled = false;
    }

    protected override void PostResume() {
        base.PostResume();
        _anim.enabled = true;
    }
}
