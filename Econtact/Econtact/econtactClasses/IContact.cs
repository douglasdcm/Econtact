using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econtact.econtactClasses
{
    public interface IContact
    {
        bool Insert(Contact c);
        DataTable Select();
        bool Update(Contact c);
        bool Delete(Contact c);
    }
}
