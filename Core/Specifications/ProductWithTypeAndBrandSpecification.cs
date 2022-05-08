using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Specifications;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
    {

        public ProductWithTypeAndBrandSpecification()
        {
            AddInclude(X=>X.ProductType);
            AddInclude(X => X.ProductBrand);
        }

        public ProductWithTypeAndBrandSpecification(int id) : base(x => x.Id == id)
        {

        }
    }
}
