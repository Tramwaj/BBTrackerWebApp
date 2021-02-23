using System;

namespace BBTracker.Contracts.ViewModels
{
    public class GamePlayerDTO
    {
        public Guid Id { get; set; }
        public bool TeamB { get; set; }
        public bool OnCourt { get; set; }
    }
}
