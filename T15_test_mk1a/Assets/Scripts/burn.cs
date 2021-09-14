using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burn : MonoBehaviour
{
    //zelf in te vullen waarden
    [SerializeField]
    public float burn_speed;
    public float wax_gain_speed;
    public float begin_size;
    public GameObject schotel; //de basis voor de kaars
    public Light flame; //het licht object

    //vaste waarden
    private float burn_in_tick;
    private float actual_height;
    private float minimum_height = 1;
    private float maximum_height = 12;

    void Start()
    {
        //het onthouden van wat de start-stand is voor de kaars
        actual_height = begin_size;
        flame.intensity = 1;
    }

    void FixedUpdate()
    {
        //de burnspeed
        burn_in_tick = burn_speed;
        actual_height -= burn_in_tick;

        //het checken van de collision, alles wat overlapt met de kaars wordt in een lijst gegooid
        Collider[] InRangeFuel = Physics.OverlapSphere(this.transform.position, 0f);

        //uit de lijst met overlaps wordt gekeken welk onderdeel de tag "FuelPoint" heeft zodat de kaars niet door alles wordt opgeladen
        foreach (var fuelPoint in InRangeFuel)
        {
            if (fuelPoint.gameObject.CompareTag("FuelPoint") && actual_height < maximum_height)
            {
                //het toevoegen van meer kaarsvet
                actual_height += wax_gain_speed;
                burn_in_tick = 0;
            }
        }

        //dit zorgt ervoor dat de kaars niet te klein wordt of omgekeerd gaat groeien
        if (actual_height < minimum_height)
        {
            actual_height = minimum_height;
        }

        //de waardes van het licht aanpassen, de range en intensity
        flame.intensity = (actual_height - minimum_height) / begin_size;
        flame.range = ((actual_height - minimum_height) / begin_size) * 20;

        //de lengte van de kaars aanpassen
        this.transform.localScale = new Vector3(1, -actual_height, 1);
    }
}
