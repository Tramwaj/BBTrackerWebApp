using System;

namespace BBTracker.Model.Models
{
    //staty graczy generują się przy zapytaniu (RAZ [wcześniej miało być zapytanie z DATETIME do bazy o nowsze playsy) )
    // gdy dochodzi play to, to gracze dowiaduja się że będą się musieli odswieżyć 
    public class Play
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public Game Game { get; set; }
        public TimeSpan GameTime { get; set; }
        public bool IsTeamB { get; set; }
#nullable enable
        public Player? Player { get; set; }
    }
}