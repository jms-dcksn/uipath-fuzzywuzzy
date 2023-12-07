using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.ComponentModel.Design;
using UiPath.FuzzyWuzzy.Activities.Design.Designers;
using UiPath.FuzzyWuzzy.Activities.Design.Properties;

namespace UiPath.FuzzyWuzzy.Activities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.ValidateTable();

            var categoryAttribute = new CategoryAttribute($"{Resources.Category}");

            builder.AddCustomAttributes(typeof(FuzzyWuzzy), categoryAttribute);
            builder.AddCustomAttributes(typeof(FuzzyWuzzy), new DesignerAttribute(typeof(FuzzyWuzzyDesigner)));
            builder.AddCustomAttributes(typeof(FuzzyWuzzy), new HelpKeywordAttribute(""));


            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
