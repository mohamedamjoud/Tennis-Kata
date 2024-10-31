using System;
using System.Text;

namespace Tennis
{
    internal class Player(string name)
    {
        public string Name { get; private set; } = name;
        public int Point { get; private set; }

        public void AddPoint() => Point++;
    }
    internal enum TennisScoreEnum 
    {
        Love,
        Fifteen,
        Thirty,
        Forty,
        Deuce,
    }
    public class TennisGame3 : ITennisGame
    {
        private readonly Player _firstPlayer;
        private readonly Player _secondPlayer;

        public TennisGame3(string firstPlayerName, string secondPlayerName)
        {
            _firstPlayer = new Player(firstPlayerName);
            _secondPlayer = new Player(secondPlayerName);
        }

        public string GetScore()
        {
            if (IsFortyOrLess())
                return PlayersPointAreEqual()
                    ? $"{(TennisScoreEnum)_firstPlayer.Point}-All"
                    : $"{(TennisScoreEnum)_firstPlayer.Point}-{(TennisScoreEnum)_secondPlayer.Point}";
            
            if (IsDeuce()) return TennisScoreEnum.Deuce.ToString();

            var leadingPlayerName = LeadingPlayer()?.Name;
            return IsAdvantage()
                ? $"Advantage {leadingPlayerName}"
                : $"Win for {leadingPlayerName}";
        }

        public void WonPoint(string playerName)
        {
            if ( _firstPlayer.Name == playerName)
                _firstPlayer.AddPoint();
            else
                _secondPlayer.AddPoint();
        }

        private bool IsFortyOrLess() => (_firstPlayer.Point < 4 && _secondPlayer.Point < 4) && (_firstPlayer.Point + _secondPlayer.Point < 6);
        private bool PlayersPointAreEqual() => _firstPlayer.Point == _secondPlayer.Point;
        private bool IsDeuce() => !IsFortyOrLess() && PlayersPointAreEqual();
        private bool IsAdvantage() => Math.Abs(_firstPlayer.Point - _secondPlayer.Point) == 1;
        private Player? LeadingPlayer() => _firstPlayer.Point > _secondPlayer.Point ? _firstPlayer : _secondPlayer;
    }
}