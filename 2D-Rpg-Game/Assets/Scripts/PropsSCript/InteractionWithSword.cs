using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class InteractionWithSword : MonoBehaviour {

    Animator anim;
    BoxCollider2D coll2D;

    int animHash = Animator.StringToHash("Move");

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        coll2D = GetComponent<BoxCollider2D>();
        coll2D.isTrigger = true;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword") || collision.CompareTag("Enemy"))
        {
            anim.SetTrigger(animHash);
        }
    }
}
