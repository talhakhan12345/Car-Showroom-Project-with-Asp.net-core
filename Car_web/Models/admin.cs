using System.ComponentModel.DataAnnotations;

namespace Car_web.Models
{
    public class admin
    {
       [Key]

       public int Admin_id { get; set; }
       public string Admin_name { get; set; }
       public string Admin_email { get; set; }
       public string Admin_pwd { get; set; }
    }
}
