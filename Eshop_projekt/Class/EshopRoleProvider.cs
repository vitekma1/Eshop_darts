using DataAccess.Dao;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Eshop_projekt.Class
{
    public class EshopRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string jmeno)
        {
            EshopUzivatelDao eshopUzivatelDao = new EshopUzivatelDao();
            EshopUzivatel uzivatel = eshopUzivatelDao.GetByLogin(jmeno);

            if (uzivatel == null)
            {
                return new string[] { };
            }

            return new string[] {uzivatel.Role.Identifikator};

        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            EshopUzivatelDao eshopUzivatelDao = new EshopUzivatelDao();
            EshopUzivatel uzivatel = eshopUzivatelDao.GetByLogin(username);

            if (uzivatel == null)
                return false; 
            return uzivatel.Role.Identifikator == roleName;

        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}