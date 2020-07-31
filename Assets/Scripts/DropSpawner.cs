using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    public GameObject DropPrefab;
    [SerializeField] private int numMax = 100;
    private int curNum;

    private float cooldown = 0;
   // private Camera mainCamera;
    private bool isGameOver = false;
    private bool pullWater = false;
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.AddListener(EGameEvent.eGameEvent_PullWater, OnPullWater);
        
    }

    private void OnPullWater()
    {
        if (!pullWater)
        {
            pullWater = true;
        }
       
    }

    private void OnEnable()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pullWater)
        {
            cooldown -= Time.deltaTime;
            while(cooldown < 0 && curNum < numMax) {
                cooldown += 0.01f;
                curNum++;
                GameObject obj = Instantiate(DropPrefab, (Vector2)gameObject.transform.position+ Random.insideUnitCircle * 0.2f, Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
            }

        }

        if(curNum >= numMax&& !isGameOver)
        {
            isGameOver = true;
            pullWater = false;
            StartCoroutine("getScroe");

        }
    }

    [System.Obsolete]
    IEnumerator getScroe()
    {
        yield return new WaitForSeconds(5.0f);
        int count = gameObject.transform.GetChildCount();
        Debug.Log("child count===" + count);
        if (count >= numMax)
        {
            Debug.Log("满了");
        }
        else if (count >=numMax / 2)
        {
            Debug.Log("半满");
        }
        else
        {
            Debug.Log("没满");
        }
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EGameEvent.eGameEvent_PullWater, OnPullWater);
    }
}
