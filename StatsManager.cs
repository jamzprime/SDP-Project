using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * record the player stats
 * display the stats on the screen
 * */
public class StatsManager : MonoBehaviour {

	public static StatsManager instance;

	public int currentWaveCount;
	public int currentGoldCount;
	public int currentLivesCount;
	public int MapWaveNumber;

	public Text goldCountText;
	public Text waveCountText;
	public Text livesCountText;
	public Text toggleButtonText;
	public Button toggleButton;
	public float currentTimeScale;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		currentWaveCount = 0;
		currentGoldCount = 0;
		UpdateGoldCCount(100);
		currentLivesCount = 5;
		goldCountText.text = "Gold: " + currentGoldCount;
		waveCountText.text = "Wave: " + currentWaveCount + "/" + Maps.maxWaves[MapNumStore.selectedMap];
		livesCountText.text = "" + currentLivesCount;
		currentTimeScale = Time.timeScale;
	}

	//Updates gold, wave, and lives count on a fixed update
	void FixedUpdate()
	{
		//update the information to the UI
		goldCountText.text = "Gold: "+currentGoldCount;
		waveCountText.text = "Wave: " + currentWaveCount + "/" + Maps.maxWaves[MapNumStore.selectedMap];
		livesCountText.text = ""+currentLivesCount;
	}

    /*
     * Manuel update the player gold collected
     * */
	public void UpdateGoldCCount(int goldCount)
	{
		currentGoldCount += goldCount;
		goldCountText.text = "Gold: "+currentGoldCount;
		PlayerPrefs.SetInt("TotalGoldC", PlayerPrefs.GetInt("TotalGoldC", 0) + goldCount);
		//Debug.Log(PlayerPrefs.GetInt("TotalGold"));
	}
	/*
     * Manuel update the player gold spent
     * */
	public void UpdateGoldSCount(int goldCount)
	{
		PlayerPrefs.SetInt("TotalGoldS", PlayerPrefs.GetInt("TotalGoldS", 0) + goldCount);
	}

    /*
     * Manual update the current wave
     * */
    public void UpdateWaveCount(int WaveCount)
	{
		currentWaveCount = WaveCount;
		waveCountText.text = "Wave: "+currentWaveCount+"/"+MapWaveNumber;
		PlayerPrefs.SetInt("CurrentWave", currentWaveCount);
		if (currentWaveCount > PlayerPrefs.GetInt("HighestWave"))
		{
			PlayerPrefs.SetInt("HighestWave", currentWaveCount);
			Debug.Log(PlayerPrefs.GetInt("HighestWave"));
		}
	}

    /*
     * Manual update of how any lives the player has left
     * */
    public void UpdateLivesCount()
	{
		if (currentLivesCount > 0) {
			currentLivesCount -= 1;
			livesCountText.text = "" + currentLivesCount;
		} 
		if (currentLivesCount == 0) {
			SceneManager.LoadSceneAsync("Menus/GameOverScene/gameOverScene", LoadSceneMode.Single);
		}
	}

	public void UpdateEnemiesKilledCount()
	{
		PlayerPrefs.SetInt("EnemiesKilled", (PlayerPrefs.GetInt("EnemiesKilled")+1));
		Debug.Log(PlayerPrefs.GetInt("EnemiesKilled"));
	}
	public void UpdateTurretsPlacedCount()
	{
		PlayerPrefs.SetInt("TurretsPlaced", (PlayerPrefs.GetInt("TurretsPlaced")+1));
		Debug.Log(PlayerPrefs.GetInt("TurretsPlaced"));
	}

    /*
     * change the toggle button color
     * */
	public void ToggleText(bool toggled)
	{
		if (toggled) {
			toggleButtonText.text = ">>";
			toggleButton.GetComponent<Image>().color = new Color(0,245f/255f,175f/255f,255f/255f);
		} else {
			toggleButtonText.text = ">";
			toggleButton.GetComponent<Image>().color = new Color(113f/255f,205f/255f,255f/255f,255f/255f);
		}
	}
		
}
