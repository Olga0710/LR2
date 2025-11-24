using Microsoft.Maui.Controls;
using Archive.Services;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace Archive
{
    public partial class MainPage : ContentPage
    {
        private IXmlSearchStrategy _strategy;
        private string _xmlPath = "";
        private string _xslPath = "";

        Picker strategyPicker;
        Picker attributePicker;
        Entry keywordEntry;
        Button loadXmlButton;
        Button loadXslButton;
        Button searchButton;
        Button transformButton;
        Button clearButton;
        Button exitButton;
        Editor outputEditor;

        public MainPage()
        {
         
            BuildUI();
        }

        private void BuildUI()
        {
            strategyPicker = new Picker { Title = "Метод аналізу" };
            strategyPicker.Items.Add("DOM");
            strategyPicker.Items.Add("SAX");
            strategyPicker.Items.Add("LINQ");
            strategyPicker.SelectedIndexChanged += StrategyPickerChanged;

            loadXmlButton = new Button { Text = "Обрати XML" };
            loadXmlButton.Clicked += LoadXmlClicked;

            attributePicker = new Picker { Title = "Оберіть атрибут" };

            keywordEntry = new Entry { Placeholder = "Ключове слово..." };

            searchButton = new Button { Text = "Пошук" };
            searchButton.Clicked += SearchClicked;

            loadXslButton = new Button { Text = "Обрати XSL" };
            loadXslButton.Clicked += LoadXslClicked;

            transformButton = new Button { Text = "Трансформувати XML → HTML" };
            transformButton.Clicked += TransformClicked;

            clearButton = new Button { Text = "Clear" };
            clearButton.Clicked += (s, e) =>
            {
                keywordEntry.Text = "";
                outputEditor.Text = "";
            };

            exitButton = new Button { Text = "Вихід" };
            exitButton.Clicked += ExitClicked;

            outputEditor = new Editor { AutoSize = EditorAutoSizeOption.TextChanges, HeightRequest = 300 };

            Content = new ScrollView
            {
                Content = new VerticalStackLayout
                {
                    Padding = 15,
                    Children =
                    {
                        strategyPicker,
                        loadXmlButton,
                        attributePicker,
                        keywordEntry,
                        searchButton,
                        loadXslButton,
                        transformButton,
                        clearButton,
                        exitButton,
                        outputEditor
                    }
                }
            };
        }

        private void StrategyPickerChanged(object sender, EventArgs e)
        {
            switch (strategyPicker.SelectedIndex)
            {
                case 0: _strategy = new DomStrategy(); break;
                case 1: _strategy = new SaxStrategy(); break;
                case 2: _strategy = new LinqStrategy(); break;
            }
        }

        static readonly FilePickerFileType XmlFileType = new(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.WinUI, new[] { ".xml" } },
        });

        static readonly FilePickerFileType XslFileType = new(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.WinUI, new[] { ".xslt" } },
        });

        private async void LoadXmlClicked(object sender, EventArgs e)
        {
            var file = await FilePicker.Default.PickAsync(new()
            {
                FileTypes = XmlFileType

            });

            if (file == null) return;

            _xmlPath = file.FullPath;
            outputEditor.Text = $"XML завантажено:\n{_xmlPath}";

            FillAttributes();
        }

        private void FillAttributes()
        {
            attributePicker.Items.Clear();
            var doc = XDocument.Load(_xmlPath);

            var first = doc.Root.Elements().FirstOrDefault();
            if (first == null) return;

            foreach (var attr in first.Attributes())
                attributePicker.Items.Add(attr.Name.LocalName);
        }

        private async void LoadXslClicked(object sender, EventArgs e)
        {
            var file = await FilePicker.Default.PickAsync(new()
            {
                FileTypes = XslFileType

            });

            if (file == null) return;

            _xslPath = file.FullPath;
            outputEditor.Text = $"XSL завантажено:\n{_xslPath}";
        }

        private void SearchClicked(object sender, EventArgs e)
        {
            if (_xmlPath == "") { outputEditor.Text = "Спочатку завантаж XML."; return; }
            if (attributePicker.SelectedIndex < 0) { outputEditor.Text = "Оберіть атрибут."; return; }
            if (string.IsNullOrWhiteSpace(keywordEntry.Text)) { outputEditor.Text = "Введіть ключове слово."; return; }
            string attribute = attributePicker.Items[attributePicker.SelectedIndex];

            var results = _strategy.Search(keywordEntry.Text, attribute, _xmlPath);

            outputEditor.Text = string.Join("\n", results);
        }

        private void TransformClicked(object sender, EventArgs e)
        {
            if (_xmlPath == "" || _xslPath == "")
            {
                outputEditor.Text = "Завантаж XML і XSL.";
                return;
            }

            string outHtml = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"result.html");

            XslTransformer.Transform(_xmlPath, _xslPath, outHtml);

            outputEditor.Text = "HTML згенеровано:\n" + outHtml;
        }

        private async void ExitClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Вихід", "Дійсно вийти?", "Так", "Ні");
            if (!answer) return;
            #if WINDOWS
            Application.Current?.Quit();
            #endif
        }

    }
}
