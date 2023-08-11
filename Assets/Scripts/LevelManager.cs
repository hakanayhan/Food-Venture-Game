using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; 
    public int currentLv;
    public double requiredGoldsToLevelUp;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UpgradeLevel()
    {
        if (IsNextLvExist())
            SceneManager.LoadScene(currentLv + 1);
    }
    
    public bool IsNextLvExist()
    {
        return (SceneManager.sceneCountInBuildSettings > currentLv + 1);
    }
}
