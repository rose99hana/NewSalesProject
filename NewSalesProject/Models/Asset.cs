using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Model
{
    public partial class Asset : ModelBase
    {
        public int Id { get; set; }
        [DisplayName("品名")]
        public string Name { get; set; }
        [DisplayName("資産管理番号")]
        public string ManagementCode { get; set; }
        [DisplayName("型番")]
        public string Code { get; set; }
        [DisplayName("取得年月日")]
        public DateTime? PurchasedDate { get; set; }
        [DisplayName("廃棄年月日")]
        public DateTime? DisposalDate { get; set; }
        [DisplayName("最終更新日")]
        public DateTime? LastUpdateDate { get; set; }
        [DisplayName("備考")]
        public string Note { get; set; }

        [Browsable(false)]
        public int? DepartmentID { get; set; }
        [Browsable(false)]
        public int? InstallationLocationID { get; set; }
        [Browsable(false)]
        public int? AssetCategoryID { get; set; }

        [ForeignKey("AssetCategoryID")]
        [Browsable(false)]
        public virtual AssetCategory AssetCategory { get; set; }
        [ForeignKey("DepartmentID")]
        [Browsable(false)]
        public virtual Department Department { get; set; }
        [ForeignKey("InstallationLocationID")]
        [Browsable(false)]
        public virtual InstallationLocation InstallationLocation { get; set; }

        protected override void CollectErrors(string propertyName)
        {
            Errors.Clear();
            switch (propertyName)
            {
                case "Name":
                    CheckNotNull(propertyName, Name);
                    break;
            }
        }
    }
}
