using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_commerce.Models.Repositories
{

    public class ImageProductRepository : IRepository<ImagesProduct>
    {
        WebContext context;



        public ImageProductRepository(WebContext db)
        {
            this.context = db;
        }

        public void Add(ImagesProduct entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            context.ImagesProducts.Remove(Find(ID));
            context.SaveChanges();
        }

        public ImagesProduct Find(int ID)
        {
            return context.ImagesProducts.FirstOrDefault(a => a.Id == ID);
        }

        public ImagesProduct Find(string Text)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImagesProduct> show(int? ID, string name = "")
        {
            throw new NotImplementedException();
        }

        public void Update(ImagesProduct entity)
        {
            throw new NotImplementedException();
        }
    }
}
