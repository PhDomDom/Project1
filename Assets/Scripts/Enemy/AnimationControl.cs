using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;
    
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnim(int value)
    {
        anim.SetInteger("transition", value);
    }

    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

        if (hit != null)
        {
            // Detecta colisão com o player
            Debug.Log("bateu no player");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
