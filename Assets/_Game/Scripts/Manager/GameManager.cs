using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GameManager : MonoBehaviour,ISavesable
{
    [SerializeField] string ID_DATA = "23485798237498237492";
    public DataGame dataGame;
    public static GameManager instance {  get; private set; }
    private List<IWinsable> winsables = new List<IWinsable>();
    private List<ISavesable> savesables = new List<ISavesable>();
    public List<CharacterBase> characterBases = new List<CharacterBase>();
    public List<GameObject> levels = new List<GameObject>();
    public int curentLevel = 0;
    Coroutine onStop;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance == null)instance = this;
        else Destroy(gameObject);
        AddSavesable(this);
    }
    private void Start()
    {
        LoadGame();
    }
    public void AddWinsable(IWinsable winsable)=>winsables.Add(winsable);
    public void AddSavesable(ISavesable saveable)=>savesables.Add(saveable);
    public void AddCharacter( CharacterBase character)=>characterBases.Add(character);
    public void RemoveWinsable(IWinsable winsable)=>winsables.Remove(winsable);
    public void RemoveSavesable(ISavesable savesable)=> savesables.Remove(savesable);
    public void RemoveCharacter(CharacterBase charBase)=>characterBases.Remove(charBase);

    public void Winner()
    {
        foreach (IWinsable item in winsables)
        {
            item.OnWin();
        }
        curentLevel++;
        if (curentLevel > dataGame.Level)
        {
            dataGame.Level = curentLevel;
            SaveData();
        }
        onStop = StartCoroutine(Stop(5));
    }
    public void SaveData()
    {
        foreach (var item in savesables)
        {
            item.Save(ref dataGame);
        }
    }
    public void LoadGame()
    {
        string nData = PlayerPrefs.GetString(ID_DATA, "");
        dataGame = JsonUtility.FromJson<DataGame>(nData);
        Debug.Log(nData);
        foreach (var item in savesables)
        {
            item.Load(dataGame);
        }
    }
    IEnumerator Stop(float time)
    {
        yield return new WaitForSeconds(time);
        StopWin();
        yield return new WaitForEndOfFrame();
        Onlevel(curentLevel+1);
        onStop = null;

    }

    public void StopWin()
    {
        foreach (IWinsable item in winsables)
        {
            item.OnStop();
        };
    }
    public void Onlevel(int index)
    {
       
        curentLevel = index - 1;
        if (curentLevel >= levels.Count) curentLevel = 0;
        Instantiate(levels[curentLevel], Vector3.zero, Quaternion.identity);
        UI_Manager.instance.HideUI();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) Onlevel(curentLevel+1);
    }

    public void Save( ref DataGame data)
    {
        string nData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(ID_DATA, nData);
        Debug.Log(nData);
    }

    public void Load(DataGame data)
    {
        if(data == null)
        {
            data = new DataGame();
            dataGame = data;
            curentLevel = dataGame.Level;
            return;
        }
        dataGame = data;
        curentLevel = dataGame.Level;
    }
    private void OnDestroy()
    {
        RemoveSavesable(this);
    }
}
