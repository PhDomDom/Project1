using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Player player;
    private Animator anim;
    private Casting cast;

    private bool hasRolled; // <- nova flag

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
    }

    #region Movement
    void OnMove()
    {
        // Executa animação de rolagem apenas uma vez
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
}