using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int Score { get; set; }
}
