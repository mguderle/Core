﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BExIS.Security.Entities.Subjects;
using System.DirectoryServices.AccountManagement;

namespace BExIS.Security.Providers.Authentication
{
    public sealed class ADAuthenticationProvider : IAuthenticationProvider
    {
        private string AdminUsername { get; set; }
        private string AdminPassword { get; set; }
        private string Host { get; set; }
        private string BaseContainer { get; set; }

        public ADAuthenticationProvider(string connectionString)
        {
            Dictionary<string, string> parameters = connectionString
                .Split(';')
                    .Select(x => x.Split(':'))
                        .ToDictionary(x => x[0], x => x[1]);

            foreach (KeyValuePair<string, string> entry in parameters)
            {
                switch (entry.Key)
                {
                    case "AdminUsername":
                        this.AdminUsername = entry.Value;
                        break;
                    case "AdminPassword":
                        this.AdminPassword = entry.Value;
                        break;
                    case "Host":
                        this.Host = entry.Value;
                        break;
                    case "BaseContainer":
                        this.BaseContainer = entry.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        public bool IsUserAuthenticated(string username, string password)
        {
            PrincipalContext AD = new PrincipalContext(ContextType.Domain, this.Host, this.BaseContainer, this.AdminUsername, this.AdminPassword);

            if (AD.ValidateCredentials(username, password))
            {
                return true;
            }
            return false;
        }

        public User GetUser(string username, string password)
        {
            PrincipalContext AD = new PrincipalContext(ContextType.Domain, this.Host, this.BaseContainer, this.AdminUsername, this.AdminPassword);

            if (AD.ValidateCredentials(username, password))
            {
                UserPrincipal u = new UserPrincipal(AD);
                u.SamAccountName = username;

                PrincipalSearcher search = new PrincipalSearcher(u);
                UserPrincipal result = (UserPrincipal)search.FindOne();
                search.Dispose();

                if (result != null)
                {
                    User AdUser = new User();
                    AdUser.Name = result.DisplayName;
                    AdUser.Email = result.EmailAddress;
                    return AdUser;
                }
                else
                {
                    throw new Exception("User not found");
                }
            }
            else
            {
                throw new Exception("User could not be authenticated");
            }
        }

    }
}