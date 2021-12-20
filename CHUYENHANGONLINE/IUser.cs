using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUYENHANGONLINE
{
    public interface IUser
    {
        int Id { get; set; }//for purchase, change info ...
        int LoginId { get; set; }//for change passowrd,...idk:)
        string Name { get; set; }
        string Email { get; set; }
        string Tel { get; set; }
    }
}
