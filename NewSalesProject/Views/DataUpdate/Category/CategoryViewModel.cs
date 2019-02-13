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

namespace NewSalesProject.Views
{
    public class CategoryViewModel : CRUDBase<Category>, ICRUDViewModel
    {
        public CategoryViewModel()
        {
            Name = "Category";
            ViewItems = CollectionViewSource.GetDefaultView(DataAccess.Categories);
            DtGridProperties = Properties.Settings.Default.CategoryDtGridProperties;
        }

        public override void SaveProperties(Dictionary<string, string> columnDictionary)
        {
            base.SaveProperties(columnDictionary);
            Properties.Settings.Default["CategoryDtGridProperties"] = DtGridProperties;
            Properties.Settings.Default.Save();
        }

        protected async override void GetData()
        {
            DataGridSpinnerState = SpinnerState.Loading;
            DataGridState = ViewModeType.Busy;
            await Task.Delay(150);
            await DataAccess.GetAllCategories();
            ClearFilter();
            DataGridState = ViewModeType.Default;
        }

        protected override void CreateNew()
        {
            this.NewItem = null;
            Category newCategory = new Category { Name = "" };
            this.NewItem = newCategory;
        }

        protected async override void Add()
        {
            CRUDType = CRUDType.Adding;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.AddCategory(NewItem);
            SelectedItem = NewItem;
            CRUDState = CRUDCardState.Default;
        }


        protected override void Edit()
        {
            var newCategory = new Category();
            DataAccess.CopyProperties(typeof(Category), newCategory, SelectedItem);
            InEditItem = newCategory;
        }

        protected async override void Save()
        {
            CRUDType = CRUDType.Saving;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.SaveCategory(InEditItem, SelectedItem);

            var temp = SelectedItem;                                        // Update data of SelectedItem in View, REQUIRED
            DataAccess.Categories[currentIndex] = new Category();            // NOT DELETE - IMPORTANT
            DataAccess.Categories[currentIndex] = temp;
            SelectedItem = temp;

            CRUDState = CRUDCardState.Default;
        }

        protected async override void Delete()
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.DeleteCategory(SelectedItem);
            SelectedItem = null;
            ReFocusRow(DataAccess.Categories.Count);
            CRUDState = CRUDCardState.Default;
        }

        protected async override void Duplicate()
        {
            CRUDType = CRUDType.Copying;
            CRUDState = CRUDCardState.Busy;
            Category newItem = new Category();
            DataAccess.CopyProperties(typeof(Category), newItem, SelectedItem);
            newItem.Id = 0;
            await DataAccess.AddCategory(newItem);
            SelectedIndex = DataAccess.Categories.Count - 1;
            CRUDState = CRUDCardState.Default;
        }

        public async override void DeleteMany(IList items)
        {
            CRUDType = CRUDType.Deleting;
            CRUDState = CRUDCardState.Busy;
            await DataAccess.DeleteManyCategories(items);
            SelectedItem = null;
            ReFocusRow(DataAccess.Categories.Count);
            CRUDState = CRUDCardState.Default;
        }

        #region Filter Category

        protected async override void FilterData()
        {
            SearchKeyword.Trim();
            DataGridSpinnerState = SpinnerState.Searching;
            DataGridState = ViewModeType.Busy;
            await Task.Delay(500);
            switch (SearchProperty.HeaderName)
            {
                case "Id":
                    ViewItems.Filter = CategoryIdFilter;
                    break;
                case "Name":
                    ViewItems.Filter = CategoryNameFilter;
                    break;
                default:
                    break;
            }
            IsFiltered = true;
            DataGridState = ViewModeType.Default;

        }

        private bool CategoryIdFilter(object item)
        {
            Category Category = item as Category;
            return Category.Id.ToString().ToLower().Equals((SearchKeyword.ToLower()));
        }

        private bool CategoryNameFilter(object item)
        {
            Category Category = item as Category;
            if (Category.Name == null) return false;
            return Category.Name.ToLower().Contains(SearchKeyword.ToLower());
        }

        #endregion

    }
}
