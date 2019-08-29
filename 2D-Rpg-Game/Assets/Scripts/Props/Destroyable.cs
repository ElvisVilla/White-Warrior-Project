using UnityEngine;

public class Destroyable : MonoBehaviour {

    public float delay;
    Animator anim;
    BoxCollider2D coll2D;
    GameObject enemyObj;
    CapsuleCollider2D enemyColl2D;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        coll2D = GetComponent<BoxCollider2D>();
        enemyObj = GameObject.FindGameObjectWithTag("Enemy");
        enemyColl2D = enemyObj.GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        Physics2D.IgnoreCollision(coll2D, enemyColl2D);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Sword")
        {
            //Mover la caja para darle ese efecto de impulso de daño
            anim.SetBool("Break", true);
            Destroy(gameObject, delay);
            
        }

    }
}
