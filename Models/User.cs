using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.Models
{
    public class User
    {
        public int UserId {get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created_at { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated_at { get; set; }

        
        public List<Bid> Bid { get; set; }
 
        public User()
        {
            Bid = new List<Bid>();
            
        }
    }

}