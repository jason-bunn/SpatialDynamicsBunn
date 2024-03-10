using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePaintSplatter : MonoBehaviour
{
    public bool stream = false;
    public bool puffs = false;
    public Camera cam;
    [Space]
    public bool mouseSingleClick;
    [Space]
    public Color paintColor;

    public ParticleSystem paintParticles;
    public GameObject puffsPrefab;

    private Transform particleParent;

    private void Start()
    {
        particleParent = paintParticles.transform.parent;
    }

    void Update()
    {

        bool click;
        click = mouseSingleClick ? Input.GetMouseButtonDown(0) : Input.GetMouseButton(0);

        if (click)
        {
          
            
            Vector3 position = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                
                Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red);
                Vector3 pos = hit.point;

                if(stream)
                {
                    particleParent.LookAt(hit.point);
                    if (!paintParticles.isPlaying)
                    {
                        paintParticles.Play();
                    }
                }
                else if (puffs)
                {
                    GameObject puff = Instantiate(puffsPrefab, hit.point, Quaternion.identity);
                    var ps = puff.transform.Find("Particle System").GetComponent<ParticleSystem>();
                    ps.startColor = paintColor;

                    ps.Play();
                    Destroy(puff, 5f);
                }
            }
        }
        else
        {
            if(paintParticles.isPlaying)
            {
                paintParticles.Stop();
            }
        }

    }
}
