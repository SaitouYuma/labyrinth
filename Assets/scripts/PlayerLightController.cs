using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightController : MonoBehaviour
{
    public Light2D playerLight;              // 見た目のライ

    public float smallRadius = 3f;
    public float largeRadius = 6f;

    private bool isLarge = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) // Lキーで切り替え
        {
            isLarge = !isLarge;
            float radius = isLarge ? largeRadius : smallRadius;

            // ライトの半径を変更
            if (playerLight != null)
                playerLight.pointLightOuterRadius = radius;

            
        }
    }
}
