using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightController : MonoBehaviour
{
    public Light2D playerLight;              // �����ڂ̃��C

    public float smallRadius = 3f;
    public float largeRadius = 6f;

    private bool isLarge = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) // L�L�[�Ő؂�ւ�
        {
            isLarge = !isLarge;
            float radius = isLarge ? largeRadius : smallRadius;

            // ���C�g�̔��a��ύX
            if (playerLight != null)
                playerLight.pointLightOuterRadius = radius;

            
        }
    }
}
