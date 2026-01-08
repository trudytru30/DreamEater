using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Dialogue/Node")]   //Permitir crear nodos en el editor
public class DialogueNode : ScriptableObject
{
    public string Speaker;      // Nombre del personaje del diálogo
    [TextArea]
    public string Line;     // Texto del diálogo
    public List<DialogueChoice> Choices;    // Opciones a elegir del jugador
    public DialogueNode NextNode;   // Siguiente nodo tras la respuesta del jugador
}