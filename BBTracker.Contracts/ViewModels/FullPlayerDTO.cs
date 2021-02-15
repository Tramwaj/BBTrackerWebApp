using System;
using System.Collections.Generic;
using System.Text;

namespace BBTracker.Contracts.ViewModels
{
    public class FullPlayerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }
        public string City { get; set; }

        public FullPlayerDTO()
        {

        }
        public FullPlayerDTO(Guid id, string name, string nick)
        {
            Id = id;
            Name = name;
            Nick = nick;
            City = "Bielsko-Biała";
        }

        public FullPlayerDTO(Guid id, string name, string nick, string city) : this(id, name, nick)
        {
            City = city;
        }
    }
}
