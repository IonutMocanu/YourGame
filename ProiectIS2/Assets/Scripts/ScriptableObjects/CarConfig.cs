using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName ="Unnamed Car",menuName ="CarConfig",order =0)]
public class CarConfig : ScriptableObject
{
    public string manufacturer;
    public string model;
    public int year;
    public int speed;
    public int price;
}
