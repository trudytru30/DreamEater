using System.Collections;
using UnityEngine;
using TMPro;

//Requiere un componente que sea trigger
[RequireComponent(typeof(Collider))]

public class Dialogues : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private InputSystem_Actions _playerMovement;
    
    private bool _dialogueActive;
    private int _currentLine;
    private const float TypeTime = 0.05f;

    private void Start()
    {
        //_playerMovement = FindObjectOfType<PlayerMovement>();
    }
    
    //Interactuar con el npc
    public void Interact()
    {
        if (!_dialogueActive)
        {
            StartDialogue();
            
            if (_playerMovement != null)
            {
                _playerMovement.canMove = false;
            }
        }
        else if (dialogueText.text == dialogueLines[_currentLine])
        {
            NextDialogue();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[_currentLine];
        }
    }
    
    //Inicio del dialogo
    private void StartDialogue()
    {
        _dialogueActive = true;
        dialoguePanel.SetActive(true);
        _currentLine = 0;
        Time.timeScale = 0f;
        StartCoroutine(DisplayDialogue());
    }
    
    //Siguiente linea del dialogo y terminarlo si no hay mas
    private void NextDialogue()
    {
        _currentLine++;
        
        if (_currentLine < dialogueLines.Length)
        {
            StartCoroutine(DisplayDialogue());
        }
        else
        {
            _dialogueActive = false;
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
            
            if (_playerMovement != null)
            {
                _playerMovement.canMove = true;
            }
        }
    }
    
    //Corutina para el texto de los dialogos
    private IEnumerator DisplayDialogue()
    {
        dialogueText.text = string.Empty;
        
        foreach (char c in dialogueLines[_currentLine])
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(TypeTime);
        }
    }
}