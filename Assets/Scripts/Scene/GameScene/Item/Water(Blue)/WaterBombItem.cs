using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBombItem : Item
{
    // Start is called before the first frame update
    public GameObject skill;
    
    void Update()
    {
        //sizeBounceEffect();
    }
    
    public override void Use(GameObject target)
    {
        AudioManager.instance.PlaySFX("WaterBombSkill");
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, 0); 

        Instantiate(skill, newPos, Quaternion.identity);

        Destroy(gameObject);
    }
}
