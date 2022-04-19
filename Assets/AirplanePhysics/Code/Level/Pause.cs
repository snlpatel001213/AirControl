using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;

public class Pause : MonoBehaviour
{
    static bool active = false;

    /// <summary>
    /// play/pause the simulation
    /// </summary>
    public static void Pressed()
    {
            if (active==false){
                PauseGame();
                active = true;
            }
            else{
                ContinueGame();
                active = false;
            }
        
    }
    /// <summary>
    /// Pause Game
    /// </summary>
    public static void PauseGame()
    {
        Time.timeScale = 0;
        Debug.Log("Simulation Paused");
    } 
    /// <summary>
    /// Resume Game
    /// </summary>
    public static void ContinueGame()
    {
        Time.timeScale = 1;
        Debug.Log("Simulation Resumed");
    }
}
