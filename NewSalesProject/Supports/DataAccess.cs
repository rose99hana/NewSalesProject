using NewSalesProject.Interfaces;
using NewSalesProject.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace NewSalesProject.Supports
{
    public static class DataAccess
    {

        public static ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();
        public static ObservableCollection<CustomerRank> CustomerRanks { get; set; } = new ObservableCollection<CustomerRank>();
        public static ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public static ObservableCollection<Store> Stores { get; set; } = new ObservableCollection<Store>();
        public static ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public static ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public static ObservableCollection<ProductPrice> ProductPrices { get; set; } = new ObservableCollection<ProductPrice>();
        public static ObservableCollection<ReceiptDetail> ReceiptDetails { get; set; } = new ObservableCollection<ReceiptDetail>();
        public static ObservableCollection<GoodsReceipt> GoodsReceipts { get; set; } = new ObservableCollection<GoodsReceipt>();
        public static ObservableCollection<WishList> WishLists { get; set; } = new ObservableCollection<WishList>();
        public static ObservableCollection<WishListDetail> WishListDetails { get; set; } = new ObservableCollection<WishListDetail>();

        public static ObservableCollection<Asset> Assets { get; set; } = new ObservableCollection<Asset>();
        public static ObservableCollection<AssetCategory> AssetCategories { get; set; } = new ObservableCollection<AssetCategory>();
        public static ObservableCollection<Department> Departments { get; set; } = new ObservableCollection<Department>();
        public static ObservableCollection<InstallationLocation> InstallationLocations { get; set; } = new ObservableCollection<InstallationLocation>();

        public static List<string> CurrencyCodeList { get; set; } = new List<string>
        {
            "VND(₫)",
            "USD($)",
            "JPY(¥)",
            "EUR(€)",
        };

        public static List<string> CurrencySymbolList { get; set; } = new List<string>
        {
            "₫",
            "$",
            "¥",
            "€",
        };

        /// <summary>
        ///     Copy properties from source entity to target entity
        /// </summary>
        /// <param name="type">typeof(class)</param>
        /// <param name="target">target object</param>
        /// <param name="source">source object</param>
        public static void CopyProperties(Type type, object target, object source)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo pro in properties)
            {
                try
                {
                    pro.SetValue(target, pro.GetValue(source));
                }
                catch { var x = pro.Name; }
            }
        }

        public static async Task UpdateDatabase(SalesContext db)
        {
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
            }
        }



        //public async static Task GetAllCustomers()
        //{
        //    Customers.Clear();
        //    using (var db = new SalesContext())
        //    {
        //        if (CustomerRanks.Count != 0)
        //        {
        //            foreach (var i in CustomerRanks)
        //            {
        //                db.CustomerRanks.Attach(i);
        //            }
        //        }
        //        var result = await (from c in db.Customers select c).ToListAsync();
        //        foreach (Customer item in result)
        //        {
        //            Customers.Add(item);
        //        }
        //    }
        //}
        #region Disconnected Scenarios Entity Framwork

        #region Customer
        public async static Task GetAllCustomers()
        {
            Customers.Clear();
            CustomerRanks.Clear();
            using (var db = new SalesContext())
            {
                var result = await (from c in db.Customers select c).ToListAsync();
                var result1 = await (from c in db.CustomerRanks.Include("Customers") select c).ToListAsync();
                foreach (Customer item in result)
                {
                    Customers.Add(item);
                }
                foreach (CustomerRank item in result1)
                {
                    CustomerRanks.Add(item);
                }
            }
        }

        public async static Task AddCustomer(Customer itemPara)
        {
            using(var db = new SalesContext())
            {
                if (itemPara.CustomerRank != null)
                {
                    itemPara.CustomerRankID = itemPara.CustomerRank.Id;         //NOT DELETE - IMPORTANT
                    //db.CustomerRanks.Attach(itemPara.CustomerRank);             //Prevent conflick key orcur in entity, REQUIRED
                    db.Entry(itemPara).State = EntityState.Unchanged;
                }
                db.Customers.Add(itemPara);
                await UpdateDatabase(db);
                Customers.Add(itemPara);
            }
        }

        public async static Task SaveCustomer(Customer itemPara, Customer oldItem)
        {
            using (var db = new SalesContext())
            {
                //if (oldItem.CustomerRank != null && oldItem.CustomerRank.Id != itemPara.CustomerRank.Id) //NOT DELETE - IMPORTANT
                //    db.CustomerRanks.Attach(oldItem.CustomerRank);

                db.Entry(oldItem).State = EntityState.Unchanged;        //NOT DELETE - IMPORTANT
                db.CustomerRanks.Attach(itemPara.CustomerRank);         //NOT DELETE - IMPORTANT

                var query = db.Customers.Find(itemPara.Id);
                CopyProperties(typeof(Customer), query, itemPara);
                query.CustomerRankID = itemPara.CustomerRank.Id;
                db.Entry(query).State = EntityState.Modified;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteCustomer(Customer itemPara)
        {
            using (var db = new SalesContext())
            {
                //if (itemPara.CustomerRank != null)                                   //NOT DELETE - IMPORTANT
                //    db.CustomerRanks.Attach(itemPara.CustomerRank);
                db.Entry(itemPara).State = EntityState.Unchanged;                       //NOT DELETE - IMPORTANT
                Customers.Remove(itemPara);
                db.Entry(itemPara).State = EntityState.Deleted;     // must be executed after deleted it in Customers
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteManyCustomers(IList itemParas)
        {
            using (var db = new SalesContext())
            {
                List<Customer> list = new List<Customer>();
                foreach (object item in itemParas)
                {
                    list.Add((Customer)item);
                }
                foreach (Customer item in list)
                {
                    //if (item.CustomerRank != null)                                   //NOT DELETE - IMPORTANT
                    //    db.CustomerRanks.Attach(item.CustomerRank);             //Prevent conflick key orcur in entity, REQUIRED
                    db.Entry(item).State = EntityState.Unchanged;
                    Customers.Remove(item);
                    db.Entry(item).State = EntityState.Deleted;
                    //var query = db.Customers.Find(item.Id);                     //NOT DELETE - IMPORTANT
                    //db.Entry(query).State = EntityState.Deleted;
                }
                await UpdateDatabase(db);
            }
        }
        #endregion


        #region CustomerRank
        public async static Task GetAllCustomerRanks()
        {
            await GetAllCustomers();
        }

        public async static Task AddCustomerRank(CustomerRank itemPara)
        {
            using (var db = new SalesContext())
            {
                db.CustomerRanks.Add(itemPara);
                await UpdateDatabase(db);
                CustomerRanks.Add(itemPara);
            }
        }

        public async static Task SaveCustomerRank(CustomerRank itemPara, CustomerRank oldItem)
        {
            //using (var db = new SalesContext())
            //{
            //    foreach(var item in Customers)  //change customerRank of relative Customer in customers collection to new customerRank
            //    {
            //        if (item.CustomerRankID == itemPara.Id)         
            //            item.CustomerRank = itemPara;
            //    }

            //    var query = db.CustomerRanks.Find(itemPara.Id);
            //    var x = query.Customers;
            //    CopyProperties(typeof(CustomerRank), query, itemPara);
            //    query.Customers = x;
            //    db.Entry(query).State = EntityState.Modified;
            //    await UpdateDatabase(db);
            //}

            using (var db = new SalesContext())
            {
                db.Entry(oldItem).State = EntityState.Unchanged;
                var query = db.CustomerRanks.Find(itemPara.Id);
                CopyProperties(typeof(CustomerRank), query, itemPara);
                db.Entry(query).State = EntityState.Modified;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteCustomerRank(CustomerRank itemPara)
        {
            //using (var db = new SalesContext())
            //{
            //    db.CustomerRanks.Attach(itemPara);
            //    db.Entry(itemPara).State = EntityState.Deleted;
            //    CustomerRanks.Remove(itemPara);
            //    await UpdateDatabase(db);
            //}

            using (var db = new SalesContext())
            {
                db.Entry(itemPara).State = EntityState.Unchanged;                       //NOT DELETE - IMPORTANT
                CustomerRanks.Remove(itemPara);
                db.Entry(itemPara).State = EntityState.Deleted;     // must be executed after deleted it in Customers
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteManyCustomerRanks(IList itemParas)
        {
            using (var db = new SalesContext())
            {
                List<CustomerRank> list = new List<CustomerRank>();
                foreach (object item in itemParas)
                {
                    list.Add((CustomerRank)item);
                }
                foreach (CustomerRank item in list)
                {
                    db.Entry(item).State = EntityState.Unchanged;
                    CustomerRanks.Remove(item);
                    db.Entry(item).State = EntityState.Deleted;

                    //CustomerRanks.Remove(item);
                    //var query = db.CustomerRanks.Find(item.Id);
                    //db.Entry(query).State = EntityState.Deleted;
                }
                await UpdateDatabase(db);
            }
        }
        #endregion


        #region Store
        public async static Task GetAllStores()
        {
            await GetAllProducts();
        }

        public async static Task AddStore(Store itemPara)
        {
            using (var db = new SalesContext())
            {
                db.Stores.Add(itemPara);
                await UpdateDatabase(db);
                Stores.Add(itemPara);
            }
        }

        public async static Task SaveStore(Store itemPara, Store oldItem)
        {
            using (var db = new SalesContext())
            {
                db.Entry(oldItem).State = EntityState.Unchanged;
                var query = db.Stores.Find(itemPara.Id);
                CopyProperties(typeof(Store), query, itemPara);
                db.Entry(query).State = EntityState.Modified;
                db.SaveChanges();
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteStore(Store itemPara)
        {
            using (var db = new SalesContext())
            {
                db.Entry(itemPara).State = EntityState.Unchanged;                       //NOT DELETE - IMPORTANT
                Stores.Remove(itemPara);
                db.Entry(itemPara).State = EntityState.Deleted;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteManyStores(IList itemParas)
        {
            using (var db = new SalesContext())
            {
                List<Store> list = new List<Store>();
                foreach (object item in itemParas)
                {
                    list.Add((Store)item);
                }
                foreach (Store item in list)
                {
                    db.Entry(item).State = EntityState.Unchanged;
                    Stores.Remove(item);
                    db.Entry(item).State = EntityState.Deleted;
                    //Stores.Remove(item);
                    //var query = db.Stores.Find(item.Id);
                    //db.Entry(query).State = EntityState.Deleted;
                }
                await UpdateDatabase(db);
            }
        }
        #endregion


        #region Product
        public async static Task GetAllProducts()
        {
            Products.Clear();
            Categories.Clear();
            ProductPrices.Clear();
            Stores.Clear();
            GoodsReceipts.Clear();
            using (var db = new SalesContext())
            {
                try
                {
                    var result = await (from c in db.Products select c).Include("ProductPrices").ToListAsync();
                    var result1 = await (from c in db.Categories.Include("Products") select c).ToListAsync();
                    var result2 = await (from c in db.ProductPrices select c).Include("Product").Include("Store").ToListAsync();
                    var result3 = await (from c in db.Stores select c).Include("ProductPrices").ToListAsync();
                    var result4 = await (from c in db.GoodsReceipts select c).Include("ReceiptDetails").Include("Store").ToListAsync();
                    foreach (Product item in result)
                    {
                        Products.Add(item);
                    }
                    foreach (Category item in result1)
                    {
                        Categories.Add(item);
                    }
                    foreach (ProductPrice item in result2)
                    {
                        ProductPrices.Add(item);
                    }
                    foreach (Store item in result3)
                    {
                        Stores.Add(item);
                    }
                    foreach (GoodsReceipt item in result4)
                    {
                        GoodsReceipts.Add(item);
                    }
                }
                catch(Exception e)
                {
                    var x = e.Message;
                }

            }
        }

        public async static Task AddProduct(Product itemPara)
        {
            using (var db = new SalesContext())
            {
                //if (itemPara.Category != null)
                //{   //to avoid add duplicately NOT use db.attach or db.Add
                //    itemPara.CategoryID = itemPara.Category.Id;
                //    var temp = itemPara.Category;
                //    itemPara.Category = null;
                //    var cusRank = db.Categories.Find(itemPara.CategoryID);
                //    cusRank.Products.Add(itemPara);
                //    await UpdateDatabase(db);
                //    itemPara.Category = temp;
                //    Categories.Where(p => p.Id == temp.Id).Single().Products.Add(itemPara); // Update data for CategoryView
                //    Products.Add(itemPara);
                //}
                //else
                //{
                //    var x = itemPara.Category;
                //    db.Products.Add(itemPara);
                //    await UpdateDatabase(db);
                //    Products.Add(itemPara);
                //}


                if (itemPara.Category != null)
                {
                    itemPara.CategoryID = itemPara.Category.Id;         //NOT DELETE - IMPORTANT
                    db.Entry(itemPara).State = EntityState.Unchanged;   //Prevent conflick key orcur in entity, REQUIRED
                }
                db.Products.Add(itemPara);
                await UpdateDatabase(db);
                CodeGenerate(itemPara);
                await UpdateDatabase(db);
                Products.Add(itemPara);
            }
        }

        public async static Task SaveProduct(Product itemPara, Product oldItem)
        {
            using (var db = new SalesContext())
            {   //to avoid save duplicately Category, change CategoryID instead Category
                //var par = Categories.Where(p => p.Id == itemPara.CategoryID).Single();
                //var chil = par.Products.Where(p => p.Id == itemPara.Id).Single();
                //par.Products.Remove(chil);

                //itemPara.CategoryID = itemPara.Category.Id;
                //Categories.Where(p => p.Id == itemPara.CategoryID).Single().Products.Add(itemPara);

                //var query = db.Products.Find(itemPara.Id);
                //var temp = query.Category;
                //CopyProperties(typeof(Product), query, itemPara);
                //query.Category = temp;
                //db.Entry(query).State = EntityState.Modified;
                //await UpdateDatabase(db);


                db.Entry(oldItem).State = EntityState.Unchanged;        //NOT DELETE - IMPORTANT
                db.Categories.Attach(itemPara.Category);         //NOT DELETE - IMPORTANT

                var query = db.Products.Find(itemPara.Id);
                CopyProperties(typeof(Product), query, itemPara);
                query.CategoryID = itemPara.Category.Id;
                db.Entry(query).State = EntityState.Modified;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteProduct(Product itemPara)
        {
            using (var db = new SalesContext())
            {
                //db.Products.Attach(itemPara);
                //Categories.Where(p => p.Id == itemPara.CategoryID).Single().Products.Remove(itemPara);
                //Products.Remove(itemPara);
                //db.Entry(itemPara).State = EntityState.Deleted; // must be executed after deleted it in Products
                //await UpdateDatabase(db);

                db.Entry(itemPara).State = EntityState.Unchanged;                       //NOT DELETE - IMPORTANT
                Products.Remove(itemPara);
                db.Entry(itemPara).State = EntityState.Deleted;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteManyProducts(IList itemParas)
        {
            using (var db = new SalesContext())
            {
                List<Product> list = new List<Product>();
                foreach (object item in itemParas)
                {
                    list.Add((Product)item);
                }
                foreach (Product item in list)
                {
                    db.Entry(item).State = EntityState.Unchanged;
                    Products.Remove(item);
                    db.Entry(item).State = EntityState.Deleted;
                    //Products.Remove(item);
                    //Categories.Where(p => p.Id == item.CategoryID).Single().Products.Remove(item);
                    //var query = db.Products.Find(item.Id);
                    //db.Entry(query).State = EntityState.Deleted;
                }
                await UpdateDatabase(db);
            }
        }
        #endregion

        #region ProductPrice
        public async static Task GetAllProductPrices()
        {
            await GetAllProducts();
        }

        public async static Task AddProductPrice(ProductPrice itemPara)
        {
            using (var db = new SalesContext())
            {
                if (itemPara.Store != null)
                {
                    itemPara.StoreID = itemPara.Store.Id;
                    itemPara.ProductID = itemPara.Product.Id;
                    db.Entry(itemPara).State = EntityState.Unchanged;
                }
                db.ProductPrices.Add(itemPara);
                await UpdateDatabase(db);
                CodeGenerate(itemPara);
                await UpdateDatabase(db);
                ProductPrices.Add(itemPara);
            }
        }

        public async static Task SaveProductPrice(ProductPrice itemPara, ProductPrice oldItem)
        {
            using (var db = new SalesContext())
            {
                db.Entry(itemPara).State = EntityState.Modified;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteProductPrice(ProductPrice itemPara)
        {
            using (var db = new SalesContext())
            {
                //if (itemPara.Store != null)                                   //NOT DELETE - IMPORTANT
                //    db.Stores.Attach(itemPara.Store);
                //if (itemPara.Product != null)                                   //NOT DELETE - IMPORTANT
                //    db.Products.Attach(itemPara.Product);
                db.Entry(itemPara).State = EntityState.Unchanged;                       //NOT DELETE - IMPORTANT
                ProductPrices.Remove(itemPara);
                db.Entry(itemPara).State = EntityState.Deleted;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteAllProductPrices(ICollectionView itemParas)
        {
            using (var db = new SalesContext())
            {
                List<ProductPrice> list = new List<ProductPrice>();
                foreach (object item in itemParas)
                {
                    list.Add((ProductPrice)item);
                }
                foreach (ProductPrice item in list)
                {
                    db.Entry(item).State = EntityState.Unchanged;
                    ProductPrices.Remove(item);
                    db.Entry(item).State = EntityState.Deleted;
                    //Categories.Remove(item);
                    //var query = db.Categories.Find(item.Id);
                    //db.Entry(query).State = EntityState.Deleted;
                }
                await UpdateDatabase(db);
            }
        }
        #endregion



        #region Category
        public async static Task GetAllCategories()
        {
            await GetAllProducts();
        }

        public async static Task AddCategory(Category itemPara)
        {
            using (var db = new SalesContext())
            {
                db.Categories.Add(itemPara);
                await UpdateDatabase(db);
                Categories.Add(itemPara);
            }
        }

        public async static Task SaveCategory(Category itemPara, Category oldItem)
        {
            using (var db = new SalesContext())
            {
                db.Entry(oldItem).State = EntityState.Unchanged;
                var query = db.Categories.Find(itemPara.Id);
                CopyProperties(typeof(Category), query, itemPara);
                db.Entry(query).State = EntityState.Modified;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteCategory(Category itemPara)
        {
            using (var db = new SalesContext())
            {
                db.Entry(itemPara).State = EntityState.Unchanged;                       //NOT DELETE - IMPORTANT
                Categories.Remove(itemPara);
                db.Entry(itemPara).State = EntityState.Deleted;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteManyCategories(IList itemParas)
        {
            using (var db = new SalesContext())
            {
                List<Category> list = new List<Category>();
                foreach (object item in itemParas)
                {
                    list.Add((Category)item);
                }
                foreach (Category item in list)
                {
                    db.Entry(item).State = EntityState.Unchanged;
                    Categories.Remove(item);
                    db.Entry(item).State = EntityState.Deleted;
                    //Categories.Remove(item);
                    //var query = db.Categories.Find(item.Id);
                    //db.Entry(query).State = EntityState.Deleted;
                }
                await UpdateDatabase(db);
            }
        }


        #endregion

        #region ReceiptDetail
        public async static Task GetAllReceiptDetails()
        {
            await GetAllProducts();
        }

        public async static Task AddReceiptDetail(ReceiptDetail itemPara)
        {
            using (var db = new SalesContext())
            {
                if (itemPara.Product != null)
                {
                    itemPara.GoodsReceiptID = itemPara.GoodsReceipt.Id;
                    itemPara.ProductID = itemPara.Product.Id;
                    db.Entry(itemPara).State = EntityState.Unchanged;
                }
                db.ReceiptDetails.Add(itemPara);
                await UpdateDatabase(db);
                ReceiptDetails.Add(itemPara);
            }
        }

        public async static Task SaveReceiptDetail(ReceiptDetail itemPara, ReceiptDetail oldItem)
        {
            using (var db = new SalesContext())
            {
                db.Entry(itemPara).State = EntityState.Modified;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteReceiptDetail(ReceiptDetail itemPara)
        {
            using (var db = new SalesContext())
            {
                //if (itemPara.Store != null)                                   //NOT DELETE - IMPORTANT
                //    db.Stores.Attach(itemPara.Store);
                //if (itemPara.Product != null)                                   //NOT DELETE - IMPORTANT
                //    db.Products.Attach(itemPara.Product);
                db.Entry(itemPara).State = EntityState.Unchanged;                       //NOT DELETE - IMPORTANT
                ReceiptDetails.Remove(itemPara);
                db.Entry(itemPara).State = EntityState.Deleted;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteAllReceiptDetails(ICollectionView itemParas)
        {
            using (var db = new SalesContext())
            {
                List<ReceiptDetail> list = new List<ReceiptDetail>();
                foreach (object item in itemParas)
                {
                    list.Add((ReceiptDetail)item);
                }
                foreach (ReceiptDetail item in list)
                {
                    db.Entry(item).State = EntityState.Unchanged;
                    ReceiptDetails.Remove(item);
                    db.Entry(item).State = EntityState.Deleted;
                    //Categories.Remove(item);
                    //var query = db.Categories.Find(item.Id);
                    //db.Entry(query).State = EntityState.Deleted;
                }
                await UpdateDatabase(db);
            }
        }
        #endregion


        #region GoodsReceipt
        public async static Task GetAllGoodsReceipts()
        {
            await GetAllProducts();
        }

        public async static Task AddGoodsReceipt(GoodsReceipt itemPara)
        {
            using (var db = new SalesContext())
            {
                if (itemPara.Store != null)
                {
                    itemPara.StoreID = itemPara.Store.Id;
                    db.Entry(itemPara.Store).State = EntityState.Unchanged;
                }
                foreach(ReceiptDetail rd in itemPara.ReceiptDetails)
                {
                    rd.ProductID = rd.Product.Id;
                    db.Entry(rd.Product).State = EntityState.Unchanged;
                    //db.ReceiptDetails.Add(rd);
                }
                db.GoodsReceipts.Add(itemPara);
                await UpdateDatabase(db);
                CodeGenerate(itemPara);
                await UpdateDatabase(db);
                GoodsReceipts.Add(itemPara);
            }
        }

        public async static Task SaveGoodsReceipt(GoodsReceipt itemPara, GoodsReceipt oldItem)
        {
            using (var db = new SalesContext())
            {
                db.Entry(itemPara).State = EntityState.Modified;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteGoodsReceipt(GoodsReceipt itemPara)
        {
            using (var db = new SalesContext())
            {
                //if (itemPara.Store != null)                                   //NOT DELETE - IMPORTANT
                //    db.Stores.Attach(itemPara.Store);
                //if (itemPara.Product != null)                                   //NOT DELETE - IMPORTANT
                //    db.Products.Attach(itemPara.Product);
                db.Entry(itemPara).State = EntityState.Unchanged;                       //NOT DELETE - IMPORTANT
                GoodsReceipts.Remove(itemPara);
                db.Entry(itemPara).State = EntityState.Deleted;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteAllGoodsReceipts(ICollectionView itemParas)
        {
            using (var db = new SalesContext())
            {
                List<GoodsReceipt> list = new List<GoodsReceipt>();
                foreach (object item in itemParas)
                {
                    list.Add((GoodsReceipt)item);
                }
                foreach (GoodsReceipt item in list)
                {
                    db.Entry(item).State = EntityState.Unchanged;
                    GoodsReceipts.Remove(item);
                    db.Entry(item).State = EntityState.Deleted;
                    //Categories.Remove(item);
                    //var query = db.Categories.Find(item.Id);
                    //db.Entry(query).State = EntityState.Deleted;
                }
                await UpdateDatabase(db);
            }
        }
        #endregion



        #endregion


        internal static void CodeGenerate(ProductPrice model)
        {
            int numId = 0;
            numId = model.Id;
            Random rnd = new Random();
            var x = numId.ToString("D5");
            var y = x.Count();
            model.Code = "PDP" + x.Substring(y - 5, 5) + rnd.Next(0, 10);
        }

        internal static void CodeGenerate(GoodsReceipt model)
        {
            int numId = 0;
            numId = model.Id;
            Random rnd = new Random();
            var x = numId.ToString("D5");
            var y = x.Count();
            model.InvoiceCode = "IVC" + x.Substring(y - 5, 5) + rnd.Next(0, 10);
        }

        internal static void CodeGenerate(Product model)
        {
            int numId = 0;
            numId = model.Id;
            Random rnd = new Random();
            var x = numId.ToString("D5");
            var y = x.Count();
            model.Code = "PRD" + x.Substring(y - 5, 5) + rnd.Next(0, 10);
        }


        public static byte[] ImageToBytes(BitmapImage img)
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



        #region delete

        //public static SalesContext db = new SalesContext();

        //public static Customer CloneCustomer(Customer SelectedItem)
        //{
        //    using (var db = new SalesContext())
        //    {
        //        var item = db.Customers.Find(SelectedItem.Id);
        //        item.CustomerRank = CustomerRanks.Where(p => p.Id == item.CustomerRankID).Single();
        //        return item;
        //    }
        //}

        #endregion

        #region Asset
        public async static Task GetAllAssets()
        {
            Assets.Clear();
            AssetCategories.Clear();
            using (var db = new SalesContext())
            {
                var result = await (from c in db.Assets select c).ToListAsync();
                var result1 = await (from c in db.AssetCategories.Include("Assets") select c).ToListAsync();
                var result2 = await (from c in db.Departments.Include("Assets") select c).ToListAsync();
                var result3 = await (from c in db.InstallationLocations.Include("Assets") select c).ToListAsync();
                foreach (Asset item in result)
                {
                    Assets.Add(item);
                }
                foreach (AssetCategory item in result1)
                {
                    AssetCategories.Add(item);
                }
                foreach (Department item in result2)
                {
                    Departments.Add(item);
                }
                foreach (InstallationLocation item in result3)
                {
                    InstallationLocations.Add(item);
                }
            }
        }

        public async static Task AddAsset(Asset itemPara)
        {
            using (var db = new SalesContext())
            {
                if (itemPara.AssetCategory != null)
                {
                    itemPara.AssetCategoryID = itemPara.AssetCategory.Id;         //NOT DELETE - IMPORTANT
                    itemPara.InstallationLocationID = itemPara.InstallationLocation.Id;
                    itemPara.DepartmentID = itemPara.Department.Id;
                    db.Entry(itemPara).State = EntityState.Unchanged;
                }
                db.Assets.Add(itemPara);
                await UpdateDatabase(db);
                Assets.Add(itemPara);
            }
        }

        public async static Task SaveAsset(Asset itemPara, Asset oldItem)
        {
            using (var db = new SalesContext())
            {

                db.Entry(oldItem).State = EntityState.Unchanged;        //NOT DELETE - IMPORTANT
                db.AssetCategories.Attach(itemPara.AssetCategory);         //NOT DELETE - IMPORTANT
                db.Departments.Attach(itemPara.Department);
                db.InstallationLocations.Attach(itemPara.InstallationLocation);

                var query = db.Assets.Find(itemPara.Id);
                CopyProperties(typeof(Asset), query, itemPara);
                query.AssetCategoryID = itemPara.AssetCategory.Id;
                query.DepartmentID = itemPara.Department.Id;
                query.InstallationLocationID = itemPara.InstallationLocation.Id;
                db.Entry(query).State = EntityState.Modified;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteAsset(Asset itemPara)
        {
            using (var db = new SalesContext())
            {
                db.Entry(itemPara).State = EntityState.Unchanged;                       //NOT DELETE - IMPORTANT
                Assets.Remove(itemPara);
                db.Entry(itemPara).State = EntityState.Deleted;     // must be executed after deleted it in Assets
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteManyAssets(IList itemParas)
        {
            using (var db = new SalesContext())
            {
                List<Asset> list = new List<Asset>();
                foreach (object item in itemParas)
                {
                    list.Add((Asset)item);
                }
                foreach (Asset item in list)
                {
                    db.Entry(item).State = EntityState.Unchanged;
                    Assets.Remove(item);
                    db.Entry(item).State = EntityState.Deleted;
                }
                await UpdateDatabase(db);
            }
        }
        #endregion

        #region AssetCategory
        public async static Task GetAllAssetCategories()
        {
            await GetAllAssets();
        }

        public async static Task AddAssetCategory(AssetCategory itemPara)
        {
            using (var db = new SalesContext())
            {
                db.AssetCategories.Add(itemPara);
                await UpdateDatabase(db);
                AssetCategories.Add(itemPara);
            }
        }

        public async static Task SaveAssetCategory(AssetCategory itemPara, AssetCategory oldItem)
        {
            using (var db = new SalesContext())
            {
                db.Entry(oldItem).State = EntityState.Unchanged;
                var query = db.AssetCategories.Find(itemPara.Id);
                CopyProperties(typeof(AssetCategory), query, itemPara);
                db.Entry(query).State = EntityState.Modified;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteAssetCategory(AssetCategory itemPara)
        {
            using (var db = new SalesContext())
            {
                db.Entry(itemPara).State = EntityState.Unchanged;                       //NOT DELETE - IMPORTANT
                AssetCategories.Remove(itemPara);
                db.Entry(itemPara).State = EntityState.Deleted;     // must be executed after deleted it in AssetCategories
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteManyAssetCategories(IList itemParas)
        {
            using (var db = new SalesContext())
            {
                List<AssetCategory> list = new List<AssetCategory>();
                foreach (object item in itemParas)
                {
                    list.Add((AssetCategory)item);
                }
                foreach (AssetCategory item in list)
                {
                    db.Entry(item).State = EntityState.Unchanged;
                    AssetCategories.Remove(item);
                    db.Entry(item).State = EntityState.Deleted;
                }
                await UpdateDatabase(db);
            }
        }
        #endregion

        #region Department
        public async static Task GetAllDepartments()
        {
            await GetAllAssets();
        }

        public async static Task AddDepartment(Department itemPara)
        {
            using (var db = new SalesContext())
            {
                db.Departments.Add(itemPara);
                await UpdateDatabase(db);
                Departments.Add(itemPara);
            }
        }

        public async static Task SaveDepartment(Department itemPara, Department oldItem)
        {
            using (var db = new SalesContext())
            {
                db.Entry(oldItem).State = EntityState.Unchanged;
                var query = db.Departments.Find(itemPara.Id);
                CopyProperties(typeof(Department), query, itemPara);
                db.Entry(query).State = EntityState.Modified;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteDepartment(Department itemPara)
        {
            using (var db = new SalesContext())
            {
                db.Entry(itemPara).State = EntityState.Unchanged;                       //NOT DELETE - IMPORTANT
                Departments.Remove(itemPara);
                db.Entry(itemPara).State = EntityState.Deleted;     // must be executed after deleted it in Departments
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteManyDepartments(IList itemParas)
        {
            using (var db = new SalesContext())
            {
                List<Department> list = new List<Department>();
                foreach (object item in itemParas)
                {
                    list.Add((Department)item);
                }
                foreach (Department item in list)
                {
                    db.Entry(item).State = EntityState.Unchanged;
                    Departments.Remove(item);
                    db.Entry(item).State = EntityState.Deleted;
                }
                await UpdateDatabase(db);
            }
        }
        #endregion

        #region InstallationLocation
        public async static Task GetAllInstallationLocations()
        {
            await GetAllAssets();
        }

        public async static Task AddInstallationLocation(InstallationLocation itemPara)
        {
            using (var db = new SalesContext())
            {
                db.InstallationLocations.Add(itemPara);
                await UpdateDatabase(db);
                InstallationLocations.Add(itemPara);
            }
        }

        public async static Task SaveInstallationLocation(InstallationLocation itemPara, InstallationLocation oldItem)
        {
            using (var db = new SalesContext())
            {
                db.Entry(oldItem).State = EntityState.Unchanged;
                var query = db.InstallationLocations.Find(itemPara.Id);
                CopyProperties(typeof(InstallationLocation), query, itemPara);
                db.Entry(query).State = EntityState.Modified;
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteInstallationLocation(InstallationLocation itemPara)
        {
            using (var db = new SalesContext())
            {
                db.Entry(itemPara).State = EntityState.Unchanged;                       //NOT DELETE - IMPORTANT
                InstallationLocations.Remove(itemPara);
                db.Entry(itemPara).State = EntityState.Deleted;     // must be executed after deleted it in InstallationLocations
                await UpdateDatabase(db);
            }
        }

        public async static Task DeleteManyInstallationLocations(IList itemParas)
        {
            using (var db = new SalesContext())
            {
                List<InstallationLocation> list = new List<InstallationLocation>();
                foreach (object item in itemParas)
                {
                    list.Add((InstallationLocation)item);
                }
                foreach (InstallationLocation item in list)
                {
                    db.Entry(item).State = EntityState.Unchanged;
                    InstallationLocations.Remove(item);
                    db.Entry(item).State = EntityState.Deleted;
                }
                await UpdateDatabase(db);
            }
        }
        #endregion

    }
}
