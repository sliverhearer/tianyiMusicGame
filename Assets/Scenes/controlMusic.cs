using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class controlMusic : MonoBehaviour
{
    
    public GameObject notePrefab; // ����Ԥ����  
    private NewBehaviourScript newBehaviourScript;
    private Vector3 startPosition; // ��������ʼλ��  
    private float spawnTime; // ��������ʱ��
    public float fallSpeed = 0.4f;
    public float noteLifespan = 1f;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        spawnTime = Time.time; // ��¼��������ʱ��  
        newBehaviourScript = FindObjectOfType<NewBehaviourScript>();
    }

    // Update is called once per frame
    void Update()
    {


        // ����Ƿ���Ҫ�����µ�����  
        transform.Translate(Time.deltaTime * fallSpeed,0, 0);
        // ��������Ƿ�Ӧ�ñ����٣������������ڣ�  
        if (Time.time - spawnTime > noteLifespan)
        {
            Destroy(gameObject); // ��������GameObject  
            Debug.Log("miss");
            if (newBehaviourScript.score >= 100)
            {
                newBehaviourScript.score -= 100;
            }
            else
            {
                newBehaviourScript.score = 0;
            }
        }
    }
    public void getFast()
    {
        fallSpeed += 0.1f;
    }
    public void getSlow()
    {
        fallSpeed -= 0.1f;
    }
}
[System.Serializable]
public class NoteData
{
    public string type;
    public float interval;
    public Vector3 position;
}
[System.Serializable]
public class MusicSheetData
{
    public string name;
    public List<NoteData> notes;
}
