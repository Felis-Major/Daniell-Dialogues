using Daniell.Runtime.Systems.DialogueNodes;
using UnityEngine;

public class DialogueReader : MonoBehaviour
{
    public DialogueFile dialogueFile;

    private void Awake()
    {
        var currentNode = dialogueFile.GetFirstNode();

        while(currentNode != null)
        {
            if(currentNode is DialogueLineRuntimeNode node) 
            {
                print($"{node.Actor.Name} says: {node.Line}");
            }

            if(currentNode is CallEventRuntimeNode eventNode)
            {
                eventNode.Event.Raise();
            }

            currentNode = currentNode.GetNextNode();
        }
    }
}
