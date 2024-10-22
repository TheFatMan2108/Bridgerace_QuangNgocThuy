using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour,ISavesable
{
    public static UI_Manager instance { get; private set; }
    [SerializeField] Transform uiLevels,uiMenu;
    private bool isOnMusic;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        GameManager.instance.AddSavesable(this);
       
    }

    public void HideUI()
    {
        uiMenu.gameObject.SetActive(false);
    }

    public void Save(ref DataGame data)
    {
        data.isOnMusic = isOnMusic;
    }

    public void Load(DataGame data)
    {
        if (data == null)
        {
            data = new DataGame();
            isOnMusic = data.isOnMusic;
            return;
        }
        isOnMusic = data.isOnMusic;
        OnInit();
    }

    private void OnInit()
    {
        for (int i = 0; i < uiLevels.childCount; i++)
        {
            Button button = uiLevels.GetChild(i).GetComponent<Button>();
            button.interactable = false;
            int.TryParse(uiLevels.transform.GetChild(i).name, out int index);
            button.onClick.AddListener(() => GameManager.instance.Onlevel(index));

        }
        for (int i = 0; i < GameManager.instance.curentLevel + 1; i++)
        {
            uiLevels.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.RemoveSavesable(this);
    }
}
