using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MapGraphNode<T>
{
    public T value;
    public List<MapGraphEdge<T>> Neighbors = new List<MapGraphEdge<T>>();
    private Vector3Int _offset;

    //Constructor
    public MapGraphNode(T value)
    {
        this.value = value;
    }
    //Funcion que conecta nodos apartir de un offset
    public void ConnectTo(MapGraphNode<T> target,Vector3Int offset)
    {
        Neighbors.Add(new MapGraphEdge<T>(target, offset));
    }

    //Poliformismo (Multiples formas de ejecutar una funcion)
    //    En funciones se diferencian en los parametros de entrada
    public void ConnectTo(MapGraphNode<T> target, Direction direction, int distance = 0)
    {
        //TODO Crear una funciopn que apartir de una direction y un distance, me regrese un vec3 int

        DirectionalPlacer directionalPlacer = new DirectionalPlacer(distance);

        

        Vector3Int offset = directionalPlacer.GetAdjacentPosition(direction, distance);
        ConnectTo(target, offset);
    }


}
