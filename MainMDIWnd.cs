using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization; 
using System.Runtime.Serialization.Formatters.Binary; 

using WinchesterDE;
class MainMDIWnd : Form {
    private MainMenu m_MainMenu;

    private MenuItem m_MenuFile;
    private MenuItem m_MenuFileNew;
    private MenuItem m_MenuFileOpen;
    private MenuItem m_MenuFileClose;
    private MenuItem m_MenuFileExit;

    private MenuItem m_MenuView;  
    private MenuItem m_MenuViewToolBar;  
    private MenuItem m_MenuViewStatusBar; 

    private MenuItem m_MenuWindow;
    private MenuItem m_MenuWindowCascade;
    private MenuItem m_MenuWindowHorizontally;
    private MenuItem m_MenuWindowVertically;
	
	private MenuItem m_MenuHelp;  
    private MenuItem m_MenuHelpAbout; 
	
	// pentru a crea o bară de stare putem utiliza clasele StatusBar, StatusBarPanel sau clasele analogice StatusStrip, ToolStripStatusLabel, ToolStripDropDownButton, ToolStripSplitButton, ToolStripProgressBar, doar cu o funcţionalitate mai bogată.
    private StatusBar m_StatusBar;  
    public StatusBarPanel m_StatusBarTextPanel; 
    public StatusBarPanel m_StatusBarTimePanel; 
    public StatusBarPanel m_StatusBarMousePanel; 

    // declarăm o referinţe spre obiecte Timer  
    private Timer m_Timer;
	
	// creăm lista imaginilor
    private ImageList m_TBImageList;
    // mai departe creăm butoanele barei de instrumente
    private ToolBarButton m_NewTBButton;  
    private ToolBarButton m_OpenTBButton;  
    private ToolBarButton m_SaveTBButton;  
    private ToolBarButton m_Sep1TBButton;
    private ToolBarButton m_AboutTBButton; 

    // creăm însăşi caseta cu instrumente: 
    private ToolBar m_MainToolBar; 

    public MainMDIWnd() {
        // setăm textul în antetul ferestrei
        Text = "Multiple Document Interface Application";
        // setăm poziția inițială și dimensiunea ferestrei implicite
        StartPosition = FormStartPosition.WindowsDefaultBounds;
        WindowState = FormWindowState.Maximized;
        IsMdiContainer = true;

        m_MainMenu = new MainMenu();

        m_MenuFile = new MenuItem("&File");

        m_MenuFileNew = new MenuItem("&New",
        new EventHandler(MenuFileNew_OnClick));
        m_MenuFileNew.Shortcut = Shortcut.CtrlN;
        // m_MenuFileNew.ShowShortcut = false;

        m_MenuFileOpen = new MenuItem("&Open...");
        m_MenuFileOpen.Click +=
        new EventHandler(MenuFileOpen_OnClick);
        m_MenuFileOpen.Shortcut = Shortcut.CtrlO;
        m_MenuFileOpen.ShowShortcut = true;

        m_MenuFileClose = new MenuItem("&Close",
        new EventHandler(MenuFileClose_OnClick));

        m_MenuFileExit = new MenuItem("&Exit",
        new EventHandler(MenuFileExit_OnClick));

        m_MenuFile.MenuItems.Add(m_MenuFileNew);
        m_MenuFile.MenuItems.Add("-");

        m_MenuFile.MenuItems.Add(m_MenuFileOpen);
        m_MenuFile.MenuItems.Add("-");

        m_MenuFile.MenuItems.Add(m_MenuFileClose);

        m_MenuFile.MenuItems.Add("-");
        m_MenuFile.MenuItems.Add(m_MenuFileExit);

        m_MenuView = new MenuItem("&View"); 
        
        m_MenuViewToolBar = new MenuItem("&ToolBar", new EventHandler(MenuViewToolBar_OnClick)); 
        
        m_MenuViewStatusBar = new MenuItem("&StatusBar", new EventHandler(MenuViewStatusBar_OnClick)); 
        
        m_MenuView.MenuItems.Add(m_MenuViewToolBar); 
        m_MenuView.MenuItems.Add(m_MenuViewStatusBar); 

        m_MenuHelp = new MenuItem("&Help"); 
        m_MenuHelpAbout = new MenuItem("&About", new EventHandler(MenuHelpAbout_OnClick));
		m_MenuHelp.MenuItems.Add(m_MenuHelpAbout); 

        m_MenuWindow = new MenuItem("&Window");
        m_MenuWindow.MdiList = true;
        m_MenuWindowCascade = new MenuItem("&Cascade", new EventHandler(MenuWindowCascade_OnClick));
        m_MenuWindowHorizontally = new MenuItem("&Horizontally", new EventHandler(MenuWindowHorizontally_OnClick));
        m_MenuWindowVertically = new MenuItem("&Vertically", new EventHandler(MenuWindowVertically_OnClick));
        m_MenuWindow.MenuItems.Add(m_MenuWindowCascade);
        m_MenuWindow.MenuItems.Add(m_MenuWindowHorizontally);
        m_MenuWindow.MenuItems.Add(m_MenuWindowVertically);

        m_MainMenu.MenuItems.Add(m_MenuFile);
		m_MainMenu.MenuItems.Add(m_MenuView);   
        m_MainMenu.MenuItems.Add(m_MenuWindow);
		m_MainMenu.MenuItems.Add(m_MenuHelp); 

        Menu = m_MainMenu;

        //-------------------------------------------------------------------------------------------------//
        m_StatusBar = new StatusBar(); 
        
        m_StatusBarTextPanel = new StatusBarPanel();  
        m_StatusBarTextPanel.BorderStyle = StatusBarPanelBorderStyle.Sunken;   
        m_StatusBarTextPanel.Text = "";   
        m_StatusBarTextPanel.AutoSize = StatusBarPanelAutoSize.Spring; 
        
        m_StatusBarTimePanel = new StatusBarPanel();  
        m_StatusBarTimePanel.BorderStyle = StatusBarPanelBorderStyle.Sunken;   
        m_StatusBarTimePanel.AutoSize = StatusBarPanelAutoSize.Contents;  
        m_StatusBarTimePanel.Text = ""; 
        
        m_StatusBarMousePanel = new StatusBarPanel();   
        m_StatusBarMousePanel.BorderStyle = StatusBarPanelBorderStyle.Sunken;  
        m_StatusBarMousePanel.AutoSize = StatusBarPanelAutoSize.Contents; 
        m_StatusBarMousePanel.Text = ""; 
        
        m_StatusBar.ShowPanels = true;  
        m_StatusBar.Panels.Add(m_StatusBarTextPanel); 
        m_StatusBar.Panels.Add(m_StatusBarMousePanel); 
        m_StatusBar.Panels.Add(m_StatusBarTimePanel); 
        
        m_StatusBar.Dock=DockStyle.Bottom; 
        
        Controls.Add(m_StatusBar); 
        //-------------------------------------------------------------------------------------------------//
        // creăm un obiect Timer   
        m_Timer=new Timer();   
        // deblocăm Timer-ul   
        m_Timer.Enabled=true;   
        // stabilim intervalul de timp pentru generarea evenimentelor   
        m_Timer.Interval=100;   
        m_Timer.Start();   
        m_Timer.Tick+=new EventHandler(Timer_Tick); 
        
        // încărcarea imaginilor
        m_TBImageList = new ImageList();  
        m_TBImageList.ColorDepth=ColorDepth.Depth24Bit;   
        m_TBImageList.ImageSize=new System.Drawing.Size(24,24); 
        m_TBImageList.Images.Add(Image.FromFile(  Application.StartupPath+"\\Images\\new.png")); 
        m_TBImageList.Images.Add(Image.FromFile( Application.StartupPath+"\\Images\\open.png")); 
        m_TBImageList.Images.Add(Image.FromFile( Application.StartupPath+"\\Images\\save.png")); 
        m_TBImageList.Images.Add(Image.FromFile( Application.StartupPath+"\\Images\\about.png")); 
         
        // mai departe creăm butoanele barei de instrumente: 
        m_NewTBButton = new ToolBarButton();   
        m_NewTBButton.ToolTipText = "New";   
        m_NewTBButton.Style = ToolBarButtonStyle.PushButton;   
        // PushButton, Separator, ToggleButton, DropDownButton   
        m_NewTBButton.ImageIndex=0; 
        
        m_OpenTBButton = new ToolBarButton();  
        m_OpenTBButton.ToolTipText = "Open";   
        m_OpenTBButton.Style = ToolBarButtonStyle.PushButton;   
        m_OpenTBButton.ImageIndex=1; 
        
        m_SaveTBButton = new ToolBarButton();   
        m_SaveTBButton.ToolTipText = "Save";   
        m_SaveTBButton.Style = ToolBarButtonStyle.PushButton; 
        m_SaveTBButton.ImageIndex=2; 
        
        m_Sep1TBButton = new ToolBarButton();  
        m_Sep1TBButton.Style=ToolBarButtonStyle.Separator;

        m_AboutTBButton = new ToolBarButton(); 
        m_AboutTBButton.ToolTipText = "About";  
        m_AboutTBButton.Style = ToolBarButtonStyle.PushButton; 
        m_AboutTBButton.ImageIndex=3; 
        
        // creăm înseşi caseta cu instrumente: 
        m_MainToolBar = new ToolBar(); 
        m_MainToolBar.Appearance = ToolBarAppearance.Flat;                           
        // ToolBarAppearance.Normal   
        m_MainToolBar.BorderStyle = BorderStyle.None;  
        m_MainToolBar.ShowToolTips = true; 
        m_MainToolBar.ImageList=m_TBImageList; 
        
        m_MainToolBar.Buttons.Add(m_NewTBButton); 
        m_MainToolBar.Buttons.Add(m_OpenTBButton);  
        m_MainToolBar.Buttons.Add(m_SaveTBButton);  
        m_MainToolBar.Buttons.Add(m_Sep1TBButton); 
        m_MainToolBar.Buttons.Add(m_AboutTBButton); 
        
        m_MainToolBar.Dock=DockStyle.Top; 
        
        Controls.Add(m_MainToolBar); 
        m_MainToolBar.ButtonClick+= new ToolBarButtonClickEventHandler(ToolBar_ButtonClick);
    }

    private void MenuFileNew_OnClick(Object sender, EventArgs e) {
        WinchesterDLG winchesterdlg = new WinchesterDLG();
        winchesterdlg.MdiParent = this;
        winchesterdlg.Text ="Untitled";
        winchesterdlg.Show();
		m_StatusBarTextPanel.Text="[INFO] New form created"; 
    }

    private void MenuFileOpen_OnClick(Object sender, EventArgs e) {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        // în cazul când valoarea este True, pentru obținerea denumirilor ale fișierelor trebuie să utilizăm proprietatea FileNames
        openFileDialog.Multiselect = false;
        openFileDialog.Filter = "All files (*.*)|*.*";
        openFileDialog.FilterIndex = 1;
        if (openFileDialog.ShowDialog(this) == DialogResult.OK) {
            // creare fereastră
            WinchesterDLG winchesterdlg = new WinchesterDLG();
            winchesterdlg.MdiParent = this;
            // deserealizare
            IFormatter formatter = new BinaryFormatter();

            Winchester input_object =  new Winchester();
			Stream input_stream = new FileStream(openFileDialog.FileName,
											   FileMode.Open,         
											   FileAccess.Read,                                       
											   FileShare.Read); 
			input_object = (Winchester)formatter.Deserialize(input_stream);  
		
			winchesterdlg.fillForm(input_object);
			winchesterdlg.Text=openFileDialog.FileName;
			winchesterdlg.unsaved = false;
			winchesterdlg.filepath = openFileDialog.FileName;
            winchesterdlg.Show();
			input_stream.Close();
            m_StatusBarTextPanel.Text="[INFO] "+openFileDialog.FileName+" Opened"; 
        }  
        openFileDialog.Dispose(); 
    }
	
	private void MenuFileSave_OnClick(Object sender, EventArgs e) {
        if (ActiveMdiChild != null) {
            Winchester ob = ((WinchesterDLG)ActiveMdiChild).CreateWinchester();
            if (((WinchesterDLG)ActiveMdiChild).Text == "Untitled") {
                MenuFileSaveAs_OnClick(sender, e);
            } else {
                IFormatter formatter = new BinaryFormatter();
                Stream output_stream = new FileStream(((WinchesterDLG)ActiveMdiChild).Text,
                                                        FileMode.Create,
                                                        FileAccess.Write,
                                                        FileShare.None);
                    formatter.Serialize(output_stream, ob);
                    ((WinchesterDLG)ActiveMdiChild).unsaved=false;
                    ((WinchesterDLG)ActiveMdiChild).filepath=((WinchesterDLG)ActiveMdiChild).Text;
                    output_stream.Close();
                    m_StatusBarTextPanel.Text="[SUCCESS] "+((WinchesterDLG)ActiveMdiChild).Text+" File Saved";
            }
        }
    }
	 
	private void MenuFileSaveAs_OnClick(object sender, EventArgs e) {
        Winchester ob = ((WinchesterDLG)ActiveMdiChild).CreateWinchester();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        // în cazul când valoarea este true, pentru obținerea denumirilor ale fișierelor trebuie să utilizăm proprietatea FileNames    
        saveFileDialog.Filter = "All files (*.*)|*.*";
        saveFileDialog.FilterIndex = 1;
        if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {
           IFormatter formatter = new BinaryFormatter(); 
		   Stream output_stream = new FileStream(saveFileDialog.FileName+".bin", 
													 FileMode.Create,                                         
													 FileAccess.Write,                                         
													 FileShare.None);
			formatter.Serialize(output_stream, ob); 
			output_stream.Close(); 
			((WinchesterDLG)ActiveMdiChild).Text = saveFileDialog.FileName+".bin"; 
			m_StatusBarTextPanel.Text="[SUCCESS] "+((WinchesterDLG)ActiveMdiChild).Text+" File Saved";
        }
        saveFileDialog.Dispose();
        ((WinchesterDLG)ActiveMdiChild).unsaved = false;
    }  

    private void MenuFileClose_OnClick(Object sender, EventArgs e) {
        if (ActiveMdiChild != null) { ((WinchesterDLG)ActiveMdiChild).Close(); }
    }

    private void MenuFileExit_OnClick(Object sender, EventArgs e) {
        // metoda Exit a clasei Application informează pe toate cozile de mesaje că trebuie să se oprească și apoi, după ce toate mesajele vor fi prelucrate, închide pe toate ferestre ale aplicației
		bool exitFlag=true;
		do {
			if (ActiveMdiChild != null) { ((WinchesterDLG)ActiveMdiChild).Close(); }
            else exitFlag = false;
		} while(exitFlag);
		Application.Exit();
    }
	
	private void MenuViewToolBar_OnClick(object sender, EventArgs e) {
        if (m_MainToolBar.Visible == false) {
            m_MainToolBar.Visible = true;
            m_StatusBarTextPanel.Text="[INFO] ToolBar Displayed";
        }
        else {
            m_MainToolBar.Visible = false;
            m_StatusBarTextPanel.Text="[INFO] ToolBar Hiden";
        }
    }
    
    private void MenuViewStatusBar_OnClick(object sender, EventArgs e) {
        if (m_StatusBar.Visible == false) {
            m_StatusBar.Visible = true;
			m_StatusBarTextPanel.Text="[INFO] StatusBar Restored"; 
		}
        else m_StatusBar.Visible = false;
     } 
	// Status Bar Timer 
    private void Timer_Tick(object sender, System.EventArgs e) {   
		DateTime dCurrentDate = DateTime.Now; 
		m_StatusBarTimePanel.Text=  
				(dCurrentDate.Hour  <10 ? "0" : "")+dCurrentDate.Hour  +":"+
				(dCurrentDate.Minute<10 ? "0" : "")+dCurrentDate.Minute+":"+
				(dCurrentDate.Second<10 ? "0" : "")+dCurrentDate.Second;  
    } 
	 
	// Handler-ul pentru evenimentul de apăsare a butonului de pe caseta cu instrumente: 
    private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e) {  
		if (e.Button==m_NewTBButton)    
			MenuFileNew_OnClick(null,null);  
		if (e.Button==m_OpenTBButton)   
			MenuFileOpen_OnClick(null,null);  
		if (e.Button==m_SaveTBButton)   
			MenuFileSave_OnClick(null,null);  
		if (e.Button==m_AboutTBButton)   
			MenuHelpAbout_OnClick(null,null); 
    } 

    private void MenuWindowCascade_OnClick(Object sender, EventArgs e) { 
        LayoutMdi(MdiLayout.Cascade);
        m_StatusBarTextPanel.Text="[INFO] Cascade Layout applied";
	}

    private void MenuWindowHorizontally_OnClick(Object sender, EventArgs e) { 
		LayoutMdi(MdiLayout.TileHorizontal);
		m_StatusBarTextPanel.Text="[INFO] Horizontal Layout applied"; 
	}

    private void MenuWindowVertically_OnClick(Object sender, EventArgs e) { 
		LayoutMdi(MdiLayout.TileVertical);
		m_StatusBarTextPanel.Text="[INFO] Vertical Layout applied"; 
	}
	
	private void MenuHelpAbout_OnClick(object sender, EventArgs e) {
        MessageBox.Show("©Herghelegiu Doru-Marian\ndin grupa MIA2101\nLucrare individuală\nDecembrie, 2021");
    }
}
