﻿using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public interface IUser
    {
        int Id { get; set; }
        Guid PublicKey { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Username { get; set; }
    }
}