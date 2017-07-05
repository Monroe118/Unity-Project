using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holo_Capsule : MonoBehaviour {

    private float transX;
    private float transY;
    private float transZ;

    // 随机改变颜色
    public void ToTest() {

        gameObject.GetComponent<MeshRenderer>().material.color = new
            Color(Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f,
            Random.Range(0, 255) / 255f);

        Debug.Log(11);

    }

    // 缩小
    public void ToSmall() {

        transX = gameObject.GetComponent<Transform>().transform.localScale.x;

        transY = gameObject.GetComponent<Transform>().transform.localScale.y;

        transZ = gameObject.GetComponent<Transform>().transform.localScale.z;

        gameObject.GetComponent<Transform>().transform.localScale = new 
            Vector3(transX - 0.2f , transY - 0.2f, transZ - 0.2f);

        Debug.Log(12);

    }

    // 恢复原状
    public void ToRestore() {

       
        gameObject.GetComponent<Transform>().transform.localScale 
            = new Vector3(1f, 1f, 1f);

        Debug.Log(13);

    }

    private void OnTap()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    private void OnDoubleTap()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
    }

}
