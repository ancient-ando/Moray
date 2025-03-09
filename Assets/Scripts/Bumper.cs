using UnityEngine;

public class Bumper : GameplayMonoBehaviour {

    public float Force = 10;

    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        Vector2 _normal = collision.GetContact(0).normal;
        
        //Rigidbody2D _rb = collision.rigidbody;
        //_rb.AddRelativeForce(_normal * (Blackboard.s_Instance.BallSpeed * -Force), ForceMode2D.Impulse);
    }

}