using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackController : MonoBehaviour
{
    public GameObject CharacterAnimator;
    public BoxCollider2D box;
    public float attackDuration;
    public float attackStartDuration;
    private bool attacking = false;
    void Start()
    { 
        Physics.IgnoreLayerCollision(11,9);
        box.enabled = false;
    }
    public void EnableDamage()
    {
        if (attacking) return;
        StartCoroutine("DisableDamage");
        CharacterAnimator.GetComponent<Animator>().Play("Attack");
        CharacterAnimator.GetComponent<Animator>().SetBool("Jumping", false);
        }
    IEnumerator DisableDamage()
    {
        yield return new WaitForSeconds(attackStartDuration);
        transform.parent.gameObject.layer = 11;
        attacking = true;
        box.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        transform.parent.gameObject.layer = 0;
        attacking = false;
        box.enabled = false;
    }
}
