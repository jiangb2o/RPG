using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageUI : MonoBehaviour
{
    public static MessageUI Instance { get; private set; }

    private float fadeSpeed = 2;
    private List<Coroutine> coroutines;
    private List<GameObject> gameObjects;

    public GameObject textTemplate;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        coroutines = new List<Coroutine>();
        gameObjects = new List<GameObject>();

        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gameObjects.Count; ++i)
        {
            if (gameObjects[i].GetComponent<TextMeshProUGUI>().color.a <= 0)
            {
                StopCoroutine(coroutines[i]);
                Destroy(gameObjects[i]);

                gameObjects.Remove(gameObjects[i]);
                coroutines.Remove(coroutines[i]);
            }
        }
    }

    public void Show(string message)
    {
        GameObject textObject = Instantiate(textTemplate, this.transform);
        TextMeshProUGUI tmp = textObject.GetComponent<TextMeshProUGUI>();
        tmp.enabled = true;
        tmp.color = Color.white;
        tmp.text = message;

        gameObjects.Add(textObject);
        coroutines.Add(StartCoroutine(Fade(tmp)));
    }

    public void Hide()
    {
    }

    IEnumerator Fade(TextMeshProUGUI tmp)
    {
        while (true)
        {
            Color color = tmp.color;
            tmp.color = new Color(color.r, color.g, color.b, color.a - Time.deltaTime / fadeSpeed);
            yield return 0;
        }
    }
}
