using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;
    
    
    private Player player;
    private Animator anim;
    private Casting cast;

    private bool hasRolled; // <- nova flag
    private bool isHitting;
    private float recoveryTime = 1.5f;
    private float timeCount;

    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        cast = FindObjectOfType<Casting>();
    }

    void Update()
    {
        OnMove();
        OnRun();
        if (isHitting)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= recoveryTime)
            {
                isHitting = false;
                timeCount = 0f;
            }
        }
        
    }

    #region Movement
    void OnMove()
    {
        // Executa animaçãoo de rolagem apenas uma vez
        if (player.isRolling && !hasRolled)
        {
            anim.SetTrigger("isRoll");
            hasRolled = true;
        }

        // Resetar flag quando a rolagem acabar
        if (!player.isRolling && hasRolled)
        {
            hasRolled = false;
        }

        // Controle de transição
        if (player.direction.sqrMagnitude > 0 && !player.isRolling)
        {
            anim.SetInteger("transition", 1);
        }
        else if (!player.isRolling)
        {
            anim.SetInteger("transition", 0);
        }

        // Direção (flip)
        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        // Ação de cortar a árvore
        if (player.isCutting)
        {
            anim.SetInteger("transition", 3);
        }
        // Ação de cavar o buraco
        if (player.isDigging)
        {
            anim.SetInteger("transition", 4);
        }
        // Ação de soltar a água
        if (player.isWatering)
        {
            anim.SetInteger("transition", 5);
        }
    }

    void OnRun()
    {
        if (player.isRunning && !player.isRolling) // <- evita conflito
        {
            anim.SetInteger("transition", 2);
        }
    }
    #endregion

    #region Attack

    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if (hit != null)
        {
            // Atacou o inimigo
            Debug.Log("Acertou o inimigo");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
    
    #endregion
    
    public void OnCastingStarted()
    {
        anim.SetTrigger("isCasting");
        player.isPaused = true;
    }

    public void OnCastingEndend()
    {
        cast.OnCasting();
        player.isPaused = false;
    }

    public void OnHammeringStarted()
    {
        anim.SetBool("hammering", true);
    }
    public void OnHammeringEndend()
    {
        anim.SetBool("hammering", false);
    }

    public void OnHit()
    {
        if (!isHitting)
        {
            anim.SetTrigger("hit");
            isHitting = true;
        }
    }
}