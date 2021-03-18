using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servertest.Models
{
    public abstract class BaseList<T> where T : BaseModel 
    {
        public List<T> List 
        {
            get
            {
                return List;
            }
        }

        public void AddEntry(T target)
        {
            List.Add(target);
        }

        public bool RemoveEntry(Guid target)
        {
            foreach(T t in List)
            {
                if (t.guid == target)
                {
                    List.Remove(t);
                    return true;
                }
            }
            Console.WriteLine("Error: The server is trying to delete an entry that does not exist from a list. Target: " + target);
            return false;
        }

        public abstract string GetListAsString();
        public abstract string GetListAsString(Guid target);
    }
}
