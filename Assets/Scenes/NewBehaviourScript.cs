using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public LayerMask noteLayerMask; // �������ڵĲ㣬ȷ��ֻ����һ����������  
    private Camera mainCamera;
    private RaycastHit hit;
    public LayerMask ClikcLayerMask;
    public int score = 0;
    public Text scoreText;
    public Text songName;

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("Score Text element is not assigned in the inspector.");
        }
        mainCamera = Camera.main; // ��ȡ�����  
        string songname = PlayerPrefs.GetString("SelectedMusicSheet", "DefaultMusicSheet");
        songName.text = songname;
    }

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        if (Input.touchCount > 0)  
    {  
        Touch touch = Input.GetTouch(0); // ��ȡ��һ������  
        Vector2 touchPosition = new Vector2(touch.position.x, touch.position.y);  
        // ������λ�ô���Ļ����ת��Ϊ�����������������꣨ȡ�����������  
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, Camera.main.nearClipPlane));  
        // ����ʹ��Raycast����ȡ����ȷ����������  
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);  
        RaycastHit hit;  
        if (Physics.Raycast(ray, out hit,Mathf.Infinity,ClikcLayerMask))  
        {  
            // ���屻��������  
           // Debug.Log("Hit object: " + hit.collider.gameObject.name);
                // ��������ĺ���  
                GameObject clickedObject = hit.collider.gameObject;
                GameObject nearestNote = FindNearestNote(clickedObject);
                if (nearestNote != null)
                {
                    Destroy(nearestNote); // ɾ����������� 
                    //Debug.Log("1");
                }
            }  
    }  
       /* if (Input.GetMouseButtonDown(0)) // ����������Ƿ���  
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // ��������  

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ClikcLayerMask)) // ִ������Ͷ�䣬������������������  
            {
                //Debug.Log("h");
                // �ҵ����뱻������������������ɾ����  
                GameObject clickedObject = hit.collider.gameObject;
                GameObject nearestNote = FindNearestNote(clickedObject);
                if (nearestNote != null)
                {
                    Destroy(nearestNote); // ɾ����������� 

                    //Debug.Log("1");
                }
            }
        }*/
    }

    // ���Ҿ���ָ�����������������������  
    private GameObject FindNearestNote(GameObject targetObject)
    {
        GameObject nearestNote = null;
        float minDistance = Mathf.Infinity;

        // ������������������ĳ����ǩ��λ��ĳ���㣬����ʹ��LayerMask������  
        Collider[] noteColliders = Physics.OverlapBox(targetObject.transform.position, new Vector3(1f, 1f, 100f), Quaternion.identity, noteLayerMask);

        foreach (Collider noteCollider in noteColliders)
        {
            float distance = Vector3.Distance(targetObject.transform.position, noteCollider.transform.position);
            Debug.Log(distance);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNote = noteCollider.gameObject;
            }
            if (distance < 5.06f) { Debug.Log("perfact"); score += 100; }
            else { Debug.Log("far"); score += 50; }
        }
        return nearestNote;
    }
}