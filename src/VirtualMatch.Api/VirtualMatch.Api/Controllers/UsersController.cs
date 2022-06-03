using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VirtualMatch.Api.Database;
using VirtualMatch.Entities;

namespace API.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UsersController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }


        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _dataContext.Users.ToList();

            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _dataContext.Users.Find(id);

            return user;
        }

    }
}