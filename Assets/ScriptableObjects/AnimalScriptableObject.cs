using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAnimalAttributes", menuName = "Animals/AnimalAttributes")]

public class AnimalScriptableObject : ScriptableObject 
{
    public int scoreIncrease;
    public float speed;
}
