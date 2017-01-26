using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Model;
using Inventory.DAL;

namespace Inventory.BLL
{
  public class UserBs
    {
        private UserDA NewUserDA = new UserDA();

        public IEnumerable<User> ListAll()
        {
            return NewUserDA.ListAll();
        }


        public IEnumerable<User> ListAllByStatus(int status)
        {
            return NewUserDA.ListAllByStatus(status);
        }

        public string UpdateStatus(string username, int status)
        {
            return NewUserDA.UpdateStatus(username,status);
        }
        public User GetById(int id)
        {
            return NewUserDA.GetById(id);
        }

        public User GetByUsername(string username)
        {
            return NewUserDA.GetByUsername(username);
        }

        public string VerifyUsername(string username)
        {
            return NewUserDA.VerifyUsername(username);
        }
        public void Insert(User UserBsObj)
        {
            NewUserDA.Insert(UserBsObj);
        }

        public void Update(User UserBsObj)
        {
            var UserExist = NewUserDA.GetById(UserBsObj.UserID);
            if( UserBsObj.Password !="")
            UserExist.Password = UserBsObj.Password;
            UserExist.Status = UserBsObj.Status;
            UserExist.RoleID = UserBsObj.RoleID;
            UserExist.Flag = UserBsObj.Flag;
            NewUserDA.Update(UserExist);
        }

        public bool Login(string username, string password)
        {
          return NewUserDA.Login(username,password);
        }

        }
    }
