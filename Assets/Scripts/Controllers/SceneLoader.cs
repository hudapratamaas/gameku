using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    [SerializeField] AudioClip mainMenuClip;
    public GameObject mainMenu;
    public GameObject settingMenu;
    public GameObject aboutMenu;
    public StoryScene[] sceneCollections;
    public GameObject levelSelectionPrefab;
    public LevelSelector levelSelector;
    public LevelSelection levelSelection;
    public GameObject parent;
    private void Start()
    {
        levelSelector = GameObject.FindGameObjectWithTag("LevelSelector")?.GetComponent<LevelSelector>();
        if(parent==null)
            return;
        
        if(mainMenu != null || settingMenu != null || aboutMenu != null){
            mainMenu.SetActive(true);
            settingMenu.SetActive(false);
            aboutMenu.SetActive(false);
        }
        else{
            SpawnLevel();
          //  return;
        }
    }

    private void SpawnLevel()
    {
        GameController.LoadData();
        
        foreach (Transform child in parent.transform)
        {
            Destroy(child);
        }
        for(int i = 0; i < sceneCollections.Length/3; i++){
            // Debug.Log(i*3);
            int index = i*3;
            GameObject spawnedLevelButton = Instantiate(levelSelectionPrefab, Vector3.zero, Quaternion.identity, parent.transform);
            
            PlayerData data = GameController.LoadData();
            
          //  Debug.Log(data.starPerLevel[i]);
            Debug.Log("1");
            
            //spawnedLevelButton.transform.SetAsFirstSibling();
            if(i == 0)
            {
                spawnedLevelButton.transform.GetChild(0).gameObject.SetActive(false);
               // spawnedLevelButton.GetComponent<LevelSelection>().unlocked = true;
                //spawnedLevelButton.GetComponent<LevelSelection>().lockImage.sprite.;
                //spawnedLevelButton.GetComponent<Button>().onClick.AddListener(()=>{
                //levelSelector.levelSelected = index;
                //LoadScene("Kuis");
                //});
                Debug.Log("0");
            }
            else
            {
                Debug.Log("2");
                if (data.starPerLevel[i-1] > 0)//If the first level star is bigger than 0, second level can play
                {
                    spawnedLevelButton.transform.GetChild(0).gameObject.SetActive(false);
                    spawnedLevelButton.transform.GetChild(1).gameObject.SetActive(false);
                    spawnedLevelButton.transform.GetChild(2).gameObject.SetActive(false);
                    spawnedLevelButton.transform.GetChild(3).gameObject.SetActive(false);
                    //spawnedLevelButton.GetComponent<LevelSelection>().unlocked = true;
                }
            }
            if(spawnedLevelButton.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                spawnedLevelButton.GetComponent<Button>().interactable = false;
              //  spawnedLevelButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                spawnedLevelButton.GetComponent<Button>().interactable = true;
            }
            Debug.Log("pindah tempat");
            spawnedLevelButton.transform.localPosition = new Vector2((i-1)*130+5, (1-i)*250-75);
            // spawnedLevelButton.GetComponent<Button>().onClick.RemoveAllListeners();
            spawnedLevelButton.GetComponent<Button>().onClick.AddListener(()=>{
                levelSelector.levelSelected = index;
                LoadScene("Kuis");
            });
            levelSelection.levels.Add(spawnedLevelButton);
        }
    }

    public void LoadScene(string sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void Exit(string sceneIndex){
        Destroy(levelSelector.gameObject);
        var audioManager = GameObject.Find("AudioManager");
        audioManager.GetComponent<AudioSource>().clip = mainMenuClip;
        audioManager.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void SettingGame(){
        settingMenu.SetActive(true);
        mainMenu.SetActive(false);
        aboutMenu.SetActive(false);
        Debug.Log("Tombol Setting Ditekan");
    }
    public void BackMenu(){
        mainMenu.SetActive(true);
        settingMenu.SetActive(false);
        aboutMenu.SetActive(false);
        Debug.Log("Tombol Back Ditekan");
    }
    public void AboutMenu(){
        aboutMenu.SetActive(true);
        settingMenu.SetActive(false);
        mainMenu.SetActive(false);
        Debug.Log("Tombol Back Ditekan");
    }
    public void BackSetting(){
        aboutMenu.SetActive(false);
        settingMenu.SetActive(true);
        Debug.Log("Tombol Back Ditekan");
    }
}
