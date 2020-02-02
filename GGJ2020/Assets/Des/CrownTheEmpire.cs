using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownTheEmpire : MonoBehaviour
{

    public SlimeManager slimeManagerScript;
    public Slime currentSlime;
    public Slime thisSlime;

    public Vector3[] crownScales;
    public Vector3[] crownPos;

    // Start is called before the first frame update
    void Start()
    {
        slimeManagerScript = FindObjectOfType<SlimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSlime = slimeManagerScript.CurrentSlime;
        thisSlime = GetComponentInParent<Slime>();
        this.transform.localScale = crownScales[thisSlime.GetComponent<Slime>().SlimeSize];
        transform.localPosition = new Vector3(transform.localPosition.x, crownPos[thisSlime.GetComponent<Slime>().SlimeSize].y, transform.localPosition.z);


        if (currentSlime == thisSlime)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
        }
        else 
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
        
    }
}
