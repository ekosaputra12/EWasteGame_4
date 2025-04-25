using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomBarController : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;

    private int sentenceIndex = -1;
    public StoryScene storyScene;
    private StoryScene currentScene;
    private State state = State.COMPLETED;
    private Coroutine typingCoroutine;

    private enum State
    {
        PLAYING, COMPLETED
    }

    public void PlayScene(StoryScene scene)
    {
        currentScene = scene;
        sentenceIndex = -1;
        PlayNextSentence();
    }

    public void PlayNextSentence()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        sentenceIndex++;

        if (sentenceIndex >= currentScene.sentences.Count)
        {
            state = State.COMPLETED;
            return;
        }

        var sentence = currentScene.sentences[sentenceIndex];
        typingCoroutine = StartCoroutine(TypeText(sentence.text));

        personNameText.text = sentence.speaker.speakerName;
        personNameText.color = sentence.speaker.textColor;
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    private IEnumerator TypeText(string fullText)
    {
        barText.text = "";
        state = State.PLAYING;

        for (int i = 0; i < fullText.Length; i++)
        {
            barText.text += fullText[i];
            yield return new WaitForSeconds(0.05f);
        }

        state = State.COMPLETED;
    }
}
