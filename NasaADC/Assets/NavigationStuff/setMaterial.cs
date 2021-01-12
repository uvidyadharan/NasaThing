using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;
using UnityEngine;

public class setMaterial : MonoBehaviour
{
    //private Material mainMat = new Material(Shader.Find("Specular"));
   // public Material mainMat;
    public RenderTexture rTex;
    public Material FlatMat;
    public Material DistMat;
    // Start is called before the first frame update
    public IEnumerator Start()
    {
        /* yield return new WaitForEndOfFrame();
         takePic(rTex);
         yield return null;
        */

        yield return null;
    }
        
    // Update is called once per frame
    void Update()
    {
        
    }
    public void takePic(RenderTexture renderTex)
    {
        //Projector proj = GetComponent<Projector>();
        // yield return new WaitForEndOfFrame();
        Debug.Log("Ran takePic");
        Texture2D tex = new Texture2D(8192, 8192, TextureFormat.RGB24, false);
        RenderTexture.active = renderTex;
        tex.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();
        string path = Application.dataPath + "/SavedScreenNew.png";
        Debug.Log("path: " + path);
        File.WriteAllBytes(path, bytes);
        RenderTexture.active = null;
        //mainMat.mainTexture = tex;
        //proj.material = mainMat;
    }
    public void setProjMat(bool opt) //opt=0 means flat, opt=1 means dist.
    {
        Projector proj = GetComponent<Projector>();
        if (opt)
            proj.material = DistMat;
        else
            proj.material = FlatMat;
    }
}
