using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ChoiceManager : MonoBehaviour
{
    class Choice
    {
        public readonly string text;
        public readonly Action action;

        public Choice(string text, Action action)
        {
            this.text = text;
            this.action = action;
        }
    }

    public GameObject panel;
    public GameObject[] buttons;

    private readonly List<Choice> choices = new();
    private Action completion;
    
    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    public void AddChoice(string text, Action action)
    {
        choices.Add(new Choice(text, action));
    }

    public void Show(Action completion)
    {
        this.completion = completion;
        Refresh();
    }

    public void Clear()
    {
        choices.Clear();
        completion = null;
        Refresh();
    }

    public void OnSelect(int index)
    {
        Choice choice = choices[index];
        Action completion = this.completion;
        Clear();
        choice.action();
        completion();
    }

    private void Refresh()
    {
        for (int i = 0; i < buttons.Length; ++i)
        {
            GameObject button = buttons[i];

            TextMeshProUGUI textMeshPro = button.GetComponentInChildren<TextMeshProUGUI>();

            if (i < choices.Count)
            {
                Choice choice = choices[i];
                textMeshPro.SetText(choice.text);
                button.SetActive(true);
            }
            else
            {
                textMeshPro.SetText("");
                button.SetActive(false);
            }
        }

        panel.SetActive(choices.Count > 0);
    }
}
