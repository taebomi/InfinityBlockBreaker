using UnityEngine;
using System.Collections;

public class Zone_Warning : MonoBehaviour {
    public GameObject warn;
    public static int count = 0;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            if(warn.activeSelf== false)
            warn.SetActive(true);
        }
    }
    void Start()
    {
        StartCoroutine(CheckWarning());
    }
    IEnumerator CheckWarning()
    {
        while (true)
        {
            if (warn.activeSelf == true && count == 0)
            {
                warn.SetActive(false);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
