using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    // Variables de la UI 
    public TextMeshProUGUI speakerText; 
    public TextMeshProUGUI lineText; 
    public TMP_InputField nameInputField; 
    public GameObject choiceButtonPrefab; 
    public Transform choiceContainer;
    
    private System.Action<DialogueNode> _callback;
    private Coroutine _typeCoroutine;
    private string _fullText;
    private bool _isTyping;
    private bool _hasMultipleChoices;
    private bool _isInputActive;
    

    // Callback pendiente para mostrar el input de nombre al terminar la linea 
    private System.Action<string> _pendingNameCallback; 

    // Awake para auto-asignar referencias si faltan 
    private void Awake() 
    { 
        // Intentar auto-asignar referencias si no están puestas en el inspector 
        if (lineText == null) 
        { 
            lineText = GetComponentInChildren<TextMeshProUGUI>(true); 
            if (lineText != null && lineText.name.ToLower().Contains("speaker") && speakerText == null) 
            { 
                speakerText = lineText; 
                lineText = null; 
            } 
        } 
        if (speakerText == null) 
        { 
            // Intentar encontrar por nombre 
            var tmps = GetComponentsInChildren<TextMeshProUGUI>(true); 
            if (tmps != null) 
            { 
                string names = string.Join(", ", System.Array.ConvertAll(tmps, t => t.name)); 
            } 
            if (tmps != null) foreach (var t in tmps) 
            { 
                var n = t.name.ToLower(); 
                if (n.Contains("speaker") || n.Contains("name")) 
                { 
                    speakerText = t; 
                    break; 
                } 
            } 
        } 
        if (lineText == null) 
        { 
            var tmps = GetComponentsInChildren<TextMeshProUGUI>(true); 
            if (tmps.Length == 1) 
            { 
                if (tmps[0] != speakerText) lineText = tmps[0]; 
            } 
            else 
            { 
                foreach (var t in tmps) 
                { 
                    var n = t.name.ToLower(); 
                    if (n.Contains("line") || n.Contains("dialog") || n.Contains("text")) 
                    { 
                        if (t == speakerText) continue; 
                        lineText = t; 
                        break; 
                    } 
                } 
            } 
        } 
        if (nameInputField == null) 
        { 
            nameInputField = GetComponentInChildren<TMP_InputField>(true); 
        } 
        if (choiceContainer == null) 
        { 
            // Buscar un transform llamado "Choices" 
            var trs = GetComponentsInChildren<Transform>(true); 
            foreach (var tr in trs) 
            { 
                var n = tr.name.ToLower(); 
                if (n.Contains("choice") || n.Contains("choices") || n.Contains("options")) 
                { 
                    choiceContainer = tr; 
                    break; 
                } 
            } 
        } 
        // Asegurar que el campo de nombre esté oculto al iniciar 
        if (nameInputField != null) nameInputField.gameObject.SetActive(false); 
    }
    
    // Rellenar texto del diálogo
    public void AdvanceDialogue()
    {
        if (_isTyping)
        {
            if(_typeCoroutine != null) StopCoroutine(_typeCoroutine);
            if(lineText) lineText.text = _fullText;
            _isTyping = false;
            return;
        }
        
        // Si hay múltiples opciones no avanza
        if (_hasMultipleChoices)
            return;

        // Si espera input de nombre, no avanza
        if (_isInputActive)
            return;
        _callback?.Invoke(null);
    }

    // Mostrar texto 
    public void ShowLine(string speaker, string line, List<DialogueChoice> choices, 
        System.Action<DialogueNode> onChoiceSelected, System.Action<string> onNameRequested = null) 
    { 
        if (nameInputField) nameInputField.gameObject.SetActive(false);
        _fullText = line;
        _hasMultipleChoices = choices != null && choices.Count > 1;
        _isInputActive = false;
        _callback = onChoiceSelected; 
        if (speakerText) speakerText.text = speaker; 
        bool startedTyping = false;

        // Activar la UI primero para que los componentes (lineText) puedan renderizar y recibir texto 
        gameObject.SetActive(true); 
        if (lineText) 
        { 
            if (_typeCoroutine != null) StopCoroutine(_typeCoroutine); 
            _typeCoroutine = StartCoroutine(TypeText(_fullText)); 
            startedTyping = true; 
        } 

        // Borrar botones anteriores 
        if (choiceContainer) 
        { 
            for (int i = choiceContainer.childCount - 1; i >= 0; i--) 
            { 
                var child = choiceContainer.GetChild(i); 
                if (Application.isPlaying) Destroy(child.gameObject); 
                else DestroyImmediate(child.gameObject); 
            } 
        } 

        // Si no hay opciones, avanzar con la tecla E
        if (choices == null || choices.Count == 0)
        {
            if (onNameRequested != null)
            {
                _pendingNameCallback = onNameRequested;
                if(!startedTyping) StartCoroutine(WaitForTextThenShowName());
            }

            return;
        } 

        // Crear nuevos botones 
        foreach (var choice in choices) 
        { 
            var capturedNext = choice.nextNode; 
            var capturedText = choice.text; 
            if (!choiceButtonPrefab || !choiceContainer) continue; 
            var btn = Instantiate(choiceButtonPrefab, choiceContainer); 
            var tmp = btn.GetComponentInChildren<TextMeshProUGUI>(); 
            if (tmp) tmp.text = capturedText; 
            var button = btn.GetComponent<Button>(); 
            if (button) 
            { 
                button.onClick.RemoveAllListeners(); 
                button.onClick.AddListener(() => HandleChoiceSelection(capturedNext)); 
            } 
        } 

        if (onNameRequested == null) return; 
        _pendingNameCallback = onNameRequested; 
        if (!startedTyping) { StartCoroutine(WaitForTextThenShowName()); } 
    } 

    // Efecto de tipeo de texto 
    private IEnumerator TypeText(string textToType) 
    { 
        _isTyping = true; 
        if (lineText) lineText.text = ""; 
        foreach (char letter in textToType)
        { 
            if (lineText) lineText.text += letter; 
            yield return new WaitForSeconds(0.05f); 
        } 
        _isTyping = false; 
        if (_pendingNameCallback != null) 
        { 
            var cb = _pendingNameCallback; 
            _pendingNameCallback = null; 
            ShowNameInput(cb); 
        } 
    } 

    private void HandleChoiceSelection(DialogueNode nextNode) 
    { 
        _hasMultipleChoices = false;
        _isInputActive = false;
        if (_isTyping) 
        { 
            if (_typeCoroutine != null) StopCoroutine(_typeCoroutine); 
            if (lineText) lineText.text = _fullText; 
            _isTyping = false; 
            if (_pendingNameCallback == null) return; 
            var cb = _pendingNameCallback; 
            _pendingNameCallback = null; 
            ShowNameInput(cb); 
        } 
        else { _callback?.Invoke(nextNode); } 
    } 

    private void ShowNameInput(System.Action<string> onNameEntered) 
    { 
        if (!nameInputField) return;
        
        _isInputActive = true;
        
        if (_isTyping) { _pendingNameCallback = onNameEntered; return; }
        
        nameInputField.gameObject.SetActive(true); 
        nameInputField.text = ""; 
        nameInputField.Select();
        nameInputField.ActivateInputField();
        nameInputField.onEndEdit.RemoveAllListeners(); 
        nameInputField.onEndEdit.AddListener((value) => 
        { 
            if (string.IsNullOrEmpty(value)) return; 
            _isInputActive = false;
            onNameEntered(value);
            nameInputField.gameObject.SetActive(false); 
        });
    } 

    public void Hide() 
    { 
        if (nameInputField) nameInputField.gameObject.SetActive(false); 
        gameObject.SetActive(false); 
    } 

    public void ResetUI() 
    { 
        if (_typeCoroutine != null) { StopCoroutine(_typeCoroutine); _typeCoroutine = null; } 
        _isTyping = false; _fullText = null; _callback = null; _pendingNameCallback = null; 
        if (lineText) lineText.text = string.Empty; 
        if (speakerText) speakerText.text = string.Empty; 
        if (nameInputField) { nameInputField.onEndEdit.RemoveAllListeners(); nameInputField.text = string.Empty; nameInputField.gameObject.SetActive(false); } 
        if (!choiceContainer) return; 
        for (int i = choiceContainer.childCount - 1; i >= 0; i--) 
        { 
            var child = choiceContainer.GetChild(i); 
            if (Application.isPlaying) Destroy(child.gameObject); 
            else DestroyImmediate(child.gameObject); 
        } 
    } 

    private IEnumerator WaitForTextThenShowName() 
    { 
        if (!lineText) { yield return new WaitForSeconds(0.25f); } 
        else 
        { 
            int maxFrames = 10; 
            int count = 0; 
            while (count < maxFrames) 
            { 
                if (!string.IsNullOrEmpty(lineText.text)) break; 
                count++; 
                yield return null; 
            } 
        } 
        if (_pendingNameCallback == null) yield break; 
        var cb = _pendingNameCallback; 
        _pendingNameCallback = null; 
        ShowNameInput(cb); 
    }
}