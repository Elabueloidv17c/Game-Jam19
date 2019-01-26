using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LoadTextOption { EndWithArrow, EndWithOptions, EndWithoutArrow };

[RequireComponent(typeof(Text))]
public class TextTyper : MonoBehaviour
{
    public bool IsTypingText
    {
        get;
        private set;
    }
    public int textSpeed = 50;
    public LoadTextOption loadTextOption;
    public GameObject Arrow;
    private TextParser.TextNode[] parsedText;
    private Stack<string> footerStack;
    private string textCopy;
    private Text textGUI;
    public bool NextEnabled;

    void Awake()
    {
        textGUI = GetComponent<Text>();
        StartCoroutine(TypeText());
    }

    private void Update()
    {
        if (ScreenFader.instance == null || ScreenFader.instance.IsFading) return;
        if(NextEnabled && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            if(IsTypingText)
            {
                StopAllCoroutines();
                EndTextTyping();
            }
            else if(loadTextOption.IsAnyOf(LoadTextOption.EndWithArrow, LoadTextOption.EndWithoutArrow))
            {
                NextEnabled = false;
                Arrow.SetActive(false);
                SendMessageUpwards("TextTyperNext");
            }
        }
    }

    public void ShowText(string value)
    {
        StopAllCoroutines();
        textGUI.text = value;
        IsTypingText = true;
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        Arrow.SetActive(false);
        textCopy = textGUI.text;
        parsedText = TextParser.Parse(textGUI.text);
        textGUI.text = "";
        footerStack = new Stack<string>();

        while (ScreenFader.instance == null || ScreenFader.instance.IsFading || ScreenFader.instance.Alpha == 1f)
            yield return null;

        foreach (var child in parsedText)
            yield return TypeNode(child);

        EndTextTyping();
    }

    IEnumerator TypeNode(TextParser.TextNode node)
    {
        if(node.children != null)
        {
            textGUI.text += node.head;
            footerStack.Push(node.foot);
            foreach (var child in node.children)
                yield return TypeNode(child);
            textGUI.text += footerStack.Pop();
        }
        else
        {
            yield return TypeString(node.head);
        }
    }

    IEnumerator TypeString(string text)
    {
        foreach(var c in text)
        {
            textGUI.text += c;
            var localcopy = textGUI.text;
            textGUI.text += string.Join("",footerStack.ToArray());
            yield return new WaitForSecondsRealtime(1 / textSpeed);
            textGUI.text = localcopy;
        }
    }

    void EndTextTyping()
    {
        textGUI.text = textCopy;
        IsTypingText = false;
        switch (loadTextOption)
        {
            case LoadTextOption.EndWithArrow:
                Arrow.SetActive(true);
                break;
            case LoadTextOption.EndWithOptions:
                SendMessageUpwards("ShowButtons");
                break;
            case LoadTextOption.EndWithoutArrow:
                Arrow.SetActive(false);
                break;
            default:
                break;
        }
    }
}
