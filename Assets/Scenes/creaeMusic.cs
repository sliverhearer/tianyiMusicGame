using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;


public class creaeMusic : MonoBehaviour
{
    public GameObject notePrefab; // ������Ԥ���壨Prefab��  
    //public float spawnInterval = 1.0f; // ���ɼ��  
    //private float nextSpawnTime = 0.0f; // ��һ�����ɵ�ʱ��  
    // Start is called before the first frame
    public TextAsset musicSheetJson; // ����JSON�ļ���Դ  
    private MusicSheetData musicSheetData; // �洢�������ݵĶ���  
    private List<NoteData> notesToSpawn = new List<NoteData>(); // �洢�����ɵ������б�  
    private float currentTime = 0f; // ��ǰʱ��  
    private Vector3 v3;

    void Start()
    {
        string musicSheet = PlayerPrefs.GetString("SelectedMusicSheet", "DefaultMusicSheet");
        string filePath = Path.Combine(Application.streamingAssetsPath, musicSheet+".json");
        AudioClip clip = (AudioClip)Resources.Load(musicSheet, typeof(AudioClip));

        
            string jsonData = File.ReadAllText(filePath);

            musicSheetData = JsonUtility.FromJson<MusicSheetData>(jsonData);
        
        // ����JSON����  
        // ��ʼ�����������б�  
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
        // ����Ƿ���Ҫ�����µ�����
        for (int i = 0; i < notesToSpawn.Count; i++)
        {
            NoteData noteData = notesToSpawn[i];
            if (currentTime >= noteData.interval)
            {
                v3 = new Vector3(-1, noteData.position, 5);
                // ������������  
                
                    GameObject noteObj = Instantiate(notePrefab, v3, Quaternion.identity);
                
                // ����������������������������Ի���Ϊ  
                // �Ӵ������б����Ƴ������ɵ�����  
                notesToSpawn.RemoveAt(i);
            }
        }
    }

    [System.Serializable]
    public class NoteData
    {
        public float interval; // ���ʱ��
        public float position; // ����λ��  
    }

    [System.Serializable]
    public class MusicSheetData
    {
        public string name; // ��������  
        public List<NoteData> notes; // �����б�
    }
}
    /*IEnumerator SynchronizeNotesWithAudio(AudioSource audioSource, List<NoteData> notes)
    {
        float currentTime = 0f;
        foreach (var noteData in notes)
        {
            // �ȴ�ֱ����ǰʱ��ﵽ����Ӧ�ó��ֵ�ʱ��  
            while (currentTime < noteData.Time)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }

            // ��ʾ��������������߼�  
            // ...  
        }
    }*/

