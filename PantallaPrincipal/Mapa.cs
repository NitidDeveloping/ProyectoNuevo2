using CapaEntidades;
using AulaGO.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AulaGO
{
    public partial class Mapa : Form
    {
        public static Mapa CurrentMapa { get; private set; }
        public int CurrentPiso { get; private set; }

        public int SelectedX { get; private set; }
        public int SelectedY { get; private set; }

        //Leer las coordenadas desde el archivo de texto
        private readonly string[] coordenadasPlantaBaja = File.ReadAllLines("CoordPB.txt");

        public bool MapaClick = false;

        private Point startPoint = Point.Empty;
        private Point endPoint = Point.Empty;
        private int cX, cY;

        private Bitmap originalBackgroundImage;

        private const int GridSize = 2;
        private Node[,] grid;
        public Node startNode;
        private Node endNode;

        public Mapa()
        {
            InitializeComponent();
            originalBackgroundImage = new Bitmap(BackgroundImage);
            InitializeGrid();
            InitializeMap(0);
            IdentifyWalls();
            BackgroundImage = null;
            CurrentMapa = this;
            CurrentMapa.CurrentPiso = 0;
            DoubleBuffered = true;
        }
        public void InitializeMap(int mapaSeleccionado)
        {
            if (BackgroundImage != null)
            {
                int width = BackgroundImage.Width;
                int height = BackgroundImage.Height;

                grid = new Node[width, height];

                Bitmap bitmap = new Bitmap(BackgroundImage);

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color pixelColor = bitmap.GetPixel(x, y);

                        if (pixelColor.ToArgb() == Color.Black.ToArgb())
                        {
                            grid[x, y] = new Node(x, y)
                            {
                                IsWall = true,
                                IsUnwalkableFloor = true
                            };
                        }
                        else
                        {
                            grid[x, y] = new Node(x, y);
                        }
                    }
                }
            }
            if (mapaSeleccionado == 0)
            {
                if (coordenadasPlantaBaja.Length == 2)
                {
                    int cX = int.Parse(coordenadasPlantaBaja[0]); //Coordenadas para la planta baja
                    int cY = int.Parse(coordenadasPlantaBaja[1]);
                    startNode = grid[cX / GridSize, cY / GridSize];
                    Menú.rbPB.Checked = true;
                }
            }
            else if (mapaSeleccionado == 1)
            {
                startNode = grid[409 / GridSize, 548 / GridSize]; //Coordenadas para el piso 1
                Menú.rbP1.Checked = true;
            }
            else if (mapaSeleccionado == 2)
            {
                startNode = grid[329 / GridSize, 667 / GridSize]; //Coordenadas para el piso 2
                Menú.rbP2.Checked = true;
            }
        }
        private void IdentifyWalls()
        {
            if (BackgroundImage != null)
            {
                Bitmap bitmap = new Bitmap(BackgroundImage);

                // Color hexadecimal que deseas reconocer como piso (por ejemplo, #F4F4F4)
                Color floorColor = ColorTranslator.FromHtml("#F4F4F4");

                for (int x = 0; x < bitmap.Width; x++)
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        Color pixelColor = bitmap.GetPixel(x, y);

                        // Convierte los píxeles en escala de grises a píxeles negros
                        int grayscale = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                        // Compara el color del píxel con el color objetivo
                        if (grayscale < 128)
                        {
                            bitmap.SetPixel(x, y, Color.Black);
                            grid[x, y].IsWall = true;
                        }

                        if (pixelColor == floorColor)
                        {
                            grid[x, y].IsUnwalkableFloor = true;
                        }
                    }
                }
                BackgroundImage = bitmap;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawGrid(e.Graphics);
        }
        private void DrawPoint(Point point, Color color)
        {
            if (BackgroundImage != null)
            {
                Bitmap bitmap = (Bitmap)BackgroundImage.Clone(); // Clonar la imagen antes de dibujar
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.FillEllipse(new SolidBrush(color), point.X - 2, point.Y - 2, 5, 5);
                }
                BackgroundImage = bitmap;
            }
        }
        private void DrawGrid(Graphics g)
        {
            int nodeSize = GridSize; // Doble del tamaño actual

            foreach (Node node in grid)
            {
                // Dibuja las paredes de fondo primero
                if (node.IsWall)
                {
                    g.FillRectangle(Brushes.Black, node.X * GridSize, node.Y * GridSize, GridSize, GridSize);
                    continue; // Continúa al siguiente nodo sin procesar los nodos de inicio, fin o camino
                }

                if (node.IsUnwalkableFloor)
                {
                    g.FillRectangle(Brushes.WhiteSmoke, node.X * GridSize, node.Y * GridSize, GridSize, GridSize);
                    continue; // Continúa al siguiente nodo sin procesar los nodos de inicio, fin o camino
                }

                Brush brush = Brushes.White;
                if (node == startNode)
                {
                    brush = Brushes.Green;
                    g.FillRectangle(brush, node.X * GridSize, node.Y * GridSize, nodeSize, nodeSize);
                }
                else if (node == endNode)
                {
                    brush = Brushes.Red;
                    g.FillRectangle(brush, node.X * GridSize, node.Y * GridSize, nodeSize, nodeSize);
                }
                else if (node.IsPath)
                {
                    brush = Brushes.Blue;
                    g.FillRectangle(brush, node.X * GridSize, node.Y * GridSize, nodeSize, nodeSize);
                }
            }
        }
        private void InitializeGrid()
        {
            grid = new Node[ClientSize.Width, ClientSize.Height];

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = new Node(x, y);
                }
            }
        }
        public void FindPath()
        {
            foreach (Node node in grid)
            {
                node.Reset();
            }

            HashSet<Node> closedSet = new HashSet<Node>();
            HashSet<Node> openSet = new HashSet<Node>();

            startNode.GCost = 0;
            startNode.HCost = GetDistance(startNode, endNode);
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.OrderBy(node => node.FCost).First();

                if (currentNode == endNode)
                {
                    ReconstructPath(currentNode);
                    return;
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                foreach (Node neighbor in GetNeighbors(currentNode))
                {
                    if (neighbor.IsWall || closedSet.Contains(neighbor))
                        continue;

                    if (neighbor.IsUnwalkableFloor || closedSet.Contains(neighbor))
                        continue;

                    int tentativeGCost = currentNode.GCost + GetDistance(currentNode, neighbor);
                    if (tentativeGCost < neighbor.GCost || !openSet.Contains(neighbor))
                    {
                        neighbor.Parent = currentNode;
                        neighbor.GCost = tentativeGCost;
                        neighbor.HCost = GetDistance(neighbor, endNode);

                        if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);
                    }
                }
            }

            ReconstructPath(endNode);
            Invalidate();
        }
        private List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();

            int[] xOffset = { -1, 0, 1, 0 };
            int[] yOffset = { 0, 1, 0, -1 };

            for (int i = 0; i < 4; i++)
            {
                int x = node.X + xOffset[i];
                int y = node.Y + yOffset[i];

                if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
                    neighbors.Add(grid[x, y]);
            }

            return neighbors;
        }
        private int GetDistance(Node a, Node b)
        {
            if (a != null && b != null)
            {
                int distanceX = Math.Abs(a.X - b.X);
                int distanceY = Math.Abs(a.Y - b.Y);

                return distanceX + distanceY;
            }
            else
            {
                return 0;
            }
        }
        private void ReconstructPath(Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            path.Reverse();

            foreach (Node node in path)
            {
                if (node != startNode && node != endNode)
                    node.IsPath = true;
            }
        }
        private void Mapa_MouseClick(object sender, MouseEventArgs e)
        {
            MapaClick = true;

            if (e != null)
            {
                int x = e.X / GridSize;
                int y = e.Y / GridSize;

                if (endPoint == Point.Empty)
                {
                    //endPoint = new Point(x, y);
                    endNode = grid[x, y];
                    // DrawPoint(new Point(x * GridSize, y * GridSize), Color.Red);
                    Invalidate();

                }
                else
                {
                    ClearPoints();
                    endPoint = new Point(x, y);
                    endNode = grid[x, y];
                    DrawPoint(new Point(x * GridSize, y * GridSize), Color.Red);
                    Invalidate();
                }
                SelectedX = x;
                SelectedY = y;
            }
        }
        public void ClearPoints()
        {
            if (startPoint != null)
            {
                endPoint = Point.Empty;
                endNode = null;
            }
        }
        public void CambiarMapa(int mapaSeleccionado)
        {
            switch (mapaSeleccionado)
            {
                case 0:
                    BackgroundImage = Resources.planta_baja;
                    break;
                case 1:
                    BackgroundImage = Resources.piso_1;
                    break;
                case 2:
                    BackgroundImage = Resources.piso_2;
                    break;
                default:
                    BackgroundImage = Resources.planta_baja;
                    break;
            }

            InitializeGrid();
            InitializeMap(mapaSeleccionado);
            IdentifyWalls();
            Invalidate();
            BackgroundImage = null;
            CurrentPiso = mapaSeleccionado;
            CurrentMapa = this;
        }      
        public void SetNodoFinal(int x, int y, int piso)
        {
            int X = int.Parse(coordenadasPlantaBaja[0]); //Coordenadas para la planta baja
            int Y = int.Parse(coordenadasPlantaBaja[1]);

            if (piso == CurrentMapa.CurrentPiso)
            {
                cX = x;
                cY = y;
                endNode = grid[cX, cY];
                Invalidate();
            }
            else
            {
                int diferenciaPisos = Math.Abs(piso - CurrentPiso);
                string mensaje = piso > CurrentPiso ? $"suba {diferenciaPisos} piso(s)" : $"baje {diferenciaPisos} piso(s)";
                MsgBox msg = new MsgBox("aviso", $"Diríjase a las escaleras más cercanas, {mensaje} y haga el siguiente recorrido (a partir del tótem)");
                if (msg.ShowDialog() == DialogResult.OK)
                {
                    CurrentMapa.CurrentPiso = piso;
                    CambiarMapa(piso);

                    // Establecer el punto de inicio en la ubicación actual del usuario
                    if (piso == 0)
                    {
                        cX = x;
                        cY = y;

                        startNode = grid[622 / GridSize, 576 / GridSize];
                        endNode = grid[cX, cY];
                        Invalidate();
                    }
                    if (piso == 1 && X == 896 && Menú.rbP1.Checked)
                    {
                        cX = x;
                        cY = y;

                        startNode = grid[926 / GridSize, 517 / GridSize];
                        endNode = grid[cX, cY];
                        Invalidate();
                    }
                    if (piso == 1 && X == 609 && Menú.rbP1.Checked)
                     {
                        cX = x;
                        cY = y;

                        startNode = grid[388 / GridSize, 566 / GridSize];
                        endNode = grid[cX, cY];
                        Invalidate();
                     }


                    if (piso == 2 && X == 896)
                    {
                        cX = x;
                        cY = y;

                        startNode = grid[888 / GridSize, 547 / GridSize];
                        endNode = grid[cX, cY];
                        Invalidate();
                    }
                    if (piso == 2 && X == 609)
                    {
                        cX = x;
                        cY = y;

                        startNode = grid[303 / GridSize, 614 / GridSize];
                        endNode = grid[cX, cY];
                        Invalidate();
                    }
                    FindPath();
                }
            }
        }
    }
}
