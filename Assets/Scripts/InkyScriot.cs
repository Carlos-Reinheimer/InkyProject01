using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkyScriot : MonoBehaviour {

    public TextAsset inkJson;
    public Text textPrefab;
    public Button buttonPrefab;
    public new Camera camera;
    public GameObject playerOne;
    public GameObject playerTwo;

    private Story story;
    void Start() {
        story = new Story(inkJson.text);
        refreshUI();
    }

    void refreshUI() {

        eraseUI();

        Text storyText = Instantiate(textPrefab) as Text;
        string text = loadStoryChunk();

        List<string> tags = story.currentTags;
        if (tags.Count > 0) {
            if (tags[0] == "Game")
            {
                camera.transform.LookAt(playerOne.transform);
                playerOne.name = "Focus";
                playerTwo.name = "Player 2";
                camera.GetComponent<CameraController>().lookAtPlayer(playerOne.transform);
            }
            else { 
                camera.transform.LookAt(playerTwo.transform);
                playerTwo.name = "Focus";
                playerOne.name = "Player 1";
                camera.GetComponent<CameraController>().lookAtPlayer(playerTwo.transform);
            }
            text = "<b>" + tags[0] + "</b>" + " - " + text;
        }

        storyText.text = text;
        storyText.transform.SetParent(this.transform, false);

        foreach (Choice choice in story.currentChoices) {
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            Text choiceText = buttonPrefab.GetComponentInChildren<Text>();
            choiceText.text = choice.text;
            choiceButton.transform.SetParent(this.transform, false);

            choiceButton.onClick.AddListener(delegate { // send a method as a parameter for another method
                chooseStoryChoice(choice);
            });
        }
    }
    void eraseUI() {
        for (int i = 0; i < this.transform.childCount; i++) {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    void chooseStoryChoice(Choice choice) {
        story.ChooseChoiceIndex(choice.index);

        refreshUI();
    }


    string loadStoryChunk() { // returns a string
        // ------------ STORY API --------------

        //story.Continue(); // will load the next line

        string text = "";
        if (story.canContinue) {
            text = story.ContinueMaximally(); // will load until hit choises or end story
        }

        return text;
    }
}
