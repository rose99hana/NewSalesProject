using NewSalesProject.Enum;
using NewSalesProject.Interfaces;
using NewSalesProject.Model;
using NewSalesProject.Supports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace NewSalesProject.Views
{
    public class ProductPriceViewModel : CRUDBase<ProductPrice>, ICRUDViewModel
    {
        public ProductPriceViewModel()
        {
            Name = "Product Price";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.ProductPrices);

            JPYRate = (Decimal)Properties.Settings.Default["JPYRate"];
            JPYTax= (Decimal)Properties.Settings.Default["JPYTax"];

            USDRate = (Decimal)Properties.Settings.Default["USDRate"];
            USDTax = (Decimal)Properties.Settings.Default["USDTax"];

            EURRate = (Decimal)Properties.Settings.Default["EURRate"];
            EURTax = (Decimal)Properties.Settings.Default["EURTax"];
        }

        private Product selectedProduct;
        private Decimal MaxPrice = 0;
        private Decimal MinPrice = 0;

        public override void OnSelectedItemChanged()        //Not delete, prevent InselectedItem Changed
        {
        }

        public async void GetPricesOfProduct(Product productPara)
        {
            if (productPara == null)
                DataGridSpinnerState = SpinnerState.Loading;
            else DataGridSpinnerState = SpinnerState.Searching;
            DataGridState = ViewModeType.Busy;
            await Task.Delay(150);

            selectedProduct = productPara;

            if (selectedProduct == null)
            {
                ViewItems.Filter = CurrencyFilter;

                await Task.Run(() =>
                {
                    foreach (ProductPrice item in ViewItems)
                    {
                        CaculateRealPrice(item);
                    }
                });
            }
            else
            {
                ViewItems.Filter = ProductIdFilter;

                await Task.Run(() =>
                {
                    CancelEdit();

                    MinPrice = 0;
                    MaxPrice = 0;
                    try
                    {
                        foreach (ProductPrice item in ViewItems)
                        {
                            CaculateRealPrice(item);

                            if (MaxPrice == 0)
                            {
                                MaxPrice = item.ExchangeRealPrice;
                                selectedProduct.HighestProductPrice = item;
                            }
                            if (MinPrice == 0)
                            {
                                MinPrice = item.ExchangeRealPrice;
                                selectedProduct.LowestProductPrice = item;
                            }

                            if (item.ExchangeRealPrice > MaxPrice)
                            {
                                MaxPrice = item.ExchangeRealPrice;
                                selectedProduct.HighestProductPrice = item;
                            }
                            if (item.ExchangeRealPrice < MinPrice)
                            {
                                MinPrice = item.ExchangeRealPrice;
                                selectedProduct.LowestProductPrice = item;
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        var x = e.Message;
                    }
                });

            }

            DataGridState = ViewModeType.Default;
        }


        protected override void GetData()
        {
            selectedItem = null;
            OnPropertyChanged("SelectedItem");
            GetPricesOfProduct(selectedProduct);
        }

        protected override void CreateNew()
        {
            NewItem = new ProductPrice();
            NewItem.Product = selectedProduct;
        }

        protected async override void Add()
        {
            CRUDType = CRUDType.Adding;
            CRUDState = CRUDCardState.Busy;
            UpdateItem(NewItem);
            await DataAccess.AddProductPrice(NewItem);
            SelectedItem = NewItem;
            CRUDState = CRUDCardState.Default;
        }

        protected override void Edit()
        {
            InEditItem = new ProductPrice();
            DataAccess.CopyProperties(typeof(ProductPrice), InEditItem, SelectedItem);
            SelectedItem.IsEditable = true;
        }

        public override void CancelEdit()
        {
            if (selectedItem == null) return;
            if (selectedItem.IsEditable == true)
            {
                DataAccess.CopyProperties(typeof(ProductPrice), SelectedItem, InEditItem);
                UpdateItem(SelectedItem);
            }

        }

        public void CancelEditFromView()
        {
            if (selectedItemBeforeChange != null && selectedItemBeforeChange.IsEditable == true)
            {
                DataAccess.CopyProperties(typeof(ProductPrice), selectedItemBeforeChange, InEditItem);
                UpdateItem(selectedItemBeforeChange);
            }
        }

        private void UpdateItem(ProductPrice item)
        {
            CaculateRealPrice(item);
            item.RaisePropertyChanged("Price");
            item.RaisePropertyChanged("Discount");
            item.RaisePropertyChanged("Coupon");
            item.RaisePropertyChanged("Point");
            item.RaisePropertyChanged("Note");
            item.RaisePropertyChanged("CurrencySymbol");
            item.RaisePropertyChanged("IsTaxIncluding");
        }

        protected async override void Save()
        {
            CRUDType = CRUDType.Saving;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.SaveProductPrice(SelectedItem, null);
            SelectedItem.IsEditable = false;
            UpdateItem(SelectedItem);
            CRUDState = CRUDCardState.Default;
        }

        protected async override void Delete()
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.DeleteProductPrice(SelectedItem);
            SelectedItem = null;
            ReFocusRow(DataAccess.ProductPrices.Count);
            CRUDState = CRUDCardState.Default;
        }

        protected override async void DeleteAll()
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.DeleteAllProductPrices(ViewItems);
            SelectedItem = null;
            CRUDState = CRUDCardState.Default;
        }

        protected void CaculateRealPrice(ProductPrice item)
        {
            switch (item.CurrencySymbol)
            {
                case "₫":
                    break;
                case "$":
                    CaculateUSDRealPrice(item);
                    item.Tax = USDTax;
                    break;
                case "¥":
                    CaculateJPYRealPrice(item);
                    item.Tax = JPYTax;
                    break;
                case "€":
                    CaculateEURRealPrice(item);
                    item.Tax = EURTax;
                    break;
            }
        }

        private void CaculateJPYRealPrice(ProductPrice item)
        {
            if(item.IsTaxIncluding == false)
            {
                item.RealPrice = item.Price * (1 - item.Discount / 100 + JPYTax / 100) - item.Coupon;
                item.ExchangeRealPrice = (int)(item.RealPrice * JPYrate);

            }
            else
            {
                item.RealPrice = item.Price * (1 - item.Discount / 100) - item.Coupon;
                item.ExchangeRealPrice = (int)(item.RealPrice * JPYrate);
            }
        }

        private void CaculateUSDRealPrice(ProductPrice item)
        {
            if (item.IsTaxIncluding == false)
            {
                item.RealPrice = item.Price * (1 - item.Discount / 100 + USDTax / 100) - item.Coupon;
                item.ExchangeRealPrice = (int)(item.RealPrice * USDrate);
            }
            else
            {
                item.RealPrice = item.Price * (1 - item.Discount / 100) - item.Coupon;
                item.ExchangeRealPrice = (int)(item.RealPrice * USDrate);
            }
        }

        private void CaculateEURRealPrice(ProductPrice item)
        {
            if (item.IsTaxIncluding == false)
            {
                item.RealPrice = item.Price * (1 - item.Discount / 100 + EURTax / 100) - item.Coupon;
                item.ExchangeRealPrice = (int)(item.RealPrice * EURrate);
            }
            else
            {
                item.RealPrice = item.Price * (1 - item.Discount / 100) - item.Coupon;
                item.ExchangeRealPrice = (int)(item.RealPrice * EURrate);
            }
        }


        private ICommand addPriceToInvoiceCommand;
        public ICommand AddPriceToInvoiceCommand
        {
            get
            {
                if (addPriceToInvoiceCommand == null)
                {
                    addPriceToInvoiceCommand = new RelayCommand(
                        p => AddProductPriceToInvoice((int)p));
                }
                return addPriceToInvoiceCommand;
            }
        }

        private void AddProductPriceToInvoice(int quantity)
        {
            //var temp = new ReceiptDetail();
            //temp.Product = selectedItem.Product;
            //temp.ProductID = selectedItem.ProductID;
            //temp.Price = selectedItem.Price;

        }

        //protected async void Add(Store s)
        //{
        //    CRUDType = CRUDType.Adding;
        //    CRUDState = CRUDCardState.Busy;
        //    NewItem.Store = s;
        //    await DataAccess.AddProductPrice(NewItem);
        //    SelectedItem = NewItem;
        //    CRUDState = CRUDCardState.Default;
        //}


        //protected ICommand addProductPriceCommand;
        //public ICommand AddProductPriceCommand
        //{
        //    get
        //    {
        //        if (addProductPriceCommand == null)
        //        {
        //            addProductPriceCommand = new RelayCommand(
        //                p => Add((Store)p));
        //        }
        //        return addProductPriceCommand;
        //    }
        //}


        private bool ProductIdFilter(object item)
        {
            ProductPrice pr = item as ProductPrice;
            if(pr.ProductID == selectedProduct.Id)
            {
                return CurrencyFilter(pr);
            } else return false;
        }

        private bool CurrencyFilter(object pr)
        {
            ProductPrice item = pr as ProductPrice;
            var result = true;
            if (USDfilter == false)
            {
                if (item.CurrencySymbol == "$")
                    result = false;
            }
            if (JPYfilter == false)
            {
                if (item.CurrencySymbol == "¥")
                    result = false;
            }
            if (EURfilter == false)
            {
                if (item.CurrencySymbol == "€")
                    result = false;
            }
            return result;
        }


        private Decimal JPYrate;
        public Decimal JPYRate
        {
            get
            {
                return JPYrate;
            }
            set
            {
                JPYrate = value;
                OnPropertyChanged("JPYRate");
            }
        }

        private Decimal USDrate;
        public Decimal USDRate
        {
            get
            {
                return USDrate;
            }
            set
            {
                USDrate = value;
                OnPropertyChanged("USDRate");
            }
        }

        private Decimal EURrate;
        public Decimal EURRate
        {
            get
            {
                return EURrate;
            }
            set
            {
                EURrate = value;
                OnPropertyChanged("EURRate");
            }
        }

        private Decimal JPYtax;
        public Decimal JPYTax
        {
            get
            {
                return JPYtax;
            }
            set
            {
                JPYtax = value;
                OnPropertyChanged("JPYTax");
            }
        }

        private Decimal USDtax;
        public Decimal USDTax
        {
            get
            {
                return USDtax;
            }
            set
            {
                USDtax = value;
                OnPropertyChanged("USDTax");
            }
        }

        private Decimal EURtax;
        public Decimal EURTax
        {
            get
            {
                return EURtax;
            }
            set
            {
                EURtax = value;
                OnPropertyChanged("EURTax");
            }
        }

        private bool EURfilter = true;
        public bool EURFilter
        {
            get
            {
                return EURfilter;
            }
            set
            {
                EURfilter = value;
                OnPropertyChanged("EURFilter");
                if (selectedProduct != null)
                    ViewItems.Filter = ProductIdFilter;
                else ViewItems.Filter = CurrencyFilter;
            }
        }

        private bool JPYfilter = true;
        public bool JPYFilter
        {
            get
            {
                return JPYfilter;
            }
            set
            {
                JPYfilter = value;
                OnPropertyChanged("JPYFilter");
                if (selectedProduct != null)
                    ViewItems.Filter = ProductIdFilter;
                else ViewItems.Filter = CurrencyFilter;
            }
        }

        private bool USDfilter = true;
        public bool USDFilter
        {
            get
            {
                return USDfilter;
            }
            set
            {
                USDfilter = value;
                OnPropertyChanged("USDFilter");
                if (selectedProduct != null)
                    ViewItems.Filter = ProductIdFilter;
                else ViewItems.Filter = CurrencyFilter;
            }
        }

    }
}
