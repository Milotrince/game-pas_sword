using UnityEngine;

public class SwordRenderer : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    private Sword _sword;


    public void SetSword(Sword sword)
    {
        this._sword = sword;
        DrawSwordSprite();
    }

    private void DrawSwordSprite()
    {
        string password = _sword.Password.String;

        // TODO: make not arbitrary calculation
        Color color = Color.HSVToRGB(Random.value, 1, 1);
        int width = Mathf.Clamp( Mathf.RoundToInt(_sword.BaseDamage/ 100f), 1, 5 );

        Vector2Int swordSize = new Vector2Int(width, _sword.Password.String.Length);

        // texture size oriented →, sword size oriented ↓
     
        Vector2Int textureSize = new Vector2Int(swordSize.y, swordSize.x+2);
        Texture2D texture = new Texture2D(textureSize.x, textureSize.y, TextureFormat.RGBA32, false);

        Color[] allClearPixels = new Color[textureSize.x * textureSize.y];
        for (int i = 0; i < allClearPixels.Length; i++)
        {
            allClearPixels[i] = Color.clear;
        }
        texture.SetPixels(allClearPixels);

        // Hilt
        texture.SetPixel(1, 0, color);
        texture.SetPixel(1, swordSize.x+1, color);

        // Blade
        for (int i = 1; i < swordSize.x+1; i++)
        {
            for (int j = 0; j < swordSize.y; j++)
            {
                texture.SetPixel(j, i, color);
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.Apply();

        Rect rect = new Rect(Vector2.zero, textureSize);
        Vector2Int pivot = new Vector2Int(0, Mathf.CeilToInt(swordSize.x/2f));
        Sprite swordSprite = Sprite.Create(texture, rect, pivot, 8f);

        SpriteRenderer.sprite = swordSprite;
    }

}
