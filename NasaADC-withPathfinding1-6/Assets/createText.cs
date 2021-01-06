using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createText : MonoBehaviour
{
    /* public List<Object> individual = new List<Object>();
     // Update is called once per frame
     void Update()
     {
         float speed = 5f;
         float height = 0.5f;
         float newY;
         foreach(object curObj in individual)
         {
             GameObject curGameObj = curObj as GameObject;
             if (curGameObj)
             {
                 Vector3 pos = curGameObj.transform.position;
                 newY = Mathf.Sin(Time.time * speed);
                 curGameObj.transform.position = new Vector3(pos.x, newY + pos.y, pos.z) * height;
             }
             else
             {
                 Debug.Log("curGameObj is null");
             }
         }
     }

     public void putText(Vector3 loc, float azi, float elev)
     {
         var obj = Instantiate(this, loc, Quaternion.identity);
         individual.Add(obj);
         TextMesh mesh = (TextMesh)obj.GetComponent(typeof(TextMesh));
         mesh.text = "Azimuth: "+azi+"\nElevation: "+elev;
     }*/
    public List<GameObject> individual = new List<GameObject>();
    public GameObject source;
    // Update is called once per frame
    void Update()
    {
        float speed = 5f;
        float height = 0.5f;
        float newY;
        foreach (GameObject curGameObj in individual)
        {
            if (curGameObj)
            {
                Vector3 pos = curGameObj.transform.position;
                newY = Mathf.Sin(Time.time * speed);
                curGameObj.transform.position = new Vector3(pos.x, pos.y, pos.z) * height;
                
            }
            else
            {
                Debug.Log("curGameObj is null");
            }
        }
    }

    public void putText(Vector3 loc, float azi, float elev)
    {
        GameObject obj = Instantiate(source, loc, Quaternion.identity) as GameObject;
        individual.Add(obj);
        TextMesh mesh = (TextMesh)obj.GetComponent(typeof(TextMesh));
        mesh.text = "Azimuth: " + azi + "\nElevation: " + elev;
    }
}
