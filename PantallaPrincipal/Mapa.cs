using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto
{
    public partial class Mapa : Form
    {
        public static Mapa CurrentMapa { get; set; }
        public int SelectedX { get; private set; }
        public int SelectedY { get; private set; }
        public bool MapaClick = false;

        private Point startPoint = Point.Empty;
        private Point endPoint = Point.Empty;
        private int cX, cY;

        private Bitmap originalBackgroundImage;

        private const int GridSize = 2;
        private Node[,] grid;
        private Node startNode;
        private Node endNode;

        public Mapa()
        {
            InitializeComponent();
            originalBackgroundImage = new Bitmap(BackgroundImage);
            InitializeGrid();
            InitializeMap();
            IdentifyWalls();
            BackgroundImage = null;
            CurrentMapa = this;
            DoubleBuffered = true;

        }

        public void InitializeMap()
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
                            grid[x, y] = new Node(x, y);
                            grid[x, y].IsWall = true;
                            grid[x, y].IsUnwalkableFloor = true;
                        }
                        else
                        {
                            grid[x, y] = new Node(x, y);
                        }
                    }
                }
            }
            
            startNode = grid[408 / GridSize, 541 / GridSize];
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
            int distanceX = Math.Abs(a.X - b.X);
            int distanceY = Math.Abs(a.Y - b.Y);

            return distanceX + distanceY;
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

        private void btnFindPath_Click(object sender, EventArgs e)
        {
            FindPath();
            Invalidate();
        }

        private void Mapa_MouseClick(object sender, MouseEventArgs e)
        {

            /*string coordenada = $"{e.X}, {e.Y}";

            try
            {
                // Abre o crea un archivo de texto
                string rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "coordenadas.txt");

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(rutaArchivo, true))
                {
                    // Escribe la coordenada en el archivo
                    file.WriteLine(coordenada);
                }
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir al escribir en el archivo
                MessageBox.Show("Error al escribir en el archivo: " + ex.Message);
            }*/

            if (e != null)
            {
                int x = e.X / GridSize;
                int y = e.Y / GridSize;

                if (endPoint == Point.Empty)
                {
                    endPoint = new Point(x, y);
                    endNode = grid[x, y];
                    DrawPoint(new Point(x * GridSize, y * GridSize), Color.Red);
                    MapaClick = true;
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

        public void SetNodoFinal(int x, int y)
        {
            cX = x;
            cY = y;
            endNode = grid[cX, cY];
            Invalidate();
        }
    }
}