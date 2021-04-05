using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject ShopButtonPrefab;
    public GameObject ShopButtonContainer;
    public Material PlayerMaterial;
    void Start()
    {
        ChangePlayerSkin(1);
        int textureIndex = 0;
        Sprite[] textures = Resources.LoadAll<Sprite>("Shop");
        foreach(Sprite texture in textures)
        {
            GameObject container = Instantiate (ShopButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = texture;
            container.transform.SetParent(ShopButtonContainer.transform, false);
            
            int index = textureIndex;
            container.GetComponent<Button>().onClick.AddListener(() => ChangePlayerSkin(index));
            textureIndex++;
        }
    }

    private void ChangePlayerSkin(int index)
    {
        float x = (index % 4) * 0.25f;
        float y = ((int)index / 4) * 0.25f;

        y = 0.75f - y;   
        PlayerMaterial.SetTextureOffset("_MainTex", new Vector2 (x, y));
    }
}
