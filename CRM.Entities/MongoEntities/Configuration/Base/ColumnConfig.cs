using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.MongoEntities.Configuration.Base {
    public class ColumnConfig {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Page name is required!")]
        public string PageName { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Column name is required!")]
        public string ColumnName { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Visible name is required!")]
        public string VisibleName { get; set; }

        public int Index { get; set; }
        public double Width { get; set; }
        public bool IsVisible { get; set; }

        public ColumnConfig() { }
        public ColumnConfig(string pageName, int index, double width, bool isVisible) {
            this.PageName = pageName;
            this.Index = index;
            this.Width = width;
            this.IsVisible = isVisible;
        }
    }
}