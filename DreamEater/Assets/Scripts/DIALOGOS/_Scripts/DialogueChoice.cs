[System.Serializable]
public class DialogueChoice
{
    public string text;         // Texto que aparece en el botón
    public DialogueNode nextNode;       // Nodo al que lleva esta opción
}