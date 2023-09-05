using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class PlayerMovement : MonoBehaviour
{
    protected Camera cam = Camera.main;
    protected float worldWidth = Camera.main.orthographicSize * 2 * ((float)Screen.width / Screen.height);
    protected float worldHeight = Camera.main.orthographicSize * 2;

    public abstract void HandleMovement(Player player);
    public abstract void UpdatePlayerInertia(Player player);

    protected void BindToScreenY(Player player)
    {
        float camPosY = cam.transform.position.y;
        float halfWolrdHeight = worldHeight / 2;
        float y = Mathf.Clamp(player.transform.position.y, camPosY - halfWolrdHeight, camPosY + halfWolrdHeight);
        player.transform.position = new Vector3(player.transform.position.x, y, 0);
    }

    protected void WrapScreenX(Player player)
    {
        float camPosX = cam.transform.position.x;
        float halfWorldWidth = worldWidth / 2;
        player.WrapCloneRight.position = new Vector3(player.transform.position.x + worldWidth, player.transform.position.y, 0);
        player.WrapCloneLeft.position = new Vector3(player.transform.position.x - worldWidth, player.transform.position.y, 0);

        if (player.transform.position.x > camPosX + halfWorldWidth) player.transform.position = new Vector3(camPosX - halfWorldWidth, player.transform.position.y, 0);


        if (player.transform.position.x < camPosX - halfWorldWidth) player.transform.position = new Vector3(camPosX + halfWorldWidth, player.transform.position.y, 0);
        
    }
}
