using System;
using System.Windows.Forms;
public partial class MainForm : Form
{
    private MainMenu mainMenu;
    private MenuItem fileMenu;
    private MenuItem editMenu;
    private MenuItem viewMenu;
    private MenuItem helpMenu;

    private ToolBar toolBar;
    private ToolBarButton newButton;
    private ToolBarButton saveButton;
    private ToolBarButton loadButton;

    private StatusBar statusBar;
    private StatusBarPanel statusPanel;

    public MainForm()
    {
        // InitializeComponent();
        InitializeCustomComponents();
    }

    private void InitializeCustomComponents()
    {
        // MainMenu
        mainMenu = new MainMenu();
        fileMenu = new MenuItem("File");
        editMenu = new MenuItem("Edit");
        viewMenu = new MenuItem("View");
        helpMenu = new MenuItem("Help");

        mainMenu.MenuItems.Add(fileMenu);
        mainMenu.MenuItems.Add(editMenu);
        mainMenu.MenuItems.Add(viewMenu);
        mainMenu.MenuItems.Add(helpMenu);

        this.Menu = mainMenu;

        // ToolBar 
        toolBar = new ToolBar();
        newButton = new ToolBarButton("New");
        saveButton = new ToolBarButton("Save");
        loadButton = new ToolBarButton("Load");

        toolBar.Buttons.Add(newButton);
        toolBar.Buttons.Add(saveButton);
        toolBar.Buttons.Add(loadButton);

        this.Controls.Add(toolBar);

        // StatusBar
        statusBar = new StatusBar();
        statusPanel = new StatusBarPanel();
        statusBar.Panels.Add(statusPanel);
        statusBar.ShowPanels = true;

        this.Controls.Add(statusBar);
    }

    // Metode pentru manipularea evenimentelor
}
