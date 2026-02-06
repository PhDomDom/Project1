using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    private PlayerAnim player;
    private Animator anim;
    private Skeleton skeleton;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerAnim>();
        skeleton = GetComponentInParent<Skeleton>();
    }

    public void PlayAnim(int value)
    {
        anim.SetInteger("transition", value);
    }

    public void Attack()
    {
        if (!skeleton.isDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.transform.position, radius, playerLayer);

            if (hit != null)
            {
                // Detecta colisão com o player
                player.OnHit();
            }
        }
        
    }

    public void OnHit()
    {
        if (skeleton.currentHealth <= 0)
        {
            skeleton.isDead = true;
            anim.SetTrigger("death");
            Destroy(skeleton.gameObject, 2f);
        }
        else
        {
            anim.SetTrigger("hit");
            skeleton.currentHealth--;
            skeleton.healthBar.fillAmount = skeleton.currentHealth / skeleton.totalHealth;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
