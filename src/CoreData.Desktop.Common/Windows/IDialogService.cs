namespace CoreData.Desktop.Common.Windows
{
    /// <summary>Windows interaction service.
    /// <para>Concreate Window resolved automatically based on convention: ContentPresenter with a type associated DataTemplates!!!</para>
    /// <para>Alt.: view registry(container registration) based on viewmodel parameter.</para></summary>
    public interface IDialogService
    {
        // 1.  IDialogService should open a window (or inject some control into the active window)
        // 2. Create a view corresponding to the name of the given VM type
        //              (use container registration or convention or a ContentPresenter with type associated DataTemplates)
        // ---
        // 1. IDialogService (ShowDialogAsync) opens a view based on specified dialog viewmodel
        //              (DialogRegistry knows which view how to provide(get or add )
        // 2. 
        // 1. DialogService (ShowDialogAsync) should be provided with a preconfigured dialog. It is caller who !creates and setups! dialog.
        //              Constructed input dialog has a Question(s) and optional. Optional Its based on question and provided variants of answer//TaskCompletionSource<TAnswer> and return its .Task property
        // 
        //TDialog ShowAsync<TDialog, TQuestion, TAnswer>(TDialog dialog)
        //    where TDialog : Dialog<TQuestion, TAnswer>;

        /// <summary>Shows Modal dialog based on TIn-TOut.</summary>
        /// <param name="dialog">is preconfigured by a call-site set of Question-Answer(s).</param>
        /// <returns>Task that represents dialog flow completion with an answer.</returns>

        DialogData<TQuestion> Ask<TQuestion, TOpts, TAnswer>(TQuestion question, TOpts variants);
            //where TDialog : Dialog<TQuestion>;
        //Task<TDialog> AskAsync<TDialog>(Func<TDialog> dialog)
        //    where TDialog : Dialog;

        //Task<TQuery> ShowAsync<TQuery>(TQuery dialog);
    }
}
