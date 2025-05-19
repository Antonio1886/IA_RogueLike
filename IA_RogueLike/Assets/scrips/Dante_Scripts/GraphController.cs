using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GraphController : MonoBehaviour
{
    //public Tile tile;
    public Tilemap tilemap;
    public List<Tile> tilesForFactory;
    void Start()
    {
        //crear grapho
        MapGraph graph = new MapGraph();
        /*
        //crear nodos
        var lobby = graph.AddComponent(new RectangleGenerator(10, 10));
        var lobby2 = graph.AddComponent(new RectangleGenerator(10, 10));
        var hallway1 = graph.AddComponent(new RectangleGenerator(2, 6));
        var hallway2 = graph.AddComponent(new RectangleGenerator(2, 6));
        var hallway3 = graph.AddComponent(new RectangleGenerator(6, 2));
        var hallway4 = graph.AddComponent(new RectangleGenerator(6, 2));
        //crear conecciones
        lobby.ConnectTo(hallway1, Direction.North);
        lobby.ConnectTo(hallway2, Direction.South);
        hallway1.ConnectTo(lobby2, Direction.North);
        lobby2.ConnectTo(hallway3, Direction.East);
        lobby2.ConnectTo(hallway4, Direction.West);
        */
        //Calcular posiciones
        Dictionary<MapGraphNode<IBlockGenerator>, Vector3Int> positions = graph.CalculatePositions();

        //Dibujar el mapa
        foreach (var p in positions)
        {
            var node = p.Key;
            var position = p.Value;

            var generator = node.value;
            //Obtener el nombre del generador
            string generatorName = generator.GetType().Name;
            //Ejecuta los pasos anteriores del procedural contoller
            MapBlock block = generator.Generate(position);

            //Si hay tile factory agragarlas
            block.SetTileFactory(new MonoColorFactory(tilesForFactory, 0));
            //owo
            List<Tile> tiles = block.GenerateTilesArray();
            tilemap.SetTiles(block.TilesPositions.ToArray(), tiles.ToArray());
        }
    }

    /*
 * 1 Ya aparecen los bloques donde deberian con la orientacion correcta // Terminado
 * 2 No estan centrados en relacion a la posicion de los nodos // Terminado
 * 3 Hay que automatizar la creacion de los nodos // En progreso...
 * Para automatizar la creacion de nodos:
 * -Crea muchos nodos
 * -Conectalos entre ellos
 * Sencillo verdad??
 * 
 * -Creas un nodo root
 * -Creas un numero aleatorio de pasillos (0,4)
 * -Conectas el nodo root con los pasillos (En cualquier direccion) 
 * (Los pasillos tienen que estar bien orientados 2,6 para N y S, 6,2 para E y W)
 * -Por cada pasillo creas otro nodo (No pueden repetir la direccion del pasillo)
 * -Por cada nodo creas un numero aleatorio de pasillos (0,3)
 * (No pueden repetir la direccion del nodo ni del pasillo anterior)
 * 
 */

    //Automatizacion de los nodos
    public void CreateNode(MapGraph graph, IBlockGenerator generator)
    {
        //Crear nodo
        var node = graph.AddComponent(generator);
        //Crear conecciones
        foreach (var direction in System.Enum.GetValues(typeof(Direction)))
        {
            if (direction is Direction)
            {
                node.ConnectTo(node, (Direction)direction);
            }
        }
    }


}
