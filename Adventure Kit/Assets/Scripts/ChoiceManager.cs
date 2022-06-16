using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager Instance { get; private set; }

    class Choice
    {
        public readonly string text;
        public readonly string label;

        public Choice(string text, string label)
        {
            this.text = text;
            this.label = label;
        }
    }

    public GameObject panel;
    public GameObject[] buttons;

    private List<Choice> choices = new();
    private ScriptPlayer currentScriptPlayer;

    private void Awake()
    {
        if (Instance != null)
        {
            throw new UnityException("ChoiceManager must exist only once");
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddChoice(string text, string label)
    {
        choices.Add(new Choice(text, label));
    }

    public void Show(ScriptPlayer scriptPlayer)
    {
        currentScriptPlayer = scriptPlayer;
        Refresh();
    }

    public void Clear()
    {
        currentScriptPlayer = null;
        choices.Clear();
        Refresh();
    }

    public void OnSelect(int index)
    {
        Choice choice = choices[index];
        ScriptPlayer scriptPlayer = currentScriptPlayer;
        Clear();
        scriptPlayer.JumpToLabel(choice.label);
        scriptPlayer.Continue();
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
