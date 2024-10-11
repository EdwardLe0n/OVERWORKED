using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < levelButtons.Length; i++) {
            Button button = levelButtons[i];
            int levelnum = i + 1;
            TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.text = levelnum.ToString();
        }
    }

    public void SelectLevel(Button clickedButton)
    {
        List<Button> buttonsList = new List<Button>(levelButtons); // convert to list to use IndexOf
        int i = buttonsList.IndexOf(clickedButton) + 1;

        string levelName = "Level" + i;
        if(SceneExists(levelName)) {
            SceneManager.LoadScene(levelName);
        }
        else {
            Debug.Log("level doesn't exist yet");
        }
        
    }

    bool SceneExists(string sceneName)
    {
        int buildIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);
        if(buildIndex != -1) {
            return true;
        }
        else {
            return false;
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
