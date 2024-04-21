using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class controlMusic : MonoBehaviour
{
    
    public GameObject notePrefab; // 音符预制体  
    private NewBehaviourScript newBehaviourScript;
    private Vector3 startPosition; // 音符的起始位置  
    private float spawnTime; // 音符生成时间
    public float fallSpeed = 0.4f;
    public float noteLifespan = 1f;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        spawnTime = Time.time; // 记录音符生成时间  
        newBehaviourScript = FindObjectOfType<NewBehaviourScript>();
    }

    // Update is called once per frame
    void Update()
    {


        // 检查是否需要生成新的音符  
        transform.Translate(Time.deltaTime * fallSpeed,0, 0);
        // 检查音符是否应该被销毁（基于生命周期）  
        if (Time.time - spawnTime > noteLifespan)
        {
            Destroy(gameObject); // 销毁音符GameObject  
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
