
using Atom.Data.Model.Security;
using Atom.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Atom.Data.Manager
{
    public interface ISecurityManager
    {
        #region User
        UserModel? LoginUser(string emailAddress, string? password, bool ExternalAuth = false);
        IEnumerable<UserModel> GetUserModels(bool? active = true);
        void CreateNewUser(UserModel user);
        void SaveUser(UserModel user);
        UserModel GetUser(Guid userGuid);
        void UpdateManyUsers(IEnumerable<UserModel> users);
        IEnumerable<RoleModel> GetRoles(int userId);
        void AddFavoriteEvent(UserModel currentUser, EventType eventType, string eventID);
        IEnumerable<FavoriteEventModel> GetUsersFavoriteEvents(UserModel loggedInUser);
        void RemoveFavoriteEvent(UserModel currentUser, EventType eventType, string eventID);
        #endregion
    }
}
