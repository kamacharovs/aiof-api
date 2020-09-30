using System;
using System.Collections.Generic;
using System.Linq;

namespace aiof.api.data
{
    public static class Roles
    {
        public const string Admin = nameof(Admin);
        public const string User = nameof(User);
        public const string Client = nameof(Client);
        public const string Basic = nameof(Basic);
        

        public const string AdminOrUser = Admin + "," + User;
        public static string AdminOrClient = Combine(Admin, Client);
        public static string BasicOrUser = Combine(Basic, User);
        public static string BasicOrClient = Combine(Basic, Client);
        public static string Any = Combine(All.ToArray());
        public static string Combine(params string[] roles) => string.Join(",", roles);

        public static IEnumerable<string> All 
            => new string[]
            {
                Admin,
                User,
                Client,
                Basic
            };
    }
}