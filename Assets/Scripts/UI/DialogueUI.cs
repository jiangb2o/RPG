using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    // 单例模式
    public static DialogueUI Instance { get; private set; }

    // 获取文本组件 说话者 内容
    private TextMeshProUGUI speakerText;
    private TextMeshProUGUI contentText;

    // continue
    private Button continueButton;

    private int contentIndex = 0;
    private List<string> contentList;

    private GameObject ui;

    private Action OnDialogueEnd;

    private void Awake()
    {
        // 单例模式, 如果出现另外的Object则销毁
        // 实例已存在且不为此物体脚本
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // transform.Find() 查找此Object的孩子Object 并获取所需的组件
        speakerText = transform.Find("UI/SpeakerText").GetComponent<TextMeshProUGUI>();
        contentText = transform.Find("UI/ContentText").GetComponent<TextMeshProUGUI>();
        continueButton = transform.Find("UI/ContinueButton").GetComponent<Button>();
        // 监听ContinueButton是否被点击
        continueButton.onClick.AddListener(this.OnContinueButtonClick);

        ui = transform.Find("UI").gameObject;
        Hide();
    }

    public void Show()
    {
        ui.SetActive(true);
    }

    public void Show(string speaker, string[] content, Action OnDialogueEnd = null)
    {
        speakerText.text = speaker;

        contentList = new List<string>();
        contentList.AddRange(content);

        contentIndex = 0;
        contentText.text = contentList[contentIndex];

        ui.SetActive(true);
        this.OnDialogueEnd = OnDialogueEnd;
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void OnContinueButtonClick()
    {
        ++contentIndex;
        if (contentIndex >= contentList.Count)
        {
            OnDialogueEnd?.Invoke();
            Hide();
            return;
        }
        contentText.text = contentList[contentIndex];
    }
}
