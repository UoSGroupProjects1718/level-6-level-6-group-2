using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollBehaviour : MonoBehaviour
{
    public int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
    public string textureName = "_MainTex";

    Vector2 uvOffset = Vector2.zero;

	public Renderer rendr;

	void Start()
	{
		rendr = GetComponent<Renderer> ();
	}

    void LateUpdate()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (rendr.enabled)
        {
            rendr.materials[materialIndex].SetTextureOffset(textureName, uvOffset);
			
        }

    }
}
