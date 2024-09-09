using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskNPCObject : InteractableObject
{
    public string speaker;

    public GameTaskSO gameTaskSO;

    // 便于重置任务状态
    private void Start()
    {
        gameTaskSO.state = GameTaskState.Waiting;
    }

    // 对话结束后回调OnDialogueEnd函数
    protected override void Interact()
    {
        switch (gameTaskSO.state)
        {
            case GameTaskState.Waiting:
                DialogueUI.Instance.Show(speaker, gameTaskSO.dialogueTaskWaiting, OnDialogueEnd);
                break;
            case GameTaskState.Executing:
                DialogueUI.Instance.Show(speaker, gameTaskSO.dialogueTaskExecuting, OnDialogueEnd);
                break;
            case GameTaskState.Completed:
                DialogueUI.Instance.Show(speaker, gameTaskSO.dialogueTaskCompleted, OnDialogueEnd);
                break;
            case GameTaskState.End:
                DialogueUI.Instance.Show(speaker, gameTaskSO.dialogueTaskEnd, OnDialogueEnd);
                break;
        }


    }

    public void OnDialogueEnd()
    {
        switch (gameTaskSO.state)
        {
            case GameTaskState.Waiting:
                gameTaskSO.Start();
                foreach (var item in gameTaskSO.startReward)
                {
                    InventoryManager.Instance.AddItem(item);
                }
                MessageUI.Instance.Show("接受了一个任务");
                break;
            case GameTaskState.Executing:
                break;
            case GameTaskState.Completed:
                foreach (var item in gameTaskSO.completeReward)
                {
                    InventoryManager.Instance.AddItem(item);
                }
                gameTaskSO.End();
                break;
            case GameTaskState.End:
                break;
        }
    }
}
