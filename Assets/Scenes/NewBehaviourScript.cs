using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public LayerMask noteLayerMask; // 音符所在的层，确保只在这一层搜索音符  
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
        mainCamera = Camera.main; // 获取主相机  
        string songname = PlayerPrefs.GetString("SelectedMusicSheet", "DefaultMusicSheet");
        songName.text = songname;
    }

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        if (Input.touchCount > 0)  
    {  
        Touch touch = Input.GetTouch(0); // 获取第一个触摸  
        Vector2 touchPosition = new Vector2(touch.position.x, touch.position.y);  
        // 将触摸位置从屏幕坐标转换为世界坐标或摄像机坐标（取决于你的需求）  
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, Camera.main.nearClipPlane));  
        // 或者使用Raycast来获取更精确的世界坐标  
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);  
        RaycastHit hit;  
        if (Physics.Raycast(ray, out hit,Mathf.Infinity,ClikcLayerMask))  
        {  
            // 物体被触碰到了  
           // Debug.Log("Hit object: " + hit.collider.gameObject.name);
                // 触发物体的函数  
                GameObject clickedObject = hit.collider.gameObject;
                GameObject nearestNote = FindNearestNote(clickedObject);
                if (nearestNote != null)
                {
                    Destroy(nearestNote); // 删除最近的音符 
                    //Debug.Log("1");
                }
            }  
    }  
       /* if (Input.GetMouseButtonDown(0)) // 检查鼠标左键是否按下  
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // 创建射线  

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ClikcLayerMask)) // 执行射线投射，并限制在音符层搜索  
            {
                //Debug.Log("h");
                // 找到距离被点击物体最近的音符并删除它  
                GameObject clickedObject = hit.collider.gameObject;
                GameObject nearestNote = FindNearestNote(clickedObject);
                if (nearestNote != null)
                {
                    Destroy(nearestNote); // 删除最近的音符 

                    //Debug.Log("1");
                }
            }
        }*/
    }

    // 查找距离指定物体最近的音符并返回它  
    private GameObject FindNearestNote(GameObject targetObject)
    {
        GameObject nearestNote = null;
        float minDistance = Mathf.Infinity;

        // 假设所有音符都带有某个标签或位于某个层，这里使用LayerMask来过滤  
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