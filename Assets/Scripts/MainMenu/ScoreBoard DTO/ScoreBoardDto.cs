using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardDto
{
    public string Name { get; set; } = string.Empty;
    public int Puntuacion { get; set; }

    // Sin el api
    public ScoreBoardDto(string name, int puntuacion)
    {
        this.Name = name;
        this.Puntuacion = puntuacion;
    }
}