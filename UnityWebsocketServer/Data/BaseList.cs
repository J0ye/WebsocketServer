using System;
using System.Collections.Generic;

namespace WebsocketServer
{
    /// <summary>
    /// Base form of a list that introduces baisc functionality to all lists. Base lists can be created for any type, that is or inherits from type BaseModel.
    /// </summary>
    /// <typeparam name="T">Expected type of this list.</typeparam>
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

        /// <summary>
        /// Trys to remove an entry from the list. Will only work if the entry exits.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool RemoveEntry(Guid target)
        {
            T foundEntry;   
            if (TryFindEntry(target, out foundEntry))
            {
                List.Remove(foundEntry);
                return true;
            }
            Console.WriteLine("Error: The server is trying to delete an entry that does not exist from a list. Target: " + target);
            return false;
        }

        /// <summary>
        /// Will return an entry based on passed id.
        /// </summary>
        /// <param name="target">The id for target entry</param>
        /// <returns>Entry with the passed id</returns>
        public virtual T FindEntry(Guid target)
        {
            foreach (T entry in List)
            {
                if (entry.guid == target)
                {
                    return entry;
                }
            }
            Console.WriteLine("Error: The server is trying to delete an entry that does not exist from a list. Target: " + target);
            return List[0];
        }

        /// <summary>
        /// Special variant of the FindEntry function that will return a bool based on if an entry has been found and an entry via the out parameter
        /// </summary>
        /// <param name="target">ID of target entry</param>
        /// <param name="foundEntry">The entry in the list. Will be null if no entry has been found</param>
        /// <returns>True if an entry has been found and false if there is no entry.</returns>
        public virtual bool TryFindEntry(Guid target, out T foundEntry)
        {
            foreach (T entry in List)
            {
                if (entry.guid == target)
                {
                    foundEntry = entry;
                    return true;
                }
            }
            foundEntry = null;
            Console.WriteLine("Error: The server is trying to delete an entry that does not exist from a list. Target: " + target);
            return false;
        }
    }
}
