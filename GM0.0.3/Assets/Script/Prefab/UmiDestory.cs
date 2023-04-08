using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmiDestory : MonoBehaviour
{
    public GameObject FollowTarget;

    void Start()
    {
        StartCoroutine(UmiDsetroy()); 
    }

    private void Update()
    {
        transform.position = new Vector3(FollowTarget.transform.position.x, FollowTarget.transform.position.y + 1f,-3);
    }

    IEnumerator UmiDsetroy()
    {
        yield return new WaitForSecondsRealtime(2f);

        Destroy(this.gameObject);
    }
}
