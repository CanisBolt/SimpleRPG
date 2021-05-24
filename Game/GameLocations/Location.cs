namespace Game.GameLocations
{
    public class Location
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public Region Region { get; set; }
        public bool IsCheckpoint { get; set; }
        public Shop ShopOnLocation { get; set; }
        public NPC NPCOnLocation { get; set; }
    }
}
