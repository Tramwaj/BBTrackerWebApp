using BBTracker.Common;
using BBTracker.Contracts.ViewModels;
using BBTracker.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.App.Services
{
    public class PlayParser : IPlayParser
    {
        private Guid _gameId;
        public ICollection<Play> ReadPlaysBundle(ICollection<PlayDTO> playsBundle, Guid gameId)
        {
            _gameId = gameId;
            if (Enum.TryParse<PlayTypeEnum>(playsBundle.First().PlayType.Trim(), true, out PlayTypeEnum _rootType))
            {
                switch (_rootType)
                {
                    case PlayTypeEnum.fieldgoal:
                        return ParseFieldGoalBundle(playsBundle);
                    case PlayTypeEnum.steal:
                    case PlayTypeEnum.turnover:
                        return ParseTurnoverBundle(playsBundle);
                    case PlayTypeEnum.substitution:
                        return ParseSubstitutionBundle(playsBundle);
                    case PlayTypeEnum.foul:
                        throw new NotImplementedException();
                    default:
                        break;
                }
            }
            return null;
        }

        private ICollection<Play> ParseSubstitutionBundle(ICollection<PlayDTO> playsBundle)
        {
            return playsBundle.Select(s => (Play)new Substitution(Guid.NewGuid(), DateTime.Now, s.IsTeamB, s.PlayerId, _gameId, s.MainBoolProperty)).ToList();
        }

        private ICollection<Play> ParseFieldGoalBundle(ICollection<PlayDTO> playsBundle)
        {
            var _result = new List<Play>();
            switch (playsBundle.Count)
            {
                case 1:
                    _result.AddRange(playsBundle.Select(p => new FieldGoal(Guid.NewGuid(), DateTime.Now, p.IsTeamB, p.PlayerId, _gameId, p.Points, p.MainBoolProperty, false, false)).ToList());
                    break;
                case 2:
                    _result.AddRange(FieldGoalTwoActions(playsBundle));
                    break;
                case 3:
                    _result.AddRange(FieldGoalThreeActions(playsBundle));
                    break;
                default:
                    throw new InvalidDataException();
            }
            return _result;
        }

        private ICollection<Play> FieldGoalThreeActions(ICollection<PlayDTO> playsBundle)
        {
            var dto1 = playsBundle.First();
            var dto2 = playsBundle.First(p => p.PlayType.ToLower() == "block");
            var dto3 = playsBundle.First(p => p.PlayType.ToLower() == "rebound");
            var fg = new FieldGoal(Guid.NewGuid(), DateTime.Now, dto1.IsTeamB, dto1.PlayerId, _gameId, dto1.Points, false, true, false);
            var block = new Block(Guid.NewGuid(), DateTime.Now, dto2.IsTeamB, dto2.PlayerId, _gameId, fg);
            var rebound = new Rebound(Guid.NewGuid(), DateTime.Now, dto3.IsTeamB, dto3.PlayerId, _gameId, dto1.IsTeamB == dto3.IsTeamB, fg);
            return new List<Play> { fg, block, rebound };
        }

        private ICollection<Play> FieldGoalTwoActions(ICollection<PlayDTO> playsBundle)
        {
            var dto1 = playsBundle.First();
            var dto2 = playsBundle.Last();
            bool _FGmade = dto1.MainBoolProperty;
            var fg = new FieldGoal(Guid.NewGuid(), DateTime.Now, dto1.IsTeamB, dto1.PlayerId, _gameId, dto1.Points, _FGmade, dto2.PlayType.ToLower() == "block", _FGmade);
            if (dto2.PlayType.ToLower() == "assist")
            {
                var assist = new Assist(Guid.NewGuid(), DateTime.Now, dto1.IsTeamB, dto2.PlayerId, _gameId, fg);
                return new List<Play> { fg, assist };
            }
            if (playsBundle.Last().PlayType.ToLower() == "rebound")
            {
                var rebound = new Rebound(Guid.NewGuid(), DateTime.Now, dto2.IsTeamB, dto2.PlayerId, _gameId, dto1.IsTeamB == dto2.IsTeamB, fg);
                return new List<Play> { fg, rebound };
            }
            if (playsBundle.Last().PlayType.ToLower() == "block")
            {
                var block = new Block(Guid.NewGuid(), DateTime.Now, dto2.IsTeamB, dto2.PlayerId, _gameId, fg);
                return new List<Play> { fg, block };
            }
            throw new InvalidDataException();

        }


        private ICollection<Play> ParseTurnoverBundle(ICollection<PlayDTO> playsBundle)
        {
            var time = DateTime.Now;
            return playsBundle.Select<PlayDTO, Play>(p => p.PlayType.ToLower() == "steal" ?
             new Steal(Guid.NewGuid(), time, p.IsTeamB, p.PlayerId, _gameId) :
             new Turnover(Guid.NewGuid(), time, p.IsTeamB, p.PlayerId, _gameId)             
            ).ToList();
        }

    }
}
