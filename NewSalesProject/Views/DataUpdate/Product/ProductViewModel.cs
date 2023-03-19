using Microsoft.Win32;
using NewSalesProject.Enum;
using NewSalesProject.Interfaces;
using NewSalesProject.Model;
using NewSalesProject.Supports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace NewSalesProject.Views
{
    public class ProductViewModel : CRUDBase<Product>, ICRUDViewModel
    {
        public ProductViewModel()
        {
            Name = "Product";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.Products);
            DtGridProperties = (string)Properties.Settings.Default["ProductDtGridProperties"];
            ProductPriceVM = new ProductPriceViewModel();
            GoodsReceiptVM = new GoodsReceiptViewModel();
            WishListVM = new WishListViewModel();

        }

        private int test = 5;
        public int Test
        {
            get { return test; }
            set
            {
                test = value;
                OnPropertyChanged("Test");
            }
        }

        protected ProductPriceViewModel productPriceVM;
        public ProductPriceViewModel ProductPriceVM
        {
            get
            {
                return productPriceVM;
            }
            set
            {
                productPriceVM = value;
                OnPropertyChanged("ProductPriceVM");
            }
        }

        protected GoodsReceiptViewModel goodsReceiptVM;
        public GoodsReceiptViewModel GoodsReceiptVM
        {
            get
            {
                return goodsReceiptVM;
            }
            set
            {
                goodsReceiptVM = value;
                OnPropertyChanged("GoodsReceiptVM");
            }
        }

        protected WishListViewModel wishListVM;
        public WishListViewModel WishListVM
        {
            get
            {
                return wishListVM;
            }
            set
            {
                wishListVM = value;
                OnPropertyChanged("WishListVM");
            }
        }

        public override void SaveProperties(Dictionary<string, string> columnDictionary)
        {
            base.SaveProperties(columnDictionary);
            Properties.Settings.Default["ProductDtGridProperties"] = DtGridProperties;
            Properties.Settings.Default.Save();
        }

        public override void OnSelectedItemChanged()
        {
            if (IsAutoUpdateEnable == true)
            {
                InEditItem = SelectedItem;
                try
                {
                    if (selectedItemBeforeChange!= null && selectedItem != null && selectedItem.Id != selectedItemBeforeChange.Id)
                        ProductPriceVM.GetPricesOfProduct(selectedItem);
                }
                catch
                {
                    ProductPriceVM.GetPricesOfProduct(selectedItem);
                }
                GoodsReceiptVM.ReceiptDetailVM.OnSelectedProductChanged(SelectedItem);
            }
        }

        protected async override void GetData()
        {
            DataGridSpinnerState = SpinnerState.Loading;
            DataGridState = ViewModeType.Busy;
            ProductPriceVM.DataGridSpinnerState = SpinnerState.Loading;
            ProductPriceVM.DataGridState = ViewModeType.Busy;
            await Task.Delay(300);
            ClearFilter();
            await DataAccess.GetAllProducts();
            DataGridState = ViewModeType.Default;
            ProductPriceVM.DataGridState = ViewModeType.Default;
        }

        protected override void CreateNew()
        {
            NewItem = null;
            Product newCus = new Product { Name = "" };
            NewItem = newCus;
        }

        protected async override void Add()
        {
            CRUDType = CRUDType.Adding;
            CRUDState = CRUDCardState.Busy;
            await Task.Delay(300);
            await DataAccess.AddProduct(NewItem);
            SelectedItem = NewItem;
            CRUDState = CRUDCardState.Default;
        }


        protected override void Edit()
        {
            var newProduct = new Product();
            newProduct.Category = new Category();
            DataAccess.CopyProperties(typeof(Product), newProduct, SelectedItem);
            InEditItem = newProduct;
        }

        protected async override void Save()
        {
            CRUDType = CRUDType.Saving;
            CRUDState = CRUDCardState.Busy;
            await Task.Delay(300);
            await DataAccess.SaveProduct(InEditItem, SelectedItem);

            var temp = SelectedItem;                                        // Update data of SelectedItem in View, REQUIRED
            DataAccess.Products[currentIndex] = new Product();            // NOT DELETE - IMPORTANT
            DataAccess.Products[currentIndex] = temp;
            SelectedItem = temp;

            CRUDState = CRUDCardState.Default;
        }

        protected async override void Delete()
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            await Task.Delay(300);
            var x = SelectedItem;
            await DataAccess.DeleteProduct(SelectedItem);
            SelectedItem = null;
            ReFocusRow(DataAccess.Products.Count);
            CRUDState = CRUDCardState.Default;
        }

        protected async override void Duplicate()
        {
            CRUDType = CRUDType.Copying;
            CRUDState = CRUDCardState.Busy;
            await Task.Delay(300);
            Product newItem = new Product();
            newItem.Category = new Category();
            DataAccess.CopyProperties(typeof(Product), newItem, SelectedItem);
            newItem.Id = 0;
            await DataAccess.AddProduct(newItem);
            SelectedIndex = DataAccess.Products.Count - 1;
            CRUDState = CRUDCardState.Default;
        }

        public async override void DeleteMany(IList items)
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            await Task.Delay(300);
            await DataAccess.DeleteManyProducts(items);
            SelectedItem = null;
            ReFocusRow(DataAccess.Products.Count);
            CRUDState = CRUDCardState.Default;
        }

        //protected ProductPrice newProductPriceItem;
        //public ProductPrice NewProductPriceItem
        //{
        //    get
        //    {
        //        return newProductPriceItem;
        //    }
        //    set
        //    {
        //        newProductPriceItem = value;
        //        OnPropertyChanged("NewProductPriceItem");
        //    }
        //}

        //protected ICommand addProductPriceCommand;
        //public ICommand AddProductPriceCommand
        //{
        //    get
        //    {
        //        if (addProductPriceCommand == null)
        //        {
        //            addProductPriceCommand = new RelayCommand(
        //                p => AddProductPrice((Store)p));
        //        }
        //        return addProductPriceCommand;
        //    }
        //}

        //private async void AddProductPrice(Store storePara)
        //{
        //    NewProductPriceItem.Store = storePara;
        //    NewProductPriceItem.Product = SelectedItem;
        //    await DataAccess.AddProductPrice(NewProductPriceItem);
        //    NewProductPriceItem = new ProductPrice();
        //}


        //protected ICommand deleteProductPriceCommand;
        //public ICommand DeleteProductPriceCommand
        //{
        //    get
        //    {
        //        if (deleteProductPriceCommand == null)
        //        {
        //            deleteProductPriceCommand = new RelayCommand(
        //                p => DeleteProductPrice((ProductPrice)p));
        //        }
        //        return deleteProductPriceCommand;
        //    }
        //}

        //private async void DeleteProductPrice(ProductPrice p)
        //{
        //    await DataAccess.DeleteProductPrice(p);
        //}


        protected ICommand addProductPriceToWishListCommand;
        public ICommand AddProductPriceToWishListCommand
        {
            get
            {
                if (addProductPriceToWishListCommand == null)
                {
                    addProductPriceToWishListCommand = new RelayCommand(
                        p => AddToWishList(Convert.ToInt32(p)));
                }
                return addProductPriceToWishListCommand;
            }
        }

        private string qty = "";
        public string Qty
        {
            get { return qty; }
            set
            {
                qty = value;
                OnPropertyChanged("Qty");
            }
        }

        private void AddToWishList(int quantity)
        {
            WishListDetail temp = new WishListDetail();
            temp.Quantity = quantity;
            temp.ProductPrice = ProductPriceVM.SelectedItem;
            temp.ProductPriceID = ProductPriceVM.SelectedItem.Id;
            temp.StoreName = ProductPriceVM.SelectedItem.Store.Name;
            temp.CurrencySymbol = temp.ProductPrice.Store.CurrencySymbol;
            DataAccess.WishListDetails.Add(temp);
            WishListVM.WishListDetailVM.ViewItems.GroupDescriptions.Clear();
            WishListVM.WishListDetailVM.ViewItems.GroupDescriptions.Add(new PropertyGroupDescription("StoreName"));
            Qty = "";
        }


        public ICommand UpdateImageCommand
        {
            get { return new RelayCommand((p) => DetermineDialogPara(p)); }
        }

        public ICommand DeleteImageCommand
        {
            get { return new RelayCommand((p) => DetermineDialogPara(p)); }
        }

        private void DetermineDialogPara(object p)
        {
            switch (p as string)
            {
                case "New":
                    OpenImageDialog(NewItem);
                    break;
                case "Detail":
                    OpenImageDialog(InEditItem);
                    break;
            }
        }

        private void OpenImageDialog(Product itemPara)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select a picture";
            dialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (dialog.ShowDialog() == true)
            {
                itemPara.Picture = DataAccess.ImageToBytes(new BitmapImage(new Uri(dialog.FileName)));
                itemPara.RaisePropertyChanged("Picture");
            }
        }

        private void DeleteDisplayImage(Product itemPara)
        {
            itemPara.Picture = DataAccess.ImageToBytes(new BitmapImage(new Uri($"pack://application:,,,/Images/NoImage.jpeg")));
            itemPara.RaisePropertyChanged("Picture");
        }

        //protected string CodeGenerate(IModel model)
        //{
        //    int numId = 0;
        //    //int lastid = DataAccess.Products.Last().Id;
        //    //numId = (lastid + 1);
        //    numId = model.Id + 1;
        //    Random rnd = new Random();
        //    var x = numId.ToString("D3");
        //    var y = x.Count();
        //    var z = "PDP" + x.Substring(y - 3, 3) + rnd.Next(0, 10);
        //    return z;
        //}







        #region Filter Product

        protected async override void FilterData()
        {
            SearchKeyword.Trim();
            DataGridSpinnerState = SpinnerState.Searching;
            DataGridState = ViewModeType.Busy;
            await Task.Delay(400);
            switch (SearchProperty.HeaderName)
            {
                case "Id":
                    ViewItems.Filter = ProductIdFilter;
                    break;
                case "Name":
                    ViewItems.Filter = ProductNameFilter;
                    break;
                case "OriginalName":
                    ViewItems.Filter = ProductOriginalNameFilter;
                    break;
                //case "IDCard Number":
                //    ViewItems.Filter = ProductIDCardNumberFilter;
                //    break;
                //case "Address":
                //    ViewItems.Filter = ProductAddressFilter;
                //    break;
                //case "Relationship":
                //    ViewItems.Filter = ProductRelationshipFilter;
                //    break;
                //case "Product Rank":
                //    ViewItems.Filter = CategoryFilter;
                //    break;
                default:
                    break;
            }
            IsFiltered = true;
            DataGridState = ViewModeType.Default;

        }

        private bool ProductIdFilter(object item)
        {
            Product Product = item as Product;
            return Product.Id.ToString().ToLower().Equals((SearchKeyword.ToLower()));
        }

        private bool ProductNameFilter(object item)
        {
            Product Product = item as Product;
            if (Product.Name == null) return false;
            return Product.Name.ToLower().Contains(SearchKeyword.ToLower());
        }

        private bool ProductOriginalNameFilter(object item)
        {
            Product Product = item as Product;
            if (Product.OriginalName == null) return false;
            return Product.OriginalName.ToLower().Contains(SearchKeyword.ToLower());
        }

        //private bool ProductIDCardNumberFilter(object item)
        //{
        //    Product Product = item as Product;
        //    if (Product.Tel == null) return false;
        //    return Product.IdCardNumber.ToLower().Contains(SearchKeyword.ToLower());
        //}

        //private bool ProductAddressFilter(object item)
        //{
        //    bool x = false, y = false;
        //    Product Product = item as Product;
        //    if (Product.Address1 != null)
        //    {
        //        x = Product.Address1.ToLower().Contains(SearchKeyword.ToLower());
        //    }
        //    if (Product.Address2 != null)
        //    {
        //        y = Product.Address2.ToLower().Contains(SearchKeyword.ToLower());
        //    }
        //    return x || y;
        //}

        //private bool ProductRelationshipFilter(object item)
        //{
        //    Product Product = item as Product;
        //    if (Product.Relationship == null) return false;
        //    return Product.Relationship.ToLower().Contains(SearchKeyword.ToLower());
        //}

        //private bool CategoryFilter(object item)
        //{
        //    Product Product = item as Product;
        //    if (Product.Category.Name == null) return false;
        //    return Product.Category.Name.ToLower().Contains(SearchKeyword.ToLower());
        //}

        #endregion
    }
}
