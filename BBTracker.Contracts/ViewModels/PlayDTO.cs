using BBTracker.Common;
using FluentValidation;
using System;
using System.Security.Cryptography.X509Certificates;

namespace BBTracker.Contracts.ViewModels
{
    public class PlayDTO
    {
        public string PlayType { get; set; }
        public bool IsTeamB { get; set; }
        public Guid PlayerId { get; set; }

        public bool MainBoolProperty { get; set; }
        public int Points { get; set; }
        //public Guid PlayerOtherId { get; set; }

        //public TimeSpan GameTime { get; set; }
        public PlayDTO()
        {
        }
        public PlayDTO(string playType, bool isTeamB, Guid playerId)
        {
            PlayType = playType;
            IsTeamB = isTeamB;
            PlayerId = playerId;
        }
        
        public PlayDTO(string playType, bool isTeamB, Guid playerId, bool mainBoolProperty) : this(playType, isTeamB, playerId)
        {
            MainBoolProperty = mainBoolProperty;
        }

        public PlayDTO(string playType, bool isTeamB, Guid playerId, bool mainBoolProperty, int points) : this(playType, isTeamB, playerId)
        {
            MainBoolProperty = mainBoolProperty;
            Points = points;
        }
    }
    class PlayDTOValidator: AbstractValidator<PlayDTO>
    {
        public PlayDTOValidator()
        {
            RuleFor(x => x.IsTeamB).NotEmpty();
            RuleFor(x => x.PlayType).NotEmpty().IsEnumName(typeof(PlayTypeEnum),false);
            RuleFor(x => x.PlayerId).NotEmpty();
        }
    }
}
