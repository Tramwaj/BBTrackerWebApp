namespace BBTracker.Model.Models
{
    //public class CheckOut
    //{
    //    public int id { get; set; }     
    //    public DateTime Time { get; set; }
    //    public Game Game { get; set; }
    //    public TimeSpan GameTime { get; set; }        
    //    public Player Player { get; set; }
    //}
    public class FieldGoal : Play
    {
        public int Points { get; set; }
        public bool Made { get; set; }
        public bool WasBlocked { get; set; }
        public bool WasAssisted { get; set; }
    }

}