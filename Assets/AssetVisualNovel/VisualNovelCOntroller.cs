using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Dialog
{
    public string speakerName;
    [TextArea(2, 5)]
    public string sentence;
    public Sprite characterSprite;
    public bool isLeftSide;
}

public class VisualNovelController : MonoBehaviour
{
    public Image characterLeft;
    public Image characterRight;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Button nextButton;

    public Dialog[] dialogs;
    private int currentIndex = 0;

    void Start()
    {
        nextButton.onClick.AddListener(ShowNextDialog);
        ShowNextDialog();
    }

    void ShowNextDialog()
    {
        if (currentIndex >= dialogs.Length)
        {
            gameObject.SetActive(false); // Sembunyikan VN setelah selesai
            return;
        }

        var currentDialog = dialogs[currentIndex];
        nameText.text = currentDialog.speakerName;
        dialogueText.text = currentDialog.sentence;

        if (currentDialog.isLeftSide)
        {
            characterLeft.sprite = currentDialog.characterSprite;
            characterLeft.gameObject.SetActive(true);
            characterRight.gameObject.SetActive(false);
        }
        else
        {
            characterRight.sprite = currentDialog.characterSprite;
            characterRight.gameObject.SetActive(true);
            characterLeft.gameObject.SetActive(false);
        }

        currentIndex++;
    }
}
