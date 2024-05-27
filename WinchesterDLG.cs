// conectăm spații de nume
using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using WinchesterDE;

class WinchesterDLG : Form
{
    // declararea câmpurilor clasei
    private Label products_Label;
    private ListBox products_ListBox;
    private string products_Value;

    private Label name_Label;
    private TextBox name_TextBox;
    private ToolTip name_ToolTip;

    private Label yearModel_Label;
    private TextBox yearModel_TextBox;

    private Label telephoneCode_Label;
    private TextBox telephoneCode_TextBox;

    // creăm referința la obiecte DropDown
    private Label postCode_Label;
    private ComboBox postCode_ComboBox;

    // declarăm o referință la Spinnere
    private Label inthelistofCalibre_Label;
    private NumericUpDown Weight_NumericUpDown;

    // declarăm o referință la Spinnere
    public Label length_Label;
    private NumericUpDown length_NumericUpDown;

    // declarăm referințe la obiecte Glisor
    private TrackBar inthelistofCalibre_TrackBar;
    public Label weight_Level_Label;

    // declarăm butonul 
    private Button btnSave;
    private Button btnLoad;
    private Button btnCreate;
    private Button btnEdit;
    private Button btnDelete;
    private Label lblInfo;
    private Winchester currentWinchester;

    private MainMenu mainMenu;
    private MenuItem fileMenu;
    private MenuItem editMenu;
    private MenuItem viewMenu;
    private MenuItem helpMenu;

    public bool unsaved;
    public string filepath;

    //---------------------------------------------------------------------------------------------//

    // definirea constructorului implicit fară parametri
    public WinchesterDLG()
    {
        // setăm textul pe bara de titlu a ferestrei
        Text = "Winchester";
        // setăm poziţia şi dimensiunile iniţiale ale ferestrei
        StartPosition = FormStartPosition.Manual;
        Location = new System.Drawing.Point(100, 100);
        Size = new System.Drawing.Size(500, 600);
        filepath = "";
        //---------------------------------------------------------------------------------------------//

        //---------------------------------------------------------------------------------------------//
        // creăm obiectul eticheta Text
        products_Label = new Label();
        // etichetei Text îi atribuim numele "Label"
        products_Label.Name = "products_Label";
        // setăm textul etichetei Text
        products_Label.Text = "Products: ";
        // setăm dimensiunile și poziția textului în fereastră
        products_Label.Location = new System.Drawing.Point(16, 56);
        products_Label.Size = new System.Drawing.Size(100, 18);
        // setăm alinierea textului în fereastră
        products_Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
        // setăm denumirea, mărimea și stilul fontului
        products_Label.Font = new System.Drawing.Font("Arial", 10F, FontStyle.Italic | FontStyle.Bold);
        // setăm culoarea textului
        products_Label.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
        products_Label.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
        Controls.Add(products_Label);

        //---------//
        // creăm obiectul-lista
        products_ListBox = new ListBox();
        // golim lista
        products_ListBox.Items.Clear();
        // setăm poziția si dimensiunea ferestrei cu lista
        products_ListBox.IntegralHeight = false;
        products_ListBox.Location = new System.Drawing.Point(130, 56);
        products_ListBox.Size = new System.Drawing.Size(160, 60);
        // încărcăm lista cu elemente
        products_ListBox.Items.AddRange(new object[] {
            "Shotguns",
            "Rifles",
            "Limited Editions",
            "Ammunitions",
            "Clothes",
            "Accessories",
            "New Product"
        });
        // marcăm al doilea element ca fiind evidențiat
        products_ListBox.SetSelected(0, true);
        products_Value = "";
        // adăugăm denumirea Handler-ului evenimentului
        // ”S-a schimbat evidentierea elementelor”
        products_ListBox.SelectedIndexChanged += new
        EventHandler(ListBox_SelectedIndexChanged);
        // plasăm controlul în formă
        Controls.Add(products_ListBox);

        //---------------------------------------------------------------------------------------------//	
        //---------------------------------------------------------------------------------------------//	

        // creăm obiectul eticheta Text
        name_Label = new Label();
        // etichetei Text atribuim numele "Label"
        name_Label.Name = "name_Label";
        // setăm textul etichetei text
        name_Label.Text = "Name: ";
        // setăm dimensiunile și poziția textului în fereastră
        name_Label.Location = new System.Drawing.Point(16, 126);
        name_Label.Size = new System.Drawing.Size(100, 18);
        // setăm alinierea textului în fereastră
        name_Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
        // setăm denumirea, mărimea și stilul fontului
        name_Label.Font = new System.Drawing.Font("Arial", 10F, FontStyle.Italic | FontStyle.Bold);
        // setăm culoarea textului
        name_Label.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
        name_Label.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
        Controls.Add(name_Label);

        //---------//

        // creăm un obiect ”Etceta flotantă”
        name_ToolTip = new ToolTip();
        // stabilim reținerea la apariția Eticetei flotante
        name_ToolTip.AutomaticDelay = 300;
        // de a arăta Eticeta flotantă și în cazul când fereastra formei este inactivă
        name_ToolTip.ShowAlways = true;
        // definim Textul explicației flotante și o legăm cu butonul de comandă
        name_ToolTip.SetToolTip(name_Label, "Enter the name of the Winchester");

        //---------//	

        // creăm un obiect Caseta de editare
        name_TextBox = new TextBox();
        // definim textul inițial
        name_TextBox.Text = "DELUXE CASE HARDENED";
        // stabilim dimensiunea și poziția câmpului de introducere
        name_TextBox.Location = new System.Drawing.Point(130, 126);
        name_TextBox.Size = new System.Drawing.Size(160, 18);
        // stabilim modul multi-line
        name_TextBox.Multiline = false;
        // stabilim numărul maxim de caractere ce poate fi introdus în câmp
        name_TextBox.MaxLength = 1000;
        // adăugăm bare de derulare iar valorile posibile sunt: None, Horizontal, Vertical, Both
        name_TextBox.ScrollBars = ScrollBars.Both;
        // interzicem trecerea textului dintr-un rând în altul în cazul când el nu încape în lățimea casetei
        name_TextBox.WordWrap = false;
        // plasăm caseta de editare pe forma
        Controls.Add(name_TextBox);

        //---------------------------------------------------------------------------------------------//	

        // creăm obiectul cu eticheta text
        yearModel_Label = new Label();
        // etichetei text îi atribuim numele "Label"
        yearModel_Label.Name = "Year Of Model";
        // setăm textul etichetei text
        yearModel_Label.Text = "Year of Model: ";
        // setăm dimensiunile și poziția textului în fereastră
        yearModel_Label.Location = new System.Drawing.Point(16, 186);
        yearModel_Label.Size = new System.Drawing.Size(120, 18);
        // setăm alinierea textului în fereastă
        yearModel_Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
        // setăm denumirea, mărimea și stilul fontului
        yearModel_Label.Font = new System.Drawing.Font("Arial", 10F, FontStyle.Italic | FontStyle.Bold);
        // setăm culoarea textului
        yearModel_Label.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
        yearModel_Label.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
        Controls.Add(yearModel_Label);

        //---------//

        // creăm un obiect Caseta de editare
        yearModel_TextBox = new TextBox();
        // definim textul inițial
        yearModel_TextBox.Text = "1886";
        // stabilim dimensiunea și poziția câmpului de introducere
        yearModel_TextBox.Location = new System.Drawing.Point(146, 186);
        yearModel_TextBox.Size = new System.Drawing.Size(40, 18);
        // stabilim modul multi-line
        yearModel_TextBox.Multiline = false;
        // stabilim numărul maxim de caractere ce poate fi introdus în câmp
        yearModel_TextBox.MaxLength = 1000;
        // adăugăm bare de derulare, iar valorile posibile sunt: None, Horizontal, Vertical, Both
        yearModel_TextBox.ScrollBars = ScrollBars.Both;
        // interzicem trecerea textului dintr-un rând în altul în cazul când el nu încape în lățimea casetei
        yearModel_TextBox.WordWrap = false;
        // plasăm caseta de editare pe formă
        Controls.Add(yearModel_TextBox);

        //---------------------------------------------------------------------------------------------//	
        // creăm obiectul eticheta text
        telephoneCode_Label = new Label();
        // etichetei text îi atribuim numele "Label"
        telephoneCode_Label.Name = "Telephone Code";
        // setăm textul etichetei text
        telephoneCode_Label.Text = "Telephone Code: ";
        // setăm dimensiunile și pozitia textului în fereastră
        telephoneCode_Label.Location = new System.Drawing.Point(193, 186);
        telephoneCode_Label.Size = new System.Drawing.Size(125, 18);
        // setăm alinierea textului în fereastră
        telephoneCode_Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
        // setăm denumirea, mărimea și stilul fontului
        telephoneCode_Label.Font = new System.Drawing.Font("Arial", 10F, FontStyle.Italic | FontStyle.Bold);
        // setăm culoarea textului
        telephoneCode_Label.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
        telephoneCode_Label.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
        Controls.Add(telephoneCode_Label);

        //---------//

        // creăm un obiect Caseta de editare
        telephoneCode_TextBox = new TextBox();
        // definim textul inițial
        telephoneCode_TextBox.Text = "373";
        // stabilim dimensiunea și poziția câmpului de introducere
        telephoneCode_TextBox.Location = new System.Drawing.Point(326, 186);
        telephoneCode_TextBox.Size = new System.Drawing.Size(60, 18);
        // stabilim modul multi-line
        telephoneCode_TextBox.Multiline = false;
        // stabilim numărul maxim de caractere ce poate fi introdus în câmp
        telephoneCode_TextBox.MaxLength = 1000;
        // adăugăm bare de derulare, iar valorile posibile sunt: None, Horizontal, Vertical, Both
        telephoneCode_TextBox.ScrollBars = ScrollBars.Both;
        // interzicem trecerea textului dintr-un rând în altul în cazul când el nu încape în lățimea casetei
        telephoneCode_TextBox.WordWrap = false;
        // plasăm caseta de editare pe formă
        Controls.Add(telephoneCode_TextBox);

        //---------------------------------------------------------------------------------------------//	
        // creăm obiectul eticheta Text
        postCode_Label = new Label();
        // etichetei Text îi atribuim numele "Label"
        postCode_Label.Name = "postCode_Label";
        // setăm textul etichetei Text
        postCode_Label.Text = "Post Code: ";
        // setăm dimensiunile și poziția textului în fereastră
        postCode_Label.Location = new System.Drawing.Point(16, 156);
        postCode_Label.Size = new System.Drawing.Size(100, 18);
        // setăm alinierea textului în fereastră
        postCode_Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
        // setăm denumirea, mărimea și stilul fontului
        postCode_Label.Font = new System.Drawing.Font("Arial", 10F, FontStyle.Italic | FontStyle.Bold);
        // setăm culoarea textului
        postCode_Label.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
        postCode_Label.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
        Controls.Add(postCode_Label);

        //---------//

        // creăm un obiect
        postCode_ComboBox = new ComboBox();
        // golim lista DropDown
        postCode_ComboBox.Items.Clear();
        // setăm dimensiunea și poziția controlului DropDown
        postCode_ComboBox.Location = new System.Drawing.Point(130, 156);
        postCode_ComboBox.Size = new System.Drawing.Size(160, 20);
        // setăm stilul al listei DropDown
        postCode_ComboBox.DropDownStyle = ComboBoxStyle.DropDown;
        // populăm lista cu elemente
        postCode_ComboBox.Items.AddRange(new object[]
        {
            "2000",
            "2071",
            "2800",
            "4600",
            "4500",
            "5300",
            "8700",
            "8500",
            "9700",
            "1800",
            "105",
            "102",
            "103",
            "115",
            "320",
            "411",
            "416",
            "505",
            "504"
            });
        // marcăm primul element ca fiind selectat
        postCode_ComboBox.SelectedIndex = 0;

        // adăugăm lista DropDown în formă
        Controls.Add(postCode_ComboBox);

        //---------------------------------------------------------------------------------------------//	

        // creăm obiectul eticheta Text
        weight_Level_Label = new Label();
        // etichetei text îi atribuim numele "Label"
        weight_Level_Label.Name = "weight_Level_Label";
        // setăm textul etichetei Text
        weight_Level_Label.Text = "Weight (pounds)\n:";
        // setăm dimensiunile și poziția textului în fereastră
        weight_Level_Label.Location = new System.Drawing.Point(16, 306);
        weight_Level_Label.Size = new System.Drawing.Size(150, 22);
        // setăm alinierea textului în fereastră
        weight_Level_Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
        // setăm denumirea, mărimea și stilul fontului
        weight_Level_Label.Font = new System.Drawing.Font("Arial", 10F, FontStyle.Italic | FontStyle.Bold);
        // setăm culoarea textului
        weight_Level_Label.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
        weight_Level_Label.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
        Controls.Add(weight_Level_Label);

        //---------//

        // creăm un obiect Spinner
        Weight_NumericUpDown = new NumericUpDown();
        // setăm dimensiunea și poziția Spinner-ului
        Weight_NumericUpDown.Location = new System.Drawing.Point(180, 306);
        Weight_NumericUpDown.Size = new System.Drawing.Size(160, 20);
        // stabilim intervalul de derulare a valorilor
        Weight_NumericUpDown.Minimum = new Decimal(0.1);
        Weight_NumericUpDown.Maximum = new Decimal(5000);
        // setăm pasul derulării
        Weight_NumericUpDown.Increment = new Decimal(0.1);
        // setăm numărul de cifre după virgulă
        Weight_NumericUpDown.DecimalPlaces = 1;
        // setăm valoarea inițială
        Weight_NumericUpDown.Value = new Decimal(3.8);
        weight_Level_Label.Text = "Weight (pounds): ";
        // adăugăm Spinnerul în formă
        Controls.Add(Weight_NumericUpDown);

        //---------------------------------------------------------------------------------------------//

        // creăm obiectul cu eticheta Text
        length_Label = new Label();
        // etichetei text îi atribuim numele "Label"
        length_Label.Name = "length_Label";
        // setăm textul etichetei Text
        length_Label.Text = "Length (mm):";
        // setăm dimensiunile și poziția textului în fereastră
        length_Label.Location = new System.Drawing.Point(16, 216);
        length_Label.Size = new System.Drawing.Size(100, 18);
        // setăm alinierea textului în fereastră
        length_Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
        // setăm denumirea, mărimea și stilul fontului
        length_Label.Font = new System.Drawing.Font("Arial", 10F, FontStyle.Italic | FontStyle.Bold);
        // setăm culoarea textului
        length_Label.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
        length_Label.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
        Controls.Add(length_Label);

        //---------//

        // creăm un obiect Spinner
        length_NumericUpDown = new NumericUpDown();
        // setăm dimensiunea și poziția Spinner-ului
        length_NumericUpDown.Location = new System.Drawing.Point(130, 216);
        length_NumericUpDown.Size = new System.Drawing.Size(160, 20);
        // stabilim intervalul de derulare a valorilor
        length_NumericUpDown.Minimum = new Decimal(0.1);
        length_NumericUpDown.Maximum = new Decimal(5000);
        // setăm pasul derulării
        length_NumericUpDown.Increment = new Decimal(0.1);
        // setăm numărul de cifre după virgulă
        length_NumericUpDown.DecimalPlaces = 1;
        // setăm valoarea inițială
        length_NumericUpDown.Value = new Decimal(337.0);
        // adăugăm Spinnerul în formă
        Controls.Add(length_NumericUpDown);

        //---------------------------------------------------------------------------------------------//	
        //---------//
        // creăm obiectul eticheta Text
        inthelistofCalibre_Label = new Label();
        // etichetei text atribuim numele "Label"
        inthelistofCalibre_Label.Name = "inthelistofCalibre_Label";
        inthelistofCalibre_Label.Text = "In the list of Calibre: \n ";
        // setăm dimensiunile și poziția textului în fereastră
        inthelistofCalibre_Label.Location = new System.Drawing.Point(16, 246);
        inthelistofCalibre_Label.Size = new System.Drawing.Size(100, 48);
        // setăm alinierea textului în fereastră
        inthelistofCalibre_Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
        // setăm denumirea, mărimea și stilul fontului
        inthelistofCalibre_Label.Font = new System.Drawing.Font("Arial", 10F, FontStyle.Italic | FontStyle.Bold);
        // setăm culoarea textului
        inthelistofCalibre_Label.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
        inthelistofCalibre_Label.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
        Controls.Add(inthelistofCalibre_Label);

        //---------//

        // creăm un obiect Glisor
        inthelistofCalibre_TrackBar = new TrackBar();
        // setăm intervalul de valori și valoarea inițială
        inthelistofCalibre_TrackBar.Minimum = 1;
        inthelistofCalibre_TrackBar.Maximum = 270;
        inthelistofCalibre_TrackBar.Value = 1;
        // setăm textul inițial
        // setăm dimensiunea și poziția indicatorului
        inthelistofCalibre_TrackBar.Location = new System.Drawing.Point(130, 246);
        inthelistofCalibre_TrackBar.Size = new System.Drawing.Size(160, 20);
        // setăm orientarea orizontală pentru Scala
        inthelistofCalibre_TrackBar.Orientation = Orientation.Horizontal;
        // setăm modul de plasare a liniilor de divizare
        inthelistofCalibre_TrackBar.TickStyle = TickStyle.TopLeft;
        // setăm numărul de poziții între divizările vecine a scalei
        inthelistofCalibre_TrackBar.TickFrequency = 0;
        // adăugăm tratarea evenimentului de deplasare a indicatorului
        // adăugăm controlul la forma
        Controls.Add(inthelistofCalibre_TrackBar);

        //---------------------------------------------------------------------------------------------//	
        int top = 360;
        btnSave = new Button { Text = "Save", Top = top, Left = 10 };
        btnLoad = new Button { Text = "Load", Top = top, Left = 100 };
        btnCreate = new Button { Text = "Create", Top = top, Left = 190 };
        btnEdit = new Button { Text = "Edit", Top = top, Left = 280 };
        btnDelete = new Button { Text = "Delete", Top = top, Left = 370 };
        lblInfo = new Label { Text = "Info:", Top = top + 50, Left = 10, Width = 460, Height = 100 };

        btnSave.Click += (sender, e) => SaveWinchester();
        btnLoad.Click += (sender, e) => LoadWinchester();
        btnCreate.Click += (sender, e) => CreateWinchester();
        btnEdit.Click += (sender, e) => EditWinchester();
        btnDelete.Click += (sender, e) => DeleteWinchester();

        Controls.Add(btnSave);
        Controls.Add(btnLoad);
        Controls.Add(btnCreate);
        Controls.Add(btnEdit);
        Controls.Add(btnDelete);
        Controls.Add(lblInfo);
        //---------------------------------------------------------------------------------------------//	
        // setăm legătura între evenimentul ”Închiderea ferestrei” și handler-ul de evenimente.
    }

    // HANDLER-E!!!!!
    //*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*\\
    // pentru a urmări schimbările din formă 
    private void saveStatus_Changed(object sender, System.EventArgs e)
    { unsaved = true; }

    // HANDLER-E!!!!!
    //*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*\\
    // Handler-ul evenimentului ”S-a schimbat evidențierea elementelor”
    private void ListBox_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        string str;
        str = "";
        foreach (int idx in products_ListBox.SelectedIndices)
        {
            str += products_ListBox.Items[idx] + "";
        }
        products_Value = str;
        unsaved = true;
    }
    //*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*\\



    //*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*\\


    //*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*\\

    private void SaveWinchester()
    {
        if (currentWinchester != null)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "Binary files (*.bin)|*.bin" };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentWinchester.setProducts(products_Value);
                    currentWinchester.setName(name_TextBox.Text);
                    currentWinchester.setPostCode(int.Parse(postCode_ComboBox.Text));
                    currentWinchester.setTelephoneCode(Int32.Parse(telephoneCode_TextBox.Text));
                    currentWinchester.setInthelistofCalibre((int)inthelistofCalibre_TrackBar.Value);
                    currentWinchester.setYearModel(int.Parse(yearModel_TextBox.Text));
                    currentWinchester.setLength(Convert.ToDouble(length_NumericUpDown.Value));
                    currentWinchester.setWeight(Convert.ToDouble(Weight_NumericUpDown.Value));
                    // Assuming you want to save only the year
                    // Save the other properties accordingly
                    currentWinchester.SaveToFile(saveFileDialog.FileName);
                    MessageBox.Show("Winchester saved successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving Winchester:" + ex.Message);
            }
        }
        else
        {
            MessageBox.Show("No Winchester object to save.");
        }
    }

    private void LoadWinchester()
    {
        try
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Binary files (*.bin)|*.bin" };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Assuming you have a static LoadFromFile method in the Winchester class
                currentWinchester = Winchester.LoadFromFile(openFileDialog.FileName);
                DisplayWinchesterInfo();
                MessageBox.Show("Winchester loaded successfully.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading Winchester: " + ex.Message);
        }
    }

    private void CreateWinchester()
    {
        // Assuming you have added necessary parameters to the Winchester constructor
        currentWinchester = new Winchester(
            products_Value,
            name_TextBox.Text,
            int.Parse(yearModel_TextBox.Text),
            int.Parse(postCode_ComboBox.Text),
            int.Parse(telephoneCode_TextBox.Text),
            (int)inthelistofCalibre_TrackBar.Value,
            Convert.ToDouble(length_NumericUpDown.Value),
            Convert.ToDouble(Weight_NumericUpDown.Value)
            );
        DisplayWinchesterInfo();
        MessageBox.Show("Winchester created successfully.");
    }

    private void EditWinchester()
    {
        if (currentWinchester != null)
        {
            // Assuming you have added necessary properties to the Winchester class
            currentWinchester.setProducts(products_Value);
            currentWinchester.setName(name_TextBox.Text);

            currentWinchester.setPostCode(int.Parse(postCode_ComboBox.Text));
            currentWinchester.setTelephoneCode(Int32.Parse(telephoneCode_TextBox.Text));
            currentWinchester.setInthelistofCalibre((int)inthelistofCalibre_TrackBar.Value);
            // Assuming ManufactureDate is a property in the Winchester class
            currentWinchester.setYearModel(Int32.Parse(yearModel_TextBox.Text));
            currentWinchester.setLength(Decimal.ToDouble(length_NumericUpDown.Value));
            currentWinchester.setWeight(Decimal.ToDouble(Weight_NumericUpDown.Value));
            // Assuming you want to save only the year
            DisplayWinchesterInfo();
            MessageBox.Show("Winchester updated successfully.");
        }
        else
        {
            MessageBox.Show("No Winchester object to edit.");
        }
    }

    private void DeleteWinchester()
    {
        if (currentWinchester != null)
        {
            currentWinchester = null;
            lblInfo.Text = "Info:";
            MessageBox.Show("Winchester deleted successfully.");
        }
        else
        {
            MessageBox.Show("No Winchester object to delete.");
        }
    }

    private void DisplayWinchesterInfo()
    {
        if (currentWinchester != null)
        {
            // Assuming you have added necessary properties to the Winchester class
            lblInfo.Text = "Product: " + currentWinchester.getProducts() + "\n" +
                           "Name: " + currentWinchester.getName() + "\n" +
                           "Post Code: " + currentWinchester.getPostCode() + "\n" +
                           "Telephone Code: " + currentWinchester.getTelephoneCode() + "\n" +
                           "Lenght: " + currentWinchester.getLength() + " mm" + "\n" +
                           "Calibre:" + currentWinchester.getInthelistofCalibre() + "\n" +
                           "Weight (pounds):" + currentWinchester.getWeight() + "\n" +
                           "Manufacture Year: " + currentWinchester.getYearModel(); // Assuming you want to display only the year
        }
        else
        {
            lblInfo.Text = "Info:";
        }
    }


    //*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*---*\\


}
