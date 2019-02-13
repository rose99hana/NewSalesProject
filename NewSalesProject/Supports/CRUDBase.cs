using NewSalesProject.Controls;
using NewSalesProject.Enum;
using NewSalesProject.Interfaces;
using NewSalesProject.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NewSalesProject.Supports
{
    public class CRUDBase<T> : NotifyUIBase, IViewModel
    {
        public CRUDBase()
        {
        }

        #region IViewModel Interface implement

        public string Name { get; set; }

        protected bool _changeViewCommandIsEnable = true;
        public bool ChangeViewCommandIsEnable
        {
            get
            {
                return _changeViewCommandIsEnable;
            }
            set
            {
                _changeViewCommandIsEnable = value;
                OnPropertyChanged("ChangeViewCommandIsEnable");
            }
        }

        #endregion



        #region DataGrid Properties

        protected ViewModeType dataGridState;
        public ViewModeType DataGridState
        {
            get
            {
                return dataGridState;
            }
            set
            {
                dataGridState = value;
                OnPropertyChanged("DataGridState");
            }
        }

        protected SpinnerState dataGridSpinnerState;
        public SpinnerState DataGridSpinnerState
        {
            get
            {
                return dataGridSpinnerState;
            }
            set
            {
                dataGridSpinnerState = value;
                OnPropertyChanged("DataGridSpinnerState");
            }
        }


        public string DtGridProperties { get; set; }
        public virtual void SaveProperties(Dictionary<string, string> columnDictionary)
        {
            DtGridProperties = "";
            foreach (var item in columnDictionary)
            {
                DtGridProperties += item.Key + "=" + item.Value + ";";
            }
        }

        protected bool isFiltered;
        public bool IsFiltered
        {
            get
            {
                return isFiltered;
            }
            set
            {
                isFiltered = value;
                OnPropertyChanged("IsFiltered");
            }
        }

        protected bool isAutoUpdateEnable = true;
        public bool IsAutoUpdateEnable
        {
            get
            {
                return isAutoUpdateEnable;
            }
            set
            {
                isAutoUpdateEnable = value;
                OnPropertyChanged("IsAutoUpdateEnable");
            }
        }

        protected string searchKeyword = "";
        public string SearchKeyword
        {
            get
            {
                return searchKeyword;
            }
            set
            {
                searchKeyword = value;
                OnPropertyChanged("SearchKeyword");
            }
        }

        protected DataGridColumnPropertyItem searchProperty = new DataGridColumnPropertyItem();
        public DataGridColumnPropertyItem SearchProperty
        {
            get
            {
                return searchProperty;
            }
            set
            {
                searchProperty = value;
                OnPropertyChanged("SearchProperty");
            }
        }

        protected T selectedItemBeforeChange;
        protected T selectedItem;
        public dynamic SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                OnSelectedItemChange();
                selectedItem = value;
                OnSelectedItemChanged();
                OnPropertyChanged("SelectedItem");
            }
        }

        protected T inEditItem;
        public T InEditItem
        {
            get { return inEditItem; }
            set
            {
                inEditItem = value;
                OnPropertyChanged("InEditItem");
            }
        }

        public virtual void OnSelectedItemChange()
        {
            selectedItemBeforeChange = selectedItem;
        }

        public virtual void OnSelectedItemChanged()
        {
            if (IsAutoUpdateEnable == true)
            {
                InEditItem = SelectedItem;
            }
        }

        protected T newItem;
        public T NewItem
        {
            get
            {
                return newItem;
            }
            set
            {
                newItem = value;
                OnPropertyChanged("NewItem");
            }
        }

        protected int currentIndex = 0;
        protected int selectedIndex = 0;
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                selectedIndex = value;
                if (value >= 0)
                {
                    currentIndex = value;
                }
                OnPropertyChanged("SelectedIndex");
            }
        }


        #endregion




        protected ICollectionView viewItems;
        public ICollectionView ViewItems
        {
            get { return viewItems; }
            set
            {
                viewItems = value;
                OnPropertyChanged("ViewItems");
            }
        }


        protected CRUDCardState crudState;
        public CRUDCardState CRUDState
        {
            get
            {
                return crudState;
            }
            set
            {
                crudState = value;
                OnPropertyChanged("CRUDState");
            }
        }

        protected CRUDType crudType;
        public CRUDType CRUDType
        {
            get
            {
                return crudType;
            }
            set
            {
                crudType = value;
                OnPropertyChanged("CRUDType");
            }
        }


        protected void ReFocusRow(int CollectionCount)
        {
            if (CollectionCount == 0)
                return;
            else if(currentIndex == CollectionCount)
                SelectedIndex = CollectionCount - 1;
            else SelectedIndex = currentIndex;
        }

        protected ICommand _CRUDCommand;
        public ICommand CRUDCommand
        {
            get
            {
                if (_CRUDCommand == null)
                {
                    _CRUDCommand = new RelayCommand(
                        p => HandledCommandAction((string)p));
                }
                return _CRUDCommand;
            }
        }

        protected void HandledCommandAction(object commandObject)
        {
            string commandName = "";
            var commandParameter = new object();
            bool haveCommandPara = false;

            if (commandObject is CommandParaObject)
            {
                var temp = commandObject as CommandParaObject;
                commandName = temp.CommandName;
                commandParameter = temp.CommandParameter;
                haveCommandPara = true;
            }
            else commandName = (string)commandObject;

            switch (commandName)
            {
                case "CreateNew":
                    if (haveCommandPara == false)
                        CreateNew();
                    else CreateNew(commandParameter);
                    break;
                case "CancelCreateNew":
                    if (haveCommandPara == false)
                        CancelCreateNew();
                    else CancelCreateNew(commandParameter);
                    break;
                case "Edit":
                    if (haveCommandPara == false)
                        Edit();
                    else Edit(commandParameter);
                    break;
                case "CancelEdit":
                    if (haveCommandPara == false)
                        CancelEdit();
                    else CancelEdit(commandParameter);
                    break;
                case "Save":
                    if (haveCommandPara == false)
                        Save();
                    else Save(commandParameter);
                    break;
                case "SaveAll":
                    SaveAll();
                    break;
                case "Delete":
                    if (haveCommandPara == false)
                        Delete();
                    else Delete(commandParameter);
                    break;
                case "DeleteAll":
                    DeleteAll();
                    break;
                case "Refresh":
                    Refresh();
                    break;
                case "Filter":
                    if (haveCommandPara == false)
                        FilterData();
                    else FilterData(commandParameter);
                    break;
                case "ClearFilter":
                    ClearFilter();
                    break;
                case "Add":
                    if (haveCommandPara == false)
                        Add();
                    else Add(commandParameter);
                    break;
                case "UpdateDetails":
                    if (haveCommandPara == false)
                        UpdateItemDetails();
                    else UpdateItemDetails(commandParameter);
                    break;
                case "Duplicate":
                    if (haveCommandPara == false)
                        Duplicate();
                    else Duplicate(commandParameter);
                    break;

            }
        }

        protected virtual void Refresh()
        {
            GetData();
        }

        protected virtual void GetData()
        {
            throw new NotImplementedException();
        }

        protected virtual void CreateNew()
        {
            throw new NotImplementedException();
        }

        protected virtual void CreateNew(object para)
        {
            throw new NotImplementedException();
        }

        protected virtual void CancelCreateNew()
        {
            throw new NotImplementedException();
        }

        protected virtual void CancelCreateNew(object para)
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateItemDetails()
        {
            InEditItem = SelectedItem;
        }

        public virtual void UpdateItemDetails(object para)
        {
            throw new NotImplementedException();
        }

        protected virtual void Add()
        {
            throw new NotImplementedException();
        }

        protected virtual void Add(object para)
        {
            throw new NotImplementedException();
        }

        protected virtual void Edit()
        {
            throw new NotImplementedException();
        }

        protected virtual void Edit(object para)
        {
            throw new NotImplementedException();
        }

        public virtual void CancelEdit()
        {
            throw new NotImplementedException();
        }

        protected virtual void CancelEdit(object para)
        {
            throw new NotImplementedException();
        }

        protected virtual void Save()
        {
            throw new NotImplementedException();
        }

        protected virtual void Save(object para)
        {
            throw new NotImplementedException();
        }

        protected virtual void SaveAll()
        {
            throw new NotImplementedException();
        }

        protected virtual void Duplicate()
        {
            throw new NotImplementedException();
        }

        protected virtual void Duplicate(object para)
        {
            throw new NotImplementedException();
        }

        protected virtual void FilterData()
        {
            throw new NotImplementedException();
        }

        protected virtual void FilterData(object para)
        {
            throw new NotImplementedException();
        }

        protected virtual void Delete()
        {
            throw new NotImplementedException();
        }

        protected virtual void Delete(object para)
        {
            throw new NotImplementedException();
        }

        protected virtual void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteMany(IList items)
        {
            throw new NotImplementedException();
        }

        protected void ClearFilter()
        {
            ViewItems.Filter = null;
            ViewItems.Refresh();
            IsFiltered = false;
        }


        
    }
}
