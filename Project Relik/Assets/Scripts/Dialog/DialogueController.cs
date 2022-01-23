using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLine(string agentName)
    {
        DialogWriter writer = FindWriter(agentName);

        if (writer)
        {
            writer.WriteNextLine();
        }
    }

    public void HideLine(string agentName)
    {
        DialogWriter writer = FindWriter(agentName);

        if (writer)
        {
            writer.HideLine();
        }
    }

    private DialogWriter FindWriter(string agentName)
    {
        Transform childTransform = transform.Find(agentName);

        DialogWriter writer = null;
        if (childTransform)
        {
            writer = childTransform.GetComponent<DialogWriter>();
        }

        return writer;
    }
}
