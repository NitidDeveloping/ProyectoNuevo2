﻿using CapaEntidades;
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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto
{
    public partial class Mapa : Form
    {
        private Point startPoint = Point.Empty;
        private Point endPoint = Point.Empty;

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

            DoubleBuffered = true;

        }

        private void InitializeMap()
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
                        }
                        else
                        {
                            grid[x, y] = new Node(x, y);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("La imagen no se ha cargado correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IdentifyWalls()
        {
            if (BackgroundImage != null)
            {
                Bitmap bitmap = new Bitmap(BackgroundImage);
                int wallThreshold = 128;

                for (int x = 0; x < bitmap.Width; x++)
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        Color pixelColor = bitmap.GetPixel(x, y);
                        int grayscale = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                        if (grayscale < wallThreshold)
                        {
                            bitmap.SetPixel(x, y, Color.Black);
                        }
                    }
                }

                BackgroundImage = bitmap;
            }
            else
            {
                MessageBox.Show("La imagen no se ha cargado correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            int nodeSize = GridSize * 4; // Doble del tamaño actual

            foreach (Node node in grid)
            {
                // Dibuja las paredes de fondo primero
                if (node.IsWall)
                {
                    g.FillRectangle(Brushes.Black, node.X * GridSize, node.Y * GridSize, GridSize, GridSize);
                    continue; // Continúa al siguiente nodo sin procesar los nodos de inicio, fin o camino
                }

                // Dibuja nodos directamente en el PictureBox
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

        private void FindPath()
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
                    {
                        continue;
                    }

                    int tentativeGCost = currentNode.GCost + GetDistance(currentNode, neighbor);
                    if (tentativeGCost < neighbor.GCost || !openSet.Contains(neighbor))
                    {
                        neighbor.Parent = currentNode;
                        neighbor.GCost = tentativeGCost;
                        neighbor.HCost = GetDistance(neighbor, endNode);

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
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
                {
                    neighbors.Add(grid[x, y]);
                }
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
                {
                    node.IsPath = true;
                }
            }
        }

        private void btnFindPath_Click(object sender, EventArgs e)
        {
            FindPath();
            Invalidate();
        }

        private void Mapa_MouseClick(object sender, MouseEventArgs e)
        {

            /* string coordenada = $"{e.X}, {e.Y}";

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


            MouseEventArgs mouseEvent = e;


            if (mouseEvent != null)
            {
                int x = mouseEvent.X / GridSize;
                int y = mouseEvent.Y / GridSize;

                if (startPoint == Point.Empty)
                {
                    // Establecer el punto de inicio en el primer clic
                    startPoint = new Point(x, y);
                    // Configurar startNode con las coordenadas
                    startNode = grid[x, y]; // O donde tengas almacenado el nodo en map
                                            // Dibujar el punto de inicio en la imagen
                    DrawPoint(new Point(x * GridSize, y * GridSize), Color.Blue);
                    Invalidate();
                }
                else if (endPoint == Point.Empty)
                {
                    // Establecer el punto final en el segundo clic
                    endPoint = new Point(x, y);
                    // Configurar endNode con las coordenadas
                    endNode = grid[x, y]; // O donde tengas almacenado el nodo en map
                                          // Dibujar el punto final en la imagen
                    DrawPoint(new Point(x * GridSize, y * GridSize), Color.Red);
                    Invalidate();
                }
                else
                {
                    // Ambos puntos ya han sido establecidos, reiniciar si se hace clic nuevamente
                    startPoint = Point.Empty;
                    endPoint = Point.Empty;

                    // Borrar cualquier punto dibujado previamente
                    ClearPoints();
                    Invalidate();
                }
            }
        }
        private void ClearPoints()
        {
            if (startNode != null)
            {
                startNode = null;
            }

            if (endNode != null)
            {
                endNode = null;
            }

            if (BackgroundImage != null)
            {
                BackgroundImage.Dispose(); // Liberar la imagen actual
                BackgroundImage = (Image)originalBackgroundImage.Clone(); // Restaurar la imagen original

                foreach (Node node in grid)
                {
                    node.Reset();
                }
            }
        }
    }
}

