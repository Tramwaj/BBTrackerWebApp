using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBTracker.Contracts.ViewModels
{
    public class PlayerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }
        public string City { get; set; }

        public PlayerDTO()
        {

        }
        public PlayerDTO(Guid id, string name, string nick)
        {
            Id = id;
            Name = name;
            Nick = nick;
            City = "Bielsko-Biała";
        }

        public PlayerDTO(Guid id, string name, string nick, string city) : this(id, name, nick)
        {
            City = city;
        }
    }
    public class PlayerDTOValidator : AbstractValidator<PlayerDTO>
    {
        public PlayerDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(3, 20);
            RuleFor(x => x.Nick).NotEmpty().Length(2, 30);
            RuleFor(x => x.City).NotEmpty().Length(2, 30);
        }
    }
}
