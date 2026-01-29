using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj;    // Janela do Dialogo
    public Image profileSprite;      // Sprite do perfil
    public Text speechText;         // Texto da fala
    public Text actorNameText;     //  Nome do NPC

    [Header("Settings")]
    public float typingSpeed;   // Velocidade do texto/fala

    // Variáveis de Controle
    public bool isShowing;  // Se a janela está visível
    private int index;  // Index das falas
    private string[] setences;

    public static DialogueControl instance;

    // Awake é chamado antes de todos os Start() na hierarquia de execução de scripts
    public void Awake()
    {
        instance = this;
    }

    // É chamado ao inicializar
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in setences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Pular para a próxima frase/fala
    public void NextSentence()
    {
        if(speechText.text == setences[index])
        {
            if(index < setences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else // Quando terminam os textos/falas/frases
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                setences = null;
                isShowing = false;
            }
        }
    }

    // Chamar a fala do NPC
    public void Speech(string[] txt)
    {
        if (!isShowing)
        {
            dialogueObj.SetActive(true);
            setences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }
}
