using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Planning.Api.Model
{
    public class Register
    {

        public string Email { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        //public string RePassword { get; set; }
    }
}
