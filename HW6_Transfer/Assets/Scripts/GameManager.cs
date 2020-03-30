using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // create singleton status go!

    
    public int currentGameLevel = 0;
    public int currentResetScore = 0;

    public int prizeCounter = 0;

    string CURRENTGAME_FILEPATH = "/savefiles";
    public string filePath;

    saveGameScript gameSavingUtil; //the reference to save all my game stuff
    
   

    private void Awake()
    {
        //singleton stuff
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        gameSavingUtil = gameObject.GetComponent<saveGameScript>();

        filePath = Application.dataPath + CURRENTGAME_FILEPATH; //define filepath for the files
        accessSaveFile();
        
    }
 
    public void accessSaveFile()
    {
        if (Directory.Exists(filePath))//seeing if the folder exists
        {
            //gameSavingUtil.loadGame();
        }
        else
        {
            Directory.CreateDirectory(filePath); //otherwise create the folder where the files will live
            gameSavingUtil.saveGame();//save the null game
        }

        

    }

    public void startGame()
    {
        if(Directory.Exists(filePath)) //look for the old game files
        {
            Directory.Delete(filePath, true);//delete the old ones
        }
        accessSaveFile(); //remake the files
        currentGameLevel++; //add one to the level
        //make sure to erase everything from the json files
        nextLevel(); //do the next level function
    }

    public void continueGame()
    {
        //Using all current files, start the game
        gameSavingUtil.loadGame();
        SceneManager.LoadScene(currentGameLevel); //load the current level based on the loaded game object
        

        
    }
    public void nextLevel ()
    {

        gameSavingUtil.saveGame(); //do the save game function
        Debug.Log("I am saving the game");
        Debug.Log("Loading in the next scene: " + currentGameLevel);
        SceneManager.LoadScene(currentGameLevel);
      

    }

    public void resetScoreCounter ()
    {
       
        GameObject scoreCounterOBJ = GameObject.Find("ResetCounter");
        Text scoreCounterText = scoreCounterOBJ.GetComponent<Text>();
        scoreCounterText.text = "Reset Counter: \n" + currentResetScore;


    }

    public void getPrize() //you got a prize, the number until next 
    {
        prizeCounter--;

        Debug.Log ("You got a prize! There are only " + prizeCounter + " left until the next level!");
        if(prizeCounter <= 0)
        {
            currentGameLevel++;
            nextLevel();
        }
    }

   
}
