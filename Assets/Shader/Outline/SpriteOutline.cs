using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class SpriteOutline : MonoBehaviour {
    public Color color = Color.white;

    [Range(0, 16)]
    public int outlineSize = 1;

    [SerializeField] Material material;
    [SerializeField] bool outLine;

    // [SerializeField] bool isSpriteRenderer;
    private SpriteRenderer spriteRenderer;
    // private Image img;

    void OnEnable() {
        // if(isSpriteRenderer)
            spriteRenderer = GetComponent<SpriteRenderer>();
        // else
        //     img=GetComponent<Image>();

        UpdateOutline(outLine);
    }

    void OnDisable() {
        UpdateOutline(false);
    }

    void Update() {
        UpdateOutline(outLine);
    }

    void UpdateOutline(bool outline) {
        // if(isSpriteRenderer){
            MaterialPropertyBlock mpb = new MaterialPropertyBlock();
            spriteRenderer.GetPropertyBlock(mpb);
            mpb.SetFloat("_Outline", outline ? 1f : 0);
            mpb.SetColor("_OutlineColor", color);
            mpb.SetFloat("_OutlineSize", outlineSize);
            spriteRenderer.SetPropertyBlock(mpb);
        // }
        // else{
        //     material.SetFloat("_Outline", outline ? 1f : 0);
        //     material.SetColor("_OutlineColor", color);
        //     material.SetFloat("_OutlineSize", outlineSize);
        //     img.material=material;
        // }
    }
}