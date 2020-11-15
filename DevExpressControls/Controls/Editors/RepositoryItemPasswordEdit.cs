using System.ComponentModel;
using System.Drawing;

using DevExpress.Accessibility;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

namespace Controls.Editors
{
    [UserRepositoryItem("RegisterPasswordEdit")]
    public class RepositoryItemPasswordEdit : RepositoryItemButtonEdit
    {
        static readonly object onPasswordChanged = new object();

        [Description("Occurs when the password changes.")]
        public event PasswordChangedEventHandler PasswordChanged
        {
            add { this.Events.AddHandler(onPasswordChanged, value); }
            remove { this.Events.RemoveHandler(onPasswordChanged, value); }
        }
        protected internal virtual void OnPasswordChanged(PasswordChangedEventArgs e)
        {
            ((PasswordChangedEventHandler)Events[onPasswordChanged])?.Invoke(GetEventSender(), e);
        }

        public const string CustomName = "PasswordEdit";
        public override string EditorTypeName => CustomName;

        static RepositoryItemPasswordEdit() { RegisterPasswordEdit(); }

        public RepositoryItemPasswordEdit()
        {
            AllowNullInput = DefaultBoolean.True;
            NullValuePromptShowForEmptyValue = true;
            ShowNullValuePromptWhenFocused = true;
            UseSystemPasswordChar = true;
        }

        public static void RegisterPasswordEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomName,
              typeof(PasswordEdit), typeof(RepositoryItemPasswordEdit),
              typeof(ButtonEditViewInfo), new ButtonEditPainter(), true, null, typeof(ButtonEditAccessible)));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();

            try
            {
                base.Assign(item);

                var source = item as RepositoryItemPasswordEdit;
                if (source == null)
                    return;

                Events.AddHandler(onPasswordChanged, source.Events[onPasswordChanged]);
            }
            finally
            {
                EndUpdate();
            }
        }

        public override void CreateDefaultButton()
        {
            base.CreateDefaultButton();

            if (Buttons.Count == 1)
            {
                Buttons[0].Kind = ButtonPredefines.Ellipsis;
                Buttons[0].Visible = false;
            }
        }
    }

    #region Delegates

    public delegate void PasswordChangedEventHandler(object sender, PasswordChangedEventArgs e);

    #endregion
}
