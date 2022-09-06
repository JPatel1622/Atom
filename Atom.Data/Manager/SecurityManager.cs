using Atom.Data.Criteria.Security;
using Atom.Data.Entity.Security;
using Atom.Data.Model.Security;
using Atom.Domain.Configuration;
using Atom.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Data.Manager
{
    public class SecurityManager : ManagerBase, ISecurityManager
    {
        #region Constructors
        public SecurityManager(IServiceProvider provider) : base(provider) { }
        #endregion

        #region User
        public UserModel LoginUser(string emailAddress, string? password, bool ExternalAuth = false)
        {
            var criteria = new LoginUserCriteria()
            {
                EmailAddress = emailAddress,
                Password = password,
                ExternalAuth = ExternalAuth
            };

            return base.ReadSingle<UserModel, UserEntity, LoginUserCriteria>(criteria: criteria);
        }

        public IEnumerable<UserModel> GetUserModels(bool? active = true)
        {
            GetAllUserCriteria criteria = new()
            {
                Active = active
            };

            return base.ReadMany<UserModel, UserEntity, GetAllUserCriteria>(criteria: criteria);
        }

        public void CreateNewUser(UserModel user)
        {
            CreateNewUserCriteria criteria = new()
            {
                EmailAddress = user.EmailAddress,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                CreateUserID = 0,
                ExternalAuth = false
            };

            base.CreateSingle<UserModel, UserEntity, CreateNewUserCriteria>(criteria);
            //user.Active = true;
            //user.CreatedByUser = 1;
            //base.SaveModel<UserModel, UserEntity>(user);
        }

        public void SaveUser(UserModel user)
        {
            //return SaveModel<UserModel>(user);
            user.CreatedByUser = 1;
            base.UpdateSingle<UserModel, UserEntity>(user);
        }

        public UserModel GetUser(Guid userGuid)
        {
            GetUserCriteria criteria = new() { UserGuid = userGuid };
            return base.ReadSingle<UserModel, UserEntity, GetUserCriteria>(criteria);
        }

        public void UpdateManyUsers(IEnumerable<UserModel> users)
        {
            base.UpdateMany<UserModel, UserEntity>(users);
        }

        public IEnumerable<RoleModel?> GetRoles(int userId)
        {
            var criteria = new GetRolesCriteria()
            {
                UserId = userId
            };

            return base.ReadMany<RoleModel, RoleEntity, GetRolesCriteria>(criteria);
        }

        public void AddFavoriteEvent(UserModel currentUser, EventType eventType, string eventID)
        {
            var model = new FavoriteEventModel()
            {
                Active = true,
                EventId = eventID,
                EventTypeId = eventType,
                UserId = currentUser.UserId
            };

            base.CreateSingle<FavoriteEventModel, FavoriteEventEntity>(model);
        }

        public IEnumerable<FavoriteEventModel> GetUsersFavoriteEvents(UserModel loggedInUser)
        {
            GetUserFavoriteEventsCriteria criteria = new()
            {
                UserID = loggedInUser.UserId,
            };

            return base.ReadMany<FavoriteEventModel, FavoriteEventEntity, GetUserFavoriteEventsCriteria>(criteria);
        }

        public void RemoveFavoriteEvent(UserModel currentUser, EventType eventType, string eventID)
        {
            RemoveUserFavoriteEventsCriteria criteria = new()
            {
                EventID = eventID,
                EventTypeID = eventType,
                UserID = currentUser.UserId
            };

            base.ReadSingle<FavoriteEventModel, FavoriteEventEntity, RemoveUserFavoriteEventsCriteria>(criteria);
        }
        #endregion
    }
}
