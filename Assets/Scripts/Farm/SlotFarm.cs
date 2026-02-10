using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Audio")] [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip carrotSFX;

    [Header("Components")] [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")] [SerializeField] private int digAmount; // Quantidade de "escava��es"
    [SerializeField] private float waterAmount; // Total de �gua para nascer uma cenoura

    [SerializeField] private bool detecting;

    private int initialDigAmount;
    private float currentWater;

    private bool dugHole;
    private bool plantedCarrot;
    private bool isInterecting;

    [SerializeField] PlayerItems playerItems;

    private void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        initialDigAmount = digAmount;
    }

    private void Update()
    {
        if (dugHole)
        {
            if (detecting)
            {
                currentWater += 0.01f;
            }

            if (currentWater >= waterAmount && !plantedCarrot) // Encheu o total de �gua
            {
                audioSource.PlayOneShot(holeSFX);
                spriteRenderer.sprite = carrot;

                plantedCarrot = true;
            }
            
            if (Input.GetKeyDown(KeyCode.E) && isInterecting && plantedCarrot)
            {
                audioSource.PlayOneShot(carrotSFX);
                spriteRenderer.sprite = hole;
                playerItems.carrots++;
                currentWater = 0f;
            }
        }
    }

    public void OnHit()
    {
        digAmount--;

        if (digAmount <= initialDigAmount / 2)
        {
            spriteRenderer.sprite = hole; // Abre o buraco para plantar (l� ele)
            dugHole = true;
        }

        // if (digAmount <= 0)
        //{
        //     spriteRenderer.sprite = carrot;     // Plantar cenoura
        //  }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dig"))
        {
            OnHit();
        }

        if (collision.CompareTag("Water"))
        {
            detecting = true;
        }

        if (collision.CompareTag("Player"))
        {
            isInterecting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            detecting = false;
        }

        if (collision.CompareTag("Player"))
        {
            isInterecting = false;
        }
    }
}