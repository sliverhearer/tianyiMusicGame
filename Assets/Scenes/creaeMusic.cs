using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;


public class creaeMusic : MonoBehaviour
{
    public GameObject notePrefab; // 音符的预制体（Prefab）  
    //public float spawnInterval = 1.0f; // 生成间隔  
    //private float nextSpawnTime = 0.0f; // 下一次生成的时间  
    // Start is called before the first frame
    public TextAsset musicSheetJson; // 引用JSON文件资源  
    private MusicSheetData musicSheetData; // 存储乐谱数据的对象  
    private List<NoteData> notesToSpawn = new List<NoteData>(); // 存储待生成的音符列表  
    private float currentTime = 0f; // 当前时间  
    private Vector3 v3;

    void Start()
    {
        string musicSheet = PlayerPrefs.GetString("SelectedMusicSheet", "DefaultMusicSheet");
        string filePath = Path.Combine(Application.streamingAssetsPath, musicSheet+".json");
        AudioClip clip = (AudioClip)Resources.Load(musicSheet, typeof(AudioClip));

        
            string jsonData = File.ReadAllText(filePath);

            musicSheetData = JsonUtility.FromJson<MusicSheetData>(jsonData);
        
        // 解析JSON数据  
        // 初始化音符生成列表  
        foreach (var note in musicSheetData.notes)
        {
            notesToSpawn.Add(note);
        }
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        //StartCoroutine(SynchronizeNotesWithAudio(audioSource, MusicSheetData.notes));
    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        // 检查是否需要生成新的音符
        for (int i = 0; i < notesToSpawn.Count; i++)
        {
            NoteData noteData = notesToSpawn[i];
            if (currentTime >= noteData.interval)
            {
                v3 = new Vector3(-1, noteData.position, 5);
                // 生成音符对象  
                
                    GameObject noteObj = Instantiate(notePrefab, v3, Quaternion.identity);
                
                // 根据音符类型设置音符对象的属性或行为  
                // 从待生成列表中移除已生成的音符  
                notesToSpawn.RemoveAt(i);
            }
        }
    }

    [System.Serializable]
    public class NoteData
    {
        public float interval; // 间隔时间
        public float position; // 生成位置  
    }

    [System.Serializable]
    public class MusicSheetData
    {
        public string name; // 乐谱名称  
        public List<NoteData> notes; // 音符列表
    }
}
    /*IEnumerator SynchronizeNotesWithAudio(AudioSource audioSource, List<NoteData> notes)
    {
        float currentTime = 0f;
        foreach (var noteData in notes)
        {
            // 等待直到当前时间达到音符应该出现的时间  
            while (currentTime < noteData.Time)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }

            // 显示音符或其他相关逻辑  
            // ...  
        }
    }*/

