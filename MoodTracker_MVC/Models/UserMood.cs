﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MoodTracker_MVC.Models;

public partial class UserMood
{
    public int MoodId { get; set; }

    public int UserId { get; set; }

    public int MoodType { get; set; }

    public string MoodComments { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Mood MoodTypeNavigation { get; set; }
}