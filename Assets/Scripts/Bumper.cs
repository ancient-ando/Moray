using UnityEngine;

public class Bumper : GameplayMonoBehaviour {

    public float BaseScore = 1000f;
    AudioSource _sfx;

    protected override void Awake() {
        _sfx = GetComponent<AudioSource>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        Rigidbody2D _rb = collision.rigidbody;

        //Multiply score given based on how hard the bumper was hit and make sure
        //the score given is never less than the base score
        float _velocity = 1 + _rb.velocity.magnitude;
        
        float _score = BaseScore * _velocity;
        Blackboard.s_Instance.OnScoreBoard?.Invoke((int)_score);
        _sfx.Play();
    }

}