using System.ComponentModel;
using System.Drawing;
using System.Linq;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;

using Controls.Editors;

namespace Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(TextEdit))]
    [DefaultBindingProperty("Text")]
    public class PasswordEdit : ButtonEdit
    {
        [Browsable(true)]
        [Localizable(true)]
        [Description("The cue associated with the control."), Category("Appearance")]
        public string Cue
        {
            get => Properties.NullValuePrompt;
        }

        public override string EditorTypeName => RepositoryItemPasswordEdit.CustomName;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemPasswordEdit Properties => base.Properties as RepositoryItemPasswordEdit;

        static PasswordEdit() { RepositoryItemPasswordEdit.RegisterPasswordEdit(); }

        public PasswordEdit()
        { }

        protected override void OnClickButton(EditorButtonObjectInfoArgs buttonInfo)
        {
            base.OnClickButton(buttonInfo);

            if (buttonInfo.Button.Kind == ButtonPredefines.Ellipsis)
            {
                Properties.UseSystemPasswordChar = !Properties.UseSystemPasswordChar;
            }
        }

        protected override void OnEditValueChanged()
        {
            base.OnEditValueChanged();

            //Hide password
            if (string.IsNullOrWhiteSpace(Text.Trim()))
            {
                Properties.UseSystemPasswordChar = true;
            }

            //Eye
            var btn = Properties.Buttons.FirstOrDefault(x => x.Kind == ButtonPredefines.Ellipsis);
            if (btn != null)
            {
                btn.Visible = !string.IsNullOrWhiteSpace(Text.Trim());
            }

            //Notify password changed
            var args = new PasswordChangedEventArgs();
            Properties.OnPasswordChanged(args);
        }
    }
}
