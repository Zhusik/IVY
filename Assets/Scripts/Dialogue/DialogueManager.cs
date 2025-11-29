using TMPro;
using UnityEngine;
using Ink.Runtime;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayName;
    [SerializeField] private Image portraitImage;

    [Header("Character Portraits")]
    [SerializeField] private List<CharacterPortrait> characterPortraits;

    [System.Serializable]
    public class CharacterPortrait
    {
        public string characterName;
        public Sprite portraitSprite;
    }

    private Story currentStory;
    private bool dialogueIsPlaying;
    private static DialogueManager instance;

    private PlayerMovement player; // 🔥 ДОБАВЛЕНО

    public event Action OnDialogueStart;
    public event Action OnDialogueEnd;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one dialogue manager in the scene");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        if (portraitImage != null)
            portraitImage.gameObject.SetActive(false);

        player = FindFirstObjectByType<PlayerMovement>();
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
            return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            ContinueStory();
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        if (inkJSON == null)
        {
            Debug.LogError("Ink JSON file is null!");
            return;
        }

        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        if (portraitImage != null)
            portraitImage.gameObject.SetActive(true);

        player?.DisableMovement(); // 🔥 ОТКЛЮЧИЛИ ДВИЖЕНИЕ

        OnDialogueStart?.Invoke();
        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        displayName.text = "";

        if (portraitImage != null)
            portraitImage.gameObject.SetActive(false);

        player?.EnableMovement(); // 🔥 ВКЛЮЧИЛИ ДВИЖЕНИЕ

        OnDialogueEnd?.Invoke();
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        if (currentTags == null) return;

        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogWarning("Tag not parsed: " + tag);
                continue;
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    if (displayName != null)
                        displayName.text = tagValue;
                    break;

                case PORTRAIT_TAG:
                    SetPortrait(tagValue);
                    break;

                case LAYOUT_TAG:
                    Debug.Log($"Layout changed to: {tagValue}");
                    break;

                default:
                    Debug.LogWarning("Unknown tag: " + tag);
                    break;
            }
        }
    }

    private void SetPortrait(string characterName)
    {
        if (portraitImage == null)
        {
            Debug.LogWarning("Portrait image is not assigned!");
            return;
        }

        if (characterPortraits == null || characterPortraits.Count == 0)
        {
            Debug.LogWarning("Character portraits list is empty!");
            return;
        }

        CharacterPortrait character = characterPortraits.Find(c =>
            c.characterName.Equals(characterName, StringComparison.OrdinalIgnoreCase));

        if (character != null && character.portraitSprite != null)
        {
            portraitImage.sprite = character.portraitSprite;
        }
        else
        {
            Debug.LogWarning($"Portrait not found for character: {characterName}");
        }
    }
}
