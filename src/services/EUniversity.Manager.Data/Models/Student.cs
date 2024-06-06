﻿using EUniversity.Manager.Data.Models.Base;

namespace EUniversity.Manager.Data.Models;

public class Student : BaseEntity
{
    public Guid Id { get; set; }

    public string FullName { get; set; }
}
