public class Room
{
    protected Map map;
    protected (int, int) pos;
    protected RoomType type;
    protected int level;
     

    public Map Map => map;
    public (int, int) Pos => pos;
    public RoomType Type => type;
    public int Level => level;
    public int x
    {
        get => pos.Item1;
        protected set => pos = (value, pos.Item2);
    }
    public int y
    {
        get => pos.Item2;
        protected set => pos = (pos.Item1, value);
    }

    public Room(Map map, int x, int y, int level, RoomType type = RoomType.Intermediate)
    {
        this.map = map;
        this.map[x, y] = this;
        pos = (x, y);
        this.level = level;
        this.type = type;
    }

    public void SetType(RoomType type) => this.type = type;

    protected int minDepth => level * level * level - 7 * level * level + 16 * level - 6;
    protected int maxDepth => - level * level + 7 * level + 2;
    public virtual void Generate(int depth = 0){}

    public bool isFull => false;

    public override string ToString()
    {
        return type switch {RoomType.StartRoom => "S", RoomType.Intermediate => "I", RoomType.BossRoom => "B", _ => "?"};
    }
}