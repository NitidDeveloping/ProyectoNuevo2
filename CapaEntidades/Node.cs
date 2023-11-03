namespace CapaEntidades
{
    public class Node
    {
        public int X { get; }
        public int Y { get; }
        public bool IsWall { get; set; }
        public bool IsUnwalkableFloor { get; set; }
        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost => GCost + HCost;
        public Node Parent { get; set; }
        public bool IsPath { get; set; }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Reset()
        {
            GCost = 0;
            HCost = 0;
            Parent = null;
            IsPath = false;
        }
    }
}
