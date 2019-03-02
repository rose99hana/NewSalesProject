using NewSalesProject.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NewSalesProject.Model
{
    public partial class DataInitializer : DropCreateDatabaseIfModelChanges<SalesContext>
    {
        protected override void Seed(SalesContext db)
        {
            var Categories = new List<Category>
            {
                new Category{Name="日用品"},
                new Category{Name="医薬品"},
                new Category{Name="衛生用品"},
                new Category{Name="化粧品"},
            };
            Categories.ForEach(p => db.Categories.Add(p));


            var CustomerRanks = new List<CustomerRank>
            {
                new CustomerRank{Name="Diamon"},
                new CustomerRank{Name="Gold"},
                new CustomerRank{Name="Sliver"},
                new CustomerRank{Name="Bronze"},
            };
            CustomerRanks.ForEach(p => db.CustomerRanks.Add(p));


            var Permissions = new List<Permission>
            {
                new Permission{Name="Adminitrator"},
                new Permission{Name="User"},
                new Permission{Name="Guest"},
            };
            Permissions.ForEach(p => db.Permissions.Add(p));


            var Stores = new List<Store>
            {
                new Store{ Name="Amazon.co.jp 公式サイト", Address="https://www.amazon.co.jp/", Logo = ImageToBytes(new BitmapImage(new Uri($"pack://application:,,,/Images/AmazonLogo.jpeg"))) },
                new Store{ Name="マツモトキヨシ赤羽すずらんストリート店", Address="〒115-0045 東京都北区赤羽2-13-3 サトウビル1F", Logo = ImageToBytes(new BitmapImage(new Uri($"pack://application:,,,/Images/MatsumotoLogo.jpg"))) },
                new Store{ Name="楽天と西友のネットスーパー", Address="https://sm.rakuten.co.jp/", Logo = ImageToBytes(new BitmapImage(new Uri($"pack://application:,,,/Images/seiyu.png"))) },
            };
            Stores.ForEach(p => db.Stores.Add(p));

            var Products = new List<Product>
            {
                new Product{Name="fwfhiwef", Code="AAAA", NetWeight=35, Category=Categories[0], Picture = ImageToBytes(new BitmapImage(new Uri($"pack://application:,,,/Images/chanelN5.jpg")))},
                new Product{Name="fwfhiwef", Code="BBBB", NetWeight=35, Category=Categories[0], Picture = ImageToBytes(new BitmapImage(new Uri($"pack://application:,,,/Images/collagen.jpg")))},
            };
            Products.ForEach(p => db.Products.Add(p));


            var ProductPrices = new List<ProductPrice>
            {
                new ProductPrice{Product=Products[0], Store=Stores[0], Price=40, CurrencySymbol="$", Discount=10, IsTaxIncluding=true },
                new ProductPrice{Product=Products[0], Store=Stores[1], Price=50, CurrencySymbol="¥", Discount=10, IsTaxIncluding=false },
                new ProductPrice{Product=Products[1], Store=Stores[0], Price=60, CurrencySymbol="¥", Discount=10, IsTaxIncluding=true },
                new ProductPrice{Product=Products[1], Store=Stores[1], Price=70, CurrencySymbol="€", Discount=10, IsTaxIncluding=false },
            };
            ProductPrices.ForEach(p => db.ProductPrices.Add(p));


            var Employees = new List<Employee>
            {
                new Employee{Name="Ngo Le Nhat Minh", Address="Amazon.co.jp", Permission=Permissions[0]},
                new Employee{Name="Ngo Le Thao Phuong", Address="Amazon.co.jp", Permission=Permissions[2]},
                new Employee{Name="Huynh Thi Bich Chung", Address="Amazon.co.jp", Permission=Permissions[2]},
                new Employee{Name="Aegegeg", Address="Akabane", Permission=Permissions[1]},
            };
            Employees.ForEach(p => db.Employees.Add(p));


            var Customers = new List<Customer>
            {
                new Customer{Name="Nguyen Van Anh", Tel="0439743982", Address1="Da Nang", CustomerRank=CustomerRanks[3], Relationship="Friend"},
                new Customer{Name="Nguyen Van Binh", Tel="0439743982", Address1="Da Nang", CustomerRank=CustomerRanks[3], Relationship="Friend"},
                new Customer{Name="Nguyen Van Tan", Tel="0439743982", Address1="Da Nang", CustomerRank=CustomerRanks[3], Relationship="Friend"},
                new Customer{Name="Nguyen Van Hanh", Tel="0439743982", Address1="Da Nang", CustomerRank=CustomerRanks[3], Relationship="Friend"},
            };
            Customers.ForEach(p => db.Customers.Add(p));




            var AssetCategories = new List<AssetCategory>
            {
                new AssetCategory {Name="パソコン類", },
                new AssetCategory {Name="周辺機器", },
                new AssetCategory {Name="サーバー類", },
                new AssetCategory {Name="ネットワーク機器", },
                new AssetCategory {Name="ソフトウェア", },
            };
            AssetCategories.ForEach(p => db.AssetCategories.Add(p));

            var Departments = new List<Department>
            {
                new Department {Name="総務部", },
                new Department {Name="管理部", },
                new Department {Name="教務部", },
            };
            Departments.ForEach(p => db.Departments.Add(p));

            var InstallationLocations = new List<InstallationLocation>
            {
                new InstallationLocation {Name="学校内の各教室", },
                new InstallationLocation {Name="職員室1および2", },
                new InstallationLocation {Name="倉庫", },
            };
            InstallationLocations.ForEach(p => db.InstallationLocations.Add(p));

            var Assets = new List<Asset>
            {
                new Asset{Name="ABC", AssetCategory = AssetCategories[1], Department = Departments[0], InstallationLocation = InstallationLocations[1]},
                new Asset{Name="CDE", AssetCategory = AssetCategories[2], Department = Departments[1], InstallationLocation = InstallationLocations[2]},
                new Asset{Name="aDFS", AssetCategory = AssetCategories[0], Department = Departments[2], InstallationLocation = InstallationLocations[0]},
                new Asset{Name="efwf", AssetCategory = AssetCategories[4], Department = Departments[2], InstallationLocation = InstallationLocations[1]},
            };
            Assets.ForEach(p => db.Assets.Add(p));

            db.SaveChanges();
        }

        //public byte[] ImageToByteArray(Image imageIn)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //    return ms.ToArray();
        //}

        public byte[] ImageToBytes(BitmapImage img)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(img));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }
    }
}
