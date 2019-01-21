using CoreData.Common.ModelNotifyChanged;
using System.Threading.Tasks;

namespace CoreData.Desktop.Common.Windows
{
    public static class DialogData
    {
        public static Task<TAnswer> Completion<TQuestion, TAnswer>(this DialogData<TQuestion> ctx) =>
            (Task<TAnswer>)ctx.Completion;
    }

    public abstract class DialogData<TQuestion> : ViewModel
    {
        /// <summary>Task representing dialog completion (answer).</summary>
        public abstract Task Completion { get; }
    }
    /// <summary>Dialog view model: input question---dialog task flow---output answer result.</summary>
    /// <example>await ShowDialogAsync(dialog)</example>
    public class DialogData<TQuestion, TOpts, TAnswer> : DialogData<TOpts>
    //public class Dialog : ViewModel
    {
        //public override string LogView => $"{base.LogView}";

        private readonly TaskCompletionSource<TAnswer> _taskCompletionSource;

        public DialogData(TQuestion question, TOpts options)
        {
            _taskCompletionSource = new TaskCompletionSource<TAnswer>();
            Question = question;
            Opts = options;
        }

        public TQuestion Question { get; }
        public TOpts Opts { get; }

        /// <summary>Completes dialog flow.(---with a specified answer)</summary>
        public Command Complete { get; } // Command<TAnswer>

        public override Task Completion => _taskCompletionSource.Task;
        //public override TaskAwaiter GetAwaiter() => Completion.GetAwaiter();
    }
}
