using System.Collections.Generic;
using UnityEngine;

public class MapGraph
{
    //definir root
    public MapGraphNode<IBlockGenerator> root;

    //Agrupar nodos
    private List<MapGraphNode<IBlockGenerator>> _allNodes = new List<MapGraphNode<IBlockGenerator>>();

    //Funcion para agregar nodos
    public MapGraphNode<IBlockGenerator> AddComponent(IBlockGenerator component)
    {
        //Al usar var toma implicitamente el tipo de dato del lado derecho
        //solo usen var , cuando estes seguro de que es lo que contiene, o se pone de forma explicita

        var node = new MapGraphNode<IBlockGenerator>(component);

        _allNodes.Add(node);
        if (root==null)
        {
            root = node;
        }
        return node;
    }

    //necesito definir la posicion absoluta de cada nodo apartir del root
    public Dictionary<MapGraphNode<IBlockGenerator>, Vector3Int> CalculatePositions()
    {
        //inicializar diccionario vacio
        Dictionary<MapGraphNode<IBlockGenerator>, Vector3Int> positions = new Dictionary<MapGraphNode<IBlockGenerator>, Vector3Int>();
        Traverse(root, Vector3Int.zero, positions);
        return positions;
    }
    //Calcular la posicion apartir de nodo Root
    private void Traverse(MapGraphNode<IBlockGenerator>node, Vector3Int currentPos, Dictionary<MapGraphNode<IBlockGenerator>, Vector3Int> positions)
    {
        //Validar que la poscisiones tengan nodos
        //condicion de salida ES LO PRIMERO QUE DEBE DEFINIERSE EN UNA FUNCION RECURSIVA
        if (positions.ContainsKey(node)) return;

        positions[node] = currentPos;

        //para cada edge en el nodo, calcular la poscion atraves de una funcion recursiva
        foreach (var edge in node.Neighbors)
        {
            Traverse(edge.target, currentPos + edge.offset, positions);
        }

    }
}