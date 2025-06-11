using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DialogueCuyo1 : MonoBehaviour
{
    [SerializeField] private GameObject DialogueMark;
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField, TextArea(4, 6)] private string[] DialogueLines;

    private float typingTime = 0.05f;

    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;


    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (DialogueText.text == DialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                DialogueText.text = DialogueLines[lineIndex];
            }

        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        DialoguePanel.SetActive(true);
        DialogueMark.SetActive(false);
        lineIndex = 0;
        AudioManager.instance.PlayAudio(AudioManager.instance.interact);

        StartCoroutine(Showline());
    }

    private void NextDialogueLine()
    {
        AudioManager.instance.PlayAudio(AudioManager.instance.interact);

        lineIndex++;
        if (lineIndex < DialogueLines.Length)
        {
            StartCoroutine(Showline());
        }
        else
        {
            didDialogueStart = false;
            DialoguePanel.SetActive(false);
            DialogueMark.SetActive(true);

        }
    }

    private IEnumerator Showline()
    {
        DialogueText.text = string.Empty;

        foreach (char ch in DialogueLines[lineIndex])
        {
            DialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            DialogueMark.SetActive(true);
            Debug.Log("Se puede iniciar un dialogo");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            DialogueMark.SetActive(false);
            Debug.Log("No se puede iniciar un dialogo");
        }

    }
}
