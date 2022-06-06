using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{

    public interface IHelpRepository
    {
        public IEnumerable<Help> GetAllHelpRequests();
        public void Add(Help entity);
       
        public void Delete(Help help);
        
        public Help Find(int ID);
        
        public Help Update(Help entity); 
    }
    
}
