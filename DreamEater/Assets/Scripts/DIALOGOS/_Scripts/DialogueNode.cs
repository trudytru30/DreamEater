using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Dialogue/Node")]   //Permitir crear nodos en el editor
public class DialogueNode : ScriptableObject
{
    public string speaker;      // Nombre del personaje del diálogo
    [TextArea]
    public string line;     // Texto del diálogo
    public List<DialogueChoice> choices;    // Opciones a elegir del jugador
    public DialogueNode nextNode;   // Siguiente nodo tras la respuesta del jugador (vacio = termina diálogo)
}