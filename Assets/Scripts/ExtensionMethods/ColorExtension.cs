using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class ColorExtension
{
    public static void Alpha_SpriteRenderer(this SpriteRenderer spriteRenderer, float alpha)
    {
        Color temp = spriteRenderer.color;
        spriteRenderer.color = new Color(temp.r, temp.g, temp.b, alpha);
    }

    public static Color Alpha_Color(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }

    public static void Alpha(this TextMeshProUGUI text, float alpha)
    {
        text.alpha = alpha;
    }

    public static void Alpha(this TextMeshPro text, float alpha)
    {
        text.alpha = alpha;
    }
}
