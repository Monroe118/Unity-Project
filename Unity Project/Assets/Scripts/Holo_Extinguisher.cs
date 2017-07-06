using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holo_Extinguisher : MonoBehaviour {

    private float transX;
    private float transY;
    private float transZ;

    public GameObject extinguisher;

    // 随机改变颜色
    public void ToRandom()
    {

        extinguisher.GetComponent<MeshRenderer>().material.color = new
            Color(Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f,
            Random.Range(0, 255) / 255f);

        Debug.Log(11);

    }
    
    // 缩小
    public void ToSmall()
    {

        transX = extinguisher.GetComponent<Transform>().transform.localScale.x;

        transY = extinguisher.GetComponent<Transform>().transform.localScale.y;

        transZ = extinguisher.GetComponent<Transform>().transform.localScale.z;

        extinguisher.GetComponent<Transform>().transform.localScale = new
            Vector3(transX - 0.2f, transY - 0.2f, transZ - 0.2f);

        Debug.Log(12);

    }

    // 恢复原状
    public void ToReset()
    {

        extinguisher.GetComponent<Transform>().transform.localScale
            = new Vector3(1f, 1f, 1f);

        Debug.Log(13);

    }
    
    // 单击
    private void OnTap()
    {

        extinguisher.GetComponent<MeshRenderer>().material.color = Color.white;

        Debug.Log(14);

    }

    // 双击
    private void OnDoubleTap()
    {

        extinguisher.GetComponent<MeshRenderer>().material.color = Color.cyan;

        Debug.Log(15);

    }
}
