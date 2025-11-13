using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPhoto : MonoBehaviour
{
    public Transform frame;
    public void Shoot()
    {
        Instantiate(Resources.Load("PhotoEffect"), frame);
    }
}
