using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class saveGameScript : MonoBehaviour
{
    public savedGame thisGame = new savedGame(0,0); //the current game we are in

    string SAVEFILE_PATH;
    

    private void Awake()
    {
        SAVEFILE_PATH = Application.dataPath + "/savefiles/gamestat.json";

        if (File.Exists(SAVEFILE_PATH))
        {
            loadGame();
        }
        else
        {
            saveGame();
           
        }
        //string json = JsonUtility.ToJson(thisGame, true); 
    }
    

    public class savedGame //create a class
    {
        public int Level; //level data
        public int Score; //score data

        public savedGame( int currentLevel, int currentScore) //use a constructor to populate the values, honestly this took me the longest to understand and I'm still not quite sure why it has to be done like this
        {
            this.Level = currentLevel;
            this.Score = currentScore;
        }
    }

    public void saveGame ()
    {
        thisGame.Level = GameManager.instance.currentGameLevel;
        thisGame.Score = GameManager.instance.currentResetScore;
        string gamestats = JsonUtility.ToJson(thisGame, true); //convert my class data into json
        File.WriteAllText(SAVEFILE_PATH, gamestats); //write that to my file
        Debug.Log(gamestats); //printing it to check
    }

    public void loadGame ()
    {

        string jsonStr = File.ReadAllText(SAVEFILE_PATH); //get the text from the file
        savedGame loadedGame = JsonUtility.FromJson<savedGame>(jsonStr); //read it as class savedgame
        Debug.Log("Loading save file: " + "level - " + loadedGame.Level + "     score - " + loadedGame.Score); //print out the stuff.

        GameManager.instance.currentGameLevel = loadedGame.Level; //change the current level
        GameManager.instance.currentResetScore = loadedGame.Score; //change the current score
    }

    private void OnApplicationQuit()
    {
        saveGame();
    }
}
