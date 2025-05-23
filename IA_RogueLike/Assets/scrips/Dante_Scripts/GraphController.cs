using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GraphController : MonoBehaviour
{
    //public Tile tile;
    public Tilemap tilemap;

    public List<Tile> tilesForFactory;

    public int minimoDeHabitacionesPorRoot = 0; //Minimo de habitaciones por root

    public int habitacionesPorRoot =  4; 

    //List<Vector2Int> nodosCoords = new List<Vector2Int>();
    Dictionary<Vector2Int, MapGraphNode<IBlockGenerator>> nodosConectados = new Dictionary<Vector2Int, MapGraphNode<IBlockGenerator>>(); //Para guardar los nodos conectados

    public int bloqueoDeSeguridad=3;

    private List<IBlockGenerator> _bloques = new List<IBlockGenerator>() 
    {
        new RectangleGenerator(15, 15),

        new RectangleGenerator(10,10),

        new CircleGenerator(15), //Boss

    };

    private List<IBlockGenerator> _pasillos = new List<IBlockGenerator>()
    {
        new RectangleGenerator(2, 6), // North & South
        new RectangleGenerator(6, 2), // East & West
        
        new RectangleGenerator(3, 30), // North & South Boss
        new RectangleGenerator(30, 3), // East & West Boss
    };

    private MapGraphNode<IBlockGenerator> ultimoNodeBloque;
    private Direction ultimaDirection;

    void Start()
    {
        //crear grapho
        MapGraph graph = new MapGraph();

        //Crear nodo root
        var root = graph.AddComponent(new RectangleGenerator(20, 20));
        //nodosCoords.Add(Vector2Int.zero);
        nodosConectados.Add(Vector2Int.zero, root);
        CrearMapa(graph, root, Vector2Int.zero);

        //Crear sala de jefe
        if (ultimoNodeBloque != null || ultimaDirection == Direction.None)
        {
            CrearSalaJefe(graph, ultimaDirection, ultimoNodeBloque);
        }
        else if (nodosConectados.Count < 1)
        {
            CrearSalaJefe(graph, Direction.South, graph.root);
        }

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

            List<Tile> tilesBlank = new List<Tile>(tiles.Count);

        }
    }

    public void CrearMapa(MapGraph graph, MapGraphNode<IBlockGenerator> newRoot, Vector2Int coords)
    {
        if (bloqueoDeSeguridad<=0)
        {
            return;
        }
        //Creo variable random
        int habitacionesRandom = Random.Range(minimoDeHabitacionesPorRoot, habitacionesPorRoot); //0 a 3 habitaciones por root

        Vector2Int dir = Vector2Int.zero;

        MapGraphNode<IBlockGenerator> nodeBloque = null;

        //Crear Queue de direcciones
        Queue<Direction> directions = new Queue<Direction>();

        Direction direction = Direction.None;

        //Agregar direcciones a la queue

        directions.Enqueue(Direction.North);
        directions.Enqueue(Direction.South);
        directions.Enqueue(Direction.East);
        directions.Enqueue(Direction.West);

        //Checar si la direccion ya existe
        for (int i = 0; i < 4; i++)
        {
            dir = Vector2Int.zero;
            switch (directions.Peek())
            {
                case Direction.North:
                    dir = Vector2Int.up + coords;
                    break;
                case Direction.South:
                    dir = Vector2Int.down + coords;
                    break;
                case Direction.East:
                    dir = Vector2Int.right + coords;
                    break;
                case Direction.West:
                    dir = Vector2Int.left + coords;
                    break;
                default:
                    return;
            }
            if (nodosConectados.ContainsKey(dir))
            {
                //Debug.Log("Ya existe un nodo en la direccion: " + dir + " Quitando direccion: " + directions.Peek());
                directions.Dequeue();
            }
            else
            {
                if (directions.Count == 0 || habitacionesRandom <= 0)
                {
                    //Debug.Log("No hay direcciones disponibles: " + directions.Count);
                    //Debug.Log("No hay habitaciones disponibles: " + habitacionesRandom);

                    continue;
                }

                direction = directions.Dequeue();

                IBlockGenerator pasillo;

                if (direction == Direction.South || direction == Direction.North)
                {
                    pasillo = _pasillos[0];
                }
                else
                {
                    pasillo = _pasillos[1];
                }
                var nodePasillo = graph.AddComponent(pasillo);

                var bloque = _bloques[Random.Range(0, 2)];
                nodeBloque = graph.AddComponent(bloque);

                nodosConectados.Add(dir, nodeBloque);

                newRoot.ConnectTo(nodePasillo, direction);
                nodePasillo.ConnectTo(nodeBloque, direction);

                habitacionesRandom--;
            }
        }

        if (nodeBloque != null)
        {
            CrearMapa(graph, nodeBloque, dir);
        }

        bloqueoDeSeguridad--;

        ultimoNodeBloque = nodeBloque;
        ultimaDirection = direction;
    }

    private void CrearSalaJefe(MapGraph graph, Direction _direccion, MapGraphNode<IBlockGenerator> _bloque)
    {
        if (_bloque==null)
        {
            var root = graph.AddComponent(new RectangleGenerator(20, 20));
            if (nodosConectados.ContainsKey(Vector2Int.zero))
            {
                _bloque = nodosConectados[Vector2Int.zero];

            }
            else
            {
                _bloque = root;
            }
            _direccion = Direction.North;
        }
        MapGraphNode<IBlockGenerator> pasillo = null;

        //Crear pasillo de jefe
        if (_direccion == Direction.South || _direccion==Direction.North)
        {
            pasillo = graph.AddComponent(_pasillos[2]);
        }
        else if (_direccion == Direction.East || _direccion == Direction.West)
        {
            pasillo = graph.AddComponent(_pasillos[3]);
        }

        //Crear sala de jefe
        var salaJefe = graph.AddComponent(_bloques[2]);
        _bloque.ConnectTo(pasillo, _direccion);
        if (_direccion==Direction.South)
        {
            pasillo.ConnectTo(salaJefe, Direction.None);
            
        }
        else
        {
            pasillo.ConnectTo(salaJefe, _direccion);


        }
        //Debug.Log("Creando sala de jefe en la direccion: " + _direccion);
        //Debug.Log("Ultimo bloque: " + _bloque.value.GetType().Name);
    }
}
