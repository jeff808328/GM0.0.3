using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThronAttack : MonoBehaviour
{

    private Vector2 AttackBoxSize;
    public LayerMask AttackAble;

    void Start()
    {
        AttackBoxSize = new Vector2(0.3f,20f);

        StartCoroutine(Attack());
    }

    // Update is called once per frame
    IEnumerator Attack()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        var AttackDetect = Physics2D.OverlapBoxAll(transform.position, AttackBoxSize, 0, AttackAble);

        foreach (var Attacked in AttackDetect)
        {
            //   Debug.Log(Attacked.gameObject.name);
            StartCoroutine(Attacked.GetComponent<CommonHP>().Hurt(20, this.transform.position, false));
        }

        yield return new WaitForSecondsRealtime(2.5f);

        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(transform.position, AttackBoxSize);
    }
}
