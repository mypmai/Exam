using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.Models
{
    public class Product
    {
        public int ProductId {get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public float Start_Bid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime End_Date { get; set; }

        public int UserId { get; set; }


        public List<Bid> Bid { get; set; }
        
 
        public Product()
        {
            Bid = new List<Bid>();
            
        }
    }

}