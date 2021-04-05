using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfeiteiroSkillUlt : SkillClass
{
    public GameObject pathPoint;
    public GameObject end;

    void FixedUpdate()
    {
        RotacionarEAtacar(this.transform, true, true);
        if (atirando == true)
        {
            atirando = false;
            this.GenericShoot();
        }
    }

    public override void GenericShoot()
    {
        base.GenericShoot();
        var batedeira = Instantiate(projetil, pathPoint.transform.position, pathPoint.transform.rotation);
        batedeira.GetComponent<BatedeiraScript>().endLine = end.transform.position;
        ZerarRecarga();
    }
}
