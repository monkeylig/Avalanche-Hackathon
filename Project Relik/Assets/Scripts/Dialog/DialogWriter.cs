using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelsoftGames.PixelUI;
using TMPro;

public class DialogWriter : MonoBehaviour
{
    [SerializeField]
    private UITypewriter typewriter = null;
    [SerializeField]
    private DialogueScript script = null;
    [SerializeField]
    private int dialogueLine = 0;
  
    private SpriteRenderer dialogueBubble = null;
    private int currentLine = 0;

    private int CurrentLine
    {
        get { return currentLine; }
        set
        {
            currentLine = value;
            dialogueLine = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        dialogueBubble = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        if (typewriter && dialogueBubble)
        {
            var textMesh = typewriter.GetComponent<RectTransform>();
            dialogueBubble.size = new Vector2(textMesh.rect.width + 0.5f, dialogueBubble.size.y);
        }

        if (!script)
        {
            Debug.LogWarning("Dialog writer has no script!");
            return;
        }

        if (!typewriter)
        {
            Debug.LogWarning("Dialog writer has no typewriter!");
            return;
        }

        if (dialogueLine != currentLine && dialogueLine >= 0)
        {
            typewriter.SetText(script.Lines[dialogueLine]);
            currentLine = dialogueLine;
        }
    }

    public void WriteNextLine()
    {
        if (!script)
        {
            Debug.LogWarning("Dialog writer has no script!");
            return;
        }

        if (!typewriter)
        {
            Debug.LogWarning("Dialog writer has no typewriter!");
            return;
        }

        if (CurrentLine == script.Lines.Count)
        {
            typewriter.gameObject.SetActive(false);
            return;
        }

        typewriter.gameObject.SetActive(true);

        typewriter.SetText(script.Lines[CurrentLine]);
        CurrentLine++;
    }

    public void HideLine()
    {
        if (!typewriter)
        {
            Debug.LogWarning("Dialog writer has no typewriter!");
            return;
        }

        typewriter.gameObject.SetActive(false);
    }
}
