namespace BBTracker.Model.Models
{
    public class Block : Play
    {
        public int Points { get; set; }
        public Player BlockedPlayer { get; set; }
    }

}