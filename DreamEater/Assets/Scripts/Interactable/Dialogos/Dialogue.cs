using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private PlayerMovement _playerMovement;
    
    private bool _dialogueActive;
    private int _currentLine;
    private readonly float _typeTime = 0.05f;

    private void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

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

    private void StartDialogue()
    {
        _dialogueActive = true;
        dialoguePanel.SetActive(true);
        _currentLine = 0;
        Time.timeScale = 0f;
        StartCoroutine(DisplayDialogue());
    }
    
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
    
    //Corutina para los diálogos
    private IEnumerator DisplayDialogue()
    {
        dialogueText.text = string.Empty;
        
        foreach (char c in dialogueLines[_currentLine])
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(_typeTime);
        }
    }
}