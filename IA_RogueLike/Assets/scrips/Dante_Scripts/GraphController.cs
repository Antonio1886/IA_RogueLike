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
        //crear nodos
        var lobby = graph.AddComponent(new RectangleGenerator(10, 10));
        var hallway1 = graph.AddComponent(new RectangleGenerator(2, 6));
        var hallway2 = graph.AddComponent(new RectangleGenerator(2, 6));
        //crear conecciones
        lobby.ConnectTo(hallway1, Direction.North,10);
        lobby.ConnectTo(hallway2, Direction.South,5);

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

    
}
