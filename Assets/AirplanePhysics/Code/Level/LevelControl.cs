using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SqliteDB;
using SQLite4Unity3d;
namespace AirControl
{
    public class LevelControl:MonoBehaviour
    {
        void Update()
        {
            SQLiteConnection connection = DB_Init.GetConnection();
            if(DB_Functions.getLevelReset(connection)){
                RestartLevel();
                //set back the transaction to deault to avoid multiple firing
                DB_Functions.resetLevelReset(connection);
            }
        }
        public void RestartLevel() //Restarts the level
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }
}

