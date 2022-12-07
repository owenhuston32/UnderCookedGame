using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObj : MonoBehaviour
{
    private Spawner spawner = null;

    public Spawner Spawner { get => spawner; set => spawner = value; }

}
