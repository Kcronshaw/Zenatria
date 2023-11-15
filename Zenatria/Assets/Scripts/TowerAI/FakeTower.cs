using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTower : MonoBehaviour
{

    bool canPlace = true;
    int towerCost = 5;
    public SpriteRenderer spriteRenderer;
    public GameObject towerToBuild;


    void Start()
    {
        //TowerBuilder.instance.holdingTower = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePosition;

        if(canPlace == false)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.blue;
        }
    }

    private void OnMouseDown()
    {
        if (canPlace == true)
        {
            if (TowerBuilder.instance.money >= towerCost)
            {
                TowerBuilder.instance.money -= towerCost;
                Debug.Log(TowerBuilder.instance.money + " is current balance");
                PlaceTower();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Unplaceable")
        {
            canPlace = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canPlace = true;
    }


    void PlaceTower()
    {
        Instantiate(towerToBuild, transform.position, transform.rotation);
        TowerBuilder.instance.holdingTower = false;
        Destroy(this.gameObject);

    }
}
