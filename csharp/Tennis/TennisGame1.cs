using System;
using System.Runtime.Intrinsics.X86;

namespace Tennis
{
    public enum ScoreEnum
    {
        Love, 
        Fifteen, 
        Thirty, 
        Forty,
        Deuce,
    }

    internal class Player(string name)
    {
        public string Name = name;
        public int Score = 0;
        public void AddPoint()=> Score ++;
    }
    
    public class TennisGame1 : ITennisGame
    {
        private readonly Player _firstPlayer;
        private readonly Player _secondPlayer;

        public TennisGame1(string firstPlayerName, string secondPlayerName)
        {
            _firstPlayer = new Player(firstPlayerName);
            _secondPlayer = new Player(secondPlayerName);
        }

        public void WonPoint(string playerName)
        {
            if (playerName == _firstPlayer.Name)
                _firstPlayer.AddPoint();
            else
                _secondPlayer.AddPoint();
        }

        public string GetScore()
        {
            if (ArePlayersScoreEqual()) return GetEqualScore(); 
            if (IsTheEndOfGame())
                 return GetEndGameScore(); 
            return  $"{(ScoreEnum)_firstPlayer.Score}-{(ScoreEnum)_secondPlayer.Score}";
        }

        private string GetEndGameScore()
        {
            return Math.Abs(_firstPlayer.Score - _secondPlayer.Score) switch
            {
                1 => $"Advantage {LeadingPlayer.Name}",
                _ => $"Win for {LeadingPlayer.Name}"
            };
        }
        private string GetEqualScore()
        {
            return  _firstPlayer.Score switch
            {
                var x and < 3 => $"{(ScoreEnum)x}-All",
                _ => $"{ScoreEnum.Deuce}"
            };
        }
        private bool IsTheEndOfGame() => _firstPlayer.Score >= 4 || _secondPlayer.Score >= 4;
        private bool ArePlayersScoreEqual() => _firstPlayer.Score == _secondPlayer.Score;
        private Player LeadingPlayer => _firstPlayer.Score > _secondPlayer.Score ? _firstPlayer : _secondPlayer;
    }
}

