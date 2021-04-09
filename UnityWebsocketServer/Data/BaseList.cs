using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsocketServer.Models;

namespace WebsocketServer
{
    public abstract class BaseList<T> where T : BaseModel 
    {
        protected List<T> List = new List<T>();

        public List<T> GetList()
        {
            return List;
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

        public virtual T FindEntry(Guid target)
        {
            foreach (T t in List)
            {
                if (t.guid == target)
                {
                    return t;
                }
            }
            Console.WriteLine("Error: The server is trying to delete an entry that does not exist from a list. Target: " + target);
            return List[0];
        }

        public virtual string GetListAsString()
        {
            string val = "";

            foreach (T v in List)
            {
                val += v.guid + "%";
            }

            return val;
        }
        public virtual string GetListAsString(Guid target)
        {
            string val = "";

            foreach (T v in List)
            {
                if(v.guid != target)
                {
                    val += v.guid + "%";
                }
            }

            return val;
        }
    }
}
