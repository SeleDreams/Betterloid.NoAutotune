using Yamaha.VOCALOID;
using Yamaha.VOCALOID.MusicalEditor;
using Yamaha.VOCALOID.VSM;
using VOCALOID = Yamaha.VOCALOID;
using Betterloid;

namespace NoAutotuneCommand
{
    public class NoAutotuneCommand : IPlugin
    {
        public void Startup()
        {
            MainWindow window = App.Current.MainWindow as MainWindow;
            var xMusicalEditorDiv = window.FindName("xMusicalEditorDiv") as MusicalEditorDivision;
            var MusicalEditor = xMusicalEditorDiv.DataContext as MusicalEditorViewModel;
            var activePart = MusicalEditor.ActivePart;
            if (activePart == null)
            {
                return;
            }
            var notes = activePart.SelectedNotes;
            if (notes == null || notes.Count == 0)
            {
                return;
            }
            using (Transaction transaction = new Transaction(activePart.Sequence))
            {
                foreach (var note in notes)
                {
                    VSMAiNoteExpression expression = new VSMAiNoteExpression(0.5f, 0, 0, 0.5f, 0, 0, 0, 0.5f, 0.5f, 0.5f, 0, 0);
                    note.SetAiNoteExpression(expression);
                }
            }
        }
    }
}
