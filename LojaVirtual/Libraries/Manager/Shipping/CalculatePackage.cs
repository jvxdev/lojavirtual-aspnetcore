using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using LojaVirtual.Models.ProductAggregator;

namespace LojaVirtual.Libraries.Manager.Shipping
{
    public class CalculatePackage
    {
        public List<Package> CalculateProductsPackage(List<ProductItem> products)
        {
            List<Package> packages = new List<Package>();

            Package package = new Package();
            foreach (var item in products)
            {
                for(int i = 0; i <item.ItensKartAmount; i++)
                {
                    var Weight = package.Weight + item.Weight;
                    var Lenght = (package.Lenght > item.Lenght) ? package.Lenght : item.Lenght;
                    var Width = (package.Width > item.Width) ? package.Width : item.Width;
                    var Height = package.Height + item.Height;

                    var Dimension = Lenght + Width + Height;

                    if (Weight > 30 || Dimension > 200)
                    {
                        packages.Add(package);
                        package = new Package();
                    }

                    package.Weight = package.Weight + item.Weight;
                    package.Lenght = (package.Lenght > item.Lenght) ? package.Lenght : item.Lenght;
                    package.Width = (package.Width > item.Width) ? package.Width : item.Width;
                    package.Height = package.Height + item.Height;
                }
            }
            packages.Add(package);

            return packages;
        }
    }
}
