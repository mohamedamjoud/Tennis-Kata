using System;
namespace Tennis;

public enum ScoreEnum {
    Love ,
    Fifteen,
    Thirty,
    Forty,
    Deuce
}

internal class Player
{
    public string PlayerName { get; private set; }
    public int PlayerScore { get; private set; }

    public Player(string playerName)
    {
        PlayerName = playerName;
        PlayerScore = 0;
    }

    public void AddPoint() => PlayerScore++;
}



public class TennisGame6 : ITennisGame
{
    private readonly Player _firstPlayer;
    private readonly Player _secondPlayer;

    public TennisGame6(string player1Name, string player2Name)
    {
        _firstPlayer = new Player(player1Name);
        _secondPlayer = new Player(player2Name);
    }

    public void WonPoint(string playerName)
    {
        if (_firstPlayer.PlayerName == playerName)
            _firstPlayer.AddPoint();
        else
            _secondPlayer.AddPoint();
    }

    public string GetScore()
    {
        if (IsTieScore()) return GetTieScore();
        if (IsEndGameScore()) return GetEndGameScore();
        
        return  $"{(ScoreEnum)_firstPlayer.PlayerScore}-{(ScoreEnum)_secondPlayer.PlayerScore}";
    }

    private string GetEndGameScore()
    {
        return Math.Abs(_firstPlayer.PlayerScore - _secondPlayer.PlayerScore) switch
        {
            1 => $"Advantage {LeadingPlayer?.PlayerName}",
            _ => $"Win for {LeadingPlayer?.PlayerName}"
        };
    }

    private string GetTieScore()
    {
        return _firstPlayer.PlayerScore switch
        {
            var x and < 3 => $"{(ScoreEnum)x}-All",
            _ => $"{ScoreEnum.Deuce}"
        };
    }

    private bool IsTieScore() => _firstPlayer.PlayerScore == _secondPlayer.PlayerScore;
    private bool IsEndGameScore() => _firstPlayer.PlayerScore >= 4 || _secondPlayer.PlayerScore >= 4;
    private Player? LeadingPlayer => _firstPlayer.PlayerScore > _secondPlayer.PlayerScore ? _firstPlayer : _secondPlayer;
}